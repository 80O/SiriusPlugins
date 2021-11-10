using System;
using Microsoft.Extensions.DependencyInjection;
using Sirius.Api.Plugin;

namespace Sirius.Examples.Plugin.Services
{
    public class ServicesPluginDefinition : IPluginDefinition
    {
        public string Name => "Services Plugin Example";
        public string Description => "Example how you can start standalone services and inject other services from the emulator.";
        public Version Version => new(1, 0);

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
        }

        public Type PluginClass() => typeof(ServicesPlugin);
    }

    public class ServicesPlugin : IPlugin
    {
        public ServicesPlugin()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
