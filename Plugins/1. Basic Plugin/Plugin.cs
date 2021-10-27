using System;
using Microsoft.Extensions.DependencyInjection;
using Sirius.Api.Plugin;

namespace Sirius.Examples.Plugin.BasicPlugin
{
    /// <summary>
    /// The plugin definition stores some basic information about your plugin including a pointer to the startup class.
    /// The definition must implement <see cref="IPluginDefinition"/>.
    /// </summary>
    public class BasicPluginDefinition : IPluginDefinition
    {
        public string Name => "Basic Plugin Example";
        public string Description => "Basic Plugin example to demonstrate developing plugins in Sirius";
        public Version Version => new(1, 0);

        /// <summary>
        /// If you have any services you want to use via dependency injection in your plugin, you can register them here.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
        }

        public Type PluginClass() => typeof(BasicPlugin);
    }

    /// <summary>
    /// The BasicPlugin class has been referenced in the plugin definition as the startup class,
    /// this is going to be our entry point for the plugin.
    /// You can use constructor dependency injection to add additional servers from the API.
    /// Its important that you use dependency injection as much as possible and avoid newing instances.
    /// </summary>
    public class BasicPlugin : IPlugin
    {
        public BasicPlugin()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
