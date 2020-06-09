﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using RawCMS.Library.JavascriptClient;
using System.Collections.Generic;

namespace RawCMS.Plugins.FullText.Core.Http
{
    public class LocalRestMessage<T>
    {
        public List<LocalError> Errors { get; set; } = new List<LocalError>();
        public List<LocalError> Warnings { get; set; } = new List<LocalError>();
        public List<LocalError> Infos { get; set; } = new List<LocalError>();

        public RestStatus Status { get; set; }

        public T Data { get; set; }

        public LocalRestMessage(T item)
        {
            Data = item;
        }
    }
}