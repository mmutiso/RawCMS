﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Service;

namespace RawCMS.Library.Lambdas.JSLambdas
{
    public class JSPreSaveLambda : JsDispatcher
    {
        public override PipelineStage Stage => PipelineStage.PreOperation;

        public override DataOperation Operation => DataOperation.Write;

        public override string Name => "JSPreSaveLambda";

        public override string Description => "JSPreSaveLambda";

        public JSPreSaveLambda(EntityService entityService, CRUDService crudService) : base(entityService, crudService)
        {
        }
    }
}