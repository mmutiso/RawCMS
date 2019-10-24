﻿using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Schema;
using RawCMS.Plugins.FullText.Core;
using RawCMS.Plugins.FullText.Lambdas;
using System;
using System.Collections.Generic;
using Xunit;

namespace RawCMS.Test
{
    public class ElasticFullText
    {
        public class LogDocument
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Body { get; set; }
        }

        //public class MyFirstCustomJsonNetSerializer : ConnectionSettingsAwareSerializerBase
        //{
        //    public MyFirstCustomJsonNetSerializer(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
        //        : base(builtinSerializer, connectionSettings) { }

        //    protected override JsonSerializerSettings CreateJsonSerializerSettings() =>
        //        new JsonSerializerSettings
        //        {
        //            NullValueHandling = NullValueHandling.Include
        //        };

        //    protected override void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
        //        resolver.NamingStrategy = new SnakeCaseNamingStrategy();
        //}

        [Fact]
        public void CRUD()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9300"));
            var connection = new HttpConnection();
            var connectionSettings =
            new ConnectionSettings(pool, connection, (serializer, settings) =>
            {
                //return new MyFirstCustomJsonNetSerializer(serializer, settings);
                return JsonNetSerializer.Default(serializer, settings);
            })
            // new ConnectionSettings(pool, connection)
            .DisableAutomaticProxyDetection()
            .EnableHttpCompression()
            .DisableDirectStreaming()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));

            var client = new ElasticClient(connectionSettings);

            var service = new ElasticFullTextService(client);

            var indexName = Guid.NewGuid().ToString();

            service.CreateIndex(indexName);

            LogDocument doc = null;
            for (int i = 0; i < 500; i++)

            {
                doc = new LogDocument()
                {
                    Body = $"My first document into index, position is number{i}"
                };
                service.AddDocument(indexName, doc);
            }

            var item = service.GetDocumentRaw(indexName, doc.Id.ToString());
            Assert.Equal(item["Id"], doc.Id.ToString());

            //search

            var items = service.SearchDocumentsRaw(indexName, "number1*", 0, 140);
            Assert.Equal(items.Count, 111);
        }

        [Fact]
        public void Serialize()
        {
            var coll = new CollectionSchema()
            {
                PluginConfiguration = new Dictionary<string, Newtonsoft.Json.Linq.JObject>()
                {
                    { "prova",JObject.FromObject(new FullTextFilter(){
                        CollectionName="PROC",
                        IncludedField=new List<string>()
                        {
                             "dd",
                            "dd"
                        }
                    }) }
                }
            };

            var obj = JObject.FromObject(coll);
            var result = obj.ToString();
        }
    }
}