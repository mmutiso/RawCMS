﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Lambdas;
using RawCMS.Plugins.FullText.Core;
using System.Collections.Generic;
using System.Linq;

namespace RawCMS.Plugins.FullText.Lambdas
{
    public abstract class BaseFullTextLambda : DataProcessLambda
    {
        public FullTextService FullTextService { get; private set; }
        public FullTextUtilityService Helper { get; private set; }

        public BaseFullTextLambda(FullTextService fullTextService, FullTextUtilityService helper)
        {
            this.FullTextService = fullTextService;
            this.Helper = helper;
        }
    }

    public class DeleteFullTextLambda : BaseFullTextLambda
    {
        public override string Name => "FullTextMapping";

        public override string Description => this.Name;

        public override PipelineStage Stage => PipelineStage.PostOperation;

        public override DataOperation Operation => DataOperation.Delete;

        public DeleteFullTextLambda(FullTextService fullTextService, FullTextUtilityService helper) : base(fullTextService, helper)
        {
        }

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            var filter = this.Helper.GetFilter(collection);
            if (filter == null) return;

            var id = item["_id"];
            if (id == null) return;

            var index = Helper.GetIndexName(collection);
            this.FullTextService.DeleteDocument(index, id.ToString());
        }
    }

    public class FullTextLambda : BaseFullTextLambda
    {
        public override string Name => "FullTextMapping";

        public override string Description => this.Name;

        public override PipelineStage Stage => PipelineStage.PostOperation;

        public override DataOperation Operation => DataOperation.Write;

        protected readonly FullTextService fullTextService;
        protected readonly FullTextUtilityService helper;

        public FullTextLambda(FullTextService fullTextService, FullTextUtilityService helper) : base(fullTextService, helper)
        {
        }

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            var filter = Helper.GetFilter(collection);
            if (filter == null) return;

            JObject searchDocument = new JObject();

            var list = new List<string>()
                {
                    "_id", //id is alway neededs
                    "_createdon",
                    "_modifiedon"
                };

            //if empty add all
            if (filter.IncludedField == null || filter.IncludedField.Count == 0)
            {
                list.AddRange(item.Properties().Select(p => p.Name).Distinct().ToList());
            }
            else
            {
                list.AddRange(filter.IncludedField);
            }

            foreach (var field in list)
            {
                searchDocument[field] = item[field];
            }

            this.FullTextService.AddDocumentRaw(this.Helper.GetIndexName(collection), searchDocument);
        }
    }
}