﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP.Core.Utils;
using TP.MessageBus;

namespace TP.Bff.Detran.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}