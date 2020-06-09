﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Microsoft.AspNetCore.Http;
using RawCMS.Plugins.ApiGateway.Classes.Settings;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Classes.Balancer.Policy
{
    public class RoundRobin : BalancerPolicy
    {
        public RoundRobin(ApiGatewayConfig config) : base(config)
        {
        }

        public override string Name => "RoundRobin";

        public async override Task<object> Execute(BalancerData balancerData, HttpContext context, RawHandler handler)
        {
            var vhost = context.Items["bal-vhost"] as BalancerOption;
            balancerData.LastServed = (balancerData.LastServed + 1) % vhost.Nodes.Length;
            var node = vhost.Nodes[balancerData.LastServed];
            await handler.HandleRequest(context, node);
            return node;
        }
    }
}