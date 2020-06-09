﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RawCMS.Client.BLL.Core;
using RawCMS.Client.BLL.Interfaces;
using RawCMS.Client.BLL.Services;

namespace RawCMS.Client
{
    internal class Program
    {
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add services
            serviceCollection.AddTransient<IConfigService, ConfigService>();
            serviceCollection.AddTransient<IClientConfigService, ClientConfigService>();
            serviceCollection.AddTransient<ILoggerService, LoggerService>();
            serviceCollection.AddTransient<IRawCmsService, RawCmsService>();
            serviceCollection.AddTransient<ITokenService, TokenService>();

            // add logging
            serviceCollection.AddLogging(builder =>
             {
                 builder.SetMinimumLevel(LogLevel.Trace);
                 builder.AddNLog(new NLogProviderOptions
                 {
                     CaptureMessageTemplates = true,
                     CaptureMessageProperties = true
                 });
             });

            // add app
            serviceCollection.AddTransient<App>();
        }

        private static int Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            var app = serviceProvider.GetService<App>();

            app.Run(args);

            return 0;
        }
    }
}