﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using Nest.JsonNetSerializer;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Plugins.FullText.Core;
using System;

namespace RawCMS.Plugins.FullText
{
    public class FullTextPlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<FullTextConfig>
    {
        public override string Name => "FullTextPlugin";

        public override string Description => "Add FullText capabilities";

        private readonly FullTextConfig config;

        private AppEngine appEngine;

        public FullTextPlugin(AppEngine appEngine, FullTextConfig config, ILogger logger) : base(appEngine, logger)
        {
            this.appEngine = appEngine;
            this.config = config;
            Logger.LogInformation("FullTextPlugin plugin loaded");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            if (config.Engine == FullText.Engine.Elastic)
            {
                RegisterElastiServices(services);
            }
        }

        private void RegisterElastiServices(IServiceCollection services)
        {
            var pool = new SingleNodeConnectionPool(new Uri(this.config.Url));
            var connection = new HttpConnection();
            var connectionSettings =
            new ConnectionSettings(pool, connection, (serializer, settings) =>
            {
                return JsonNetSerializer.Default(serializer, settings);
            })
            // new ConnectionSettings(pool, connection)
            .DisableAutomaticProxyDetection()
            .EnableHttpCompression()
            .DisableDirectStreaming()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));

            services.AddSingleton<ElasticClient>(new ElasticClient(connectionSettings));
            services.AddSingleton<FullTextService, ElasticFullTextService>();
        }

        public override void Configure(IApplicationBuilder app)
        {
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
        }

        public override void Setup(IConfigurationRoot configuration)
        {
        }
    }
}