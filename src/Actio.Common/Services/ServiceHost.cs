﻿using System;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var WebHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();
            
            return new HostBuilder(WebHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            
            private readonly IWebHost _webHost;
            private IBusClient _bus;
            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public Busbuilder UseRabbitMq()
            {
                _bus = (IBusClient) _webHost.Services.GetService(typeof(IBusClient));
                return new Busbuilder(_webHost,_bus);
            }
            
            
            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class Busbuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
                private IBusClient _bus;
                
                public Busbuilder(IWebHost webHost,IBusClient bus)
                {
                    _webHost = webHost;
                    _bus = bus;
                }
                
                public Busbuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
                {
                    var hundler = (ICommandHandler<TCommand>) _webHost.Services
                        .GetService(typeof(ICommandHandler<TCommand>));
                    _bus.WithCommandHandlerASync(hundler);
                    return this;
                    
                }
                
                public Busbuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
                {
                    var hundler = (IEventHandler<TEvent>) _webHost.Services
                        .GetService(typeof(IEventHandler<TEvent>));
                    _bus.WithEventHandlerASync(hundler);
                    return this;
                }
                
                public override ServiceHost Build()
                {
                    return new ServiceHost(_webHost);
                }
            
        }
    }
}