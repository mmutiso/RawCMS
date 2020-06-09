﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using MongoDB.Bson;
using MongoDB.Driver;

namespace RawCMS.Library.Lambdas
{
    public abstract class AlterQueryLambda : Lambda
    {
        public abstract void Alter(string collection, FilterDefinition<BsonDocument> query);
    }

    public abstract class CollectionAlterQueryLambda : Lambda
    {
        public abstract string Collection { get; set; }

        public abstract void Alter(FilterDefinition<BsonDocument> query);
    }
}