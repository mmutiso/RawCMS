﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Newtonsoft.Json.Linq;
using RawCMS.Library.Schema;
using RawCMS.Library.Service;
using System.Collections.Generic;

namespace RawCMS.Library.Lambdas
{
    public class SchemaDeleteLambda : PostDeleteLambda
    {
        private readonly EntityService _entityService;

        public SchemaDeleteLambda(EntityService entityService)
        {
            _entityService = entityService;
        }

        public override string Name => "SchemaDeleteLambda";

        public override string Description => "Update schema in entity service";

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (collection.Equals("_schema"))
            {
                var schema = item.ToObject<CollectionSchema>();
                _entityService.AddOrReplaceEntity(schema.CollectionName, schema, this.Operation);
            }
        }
    }
}