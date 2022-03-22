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
        public string Author => "The General";
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
    /// Note that this instance is already created BEFORE the whole emulator is fully launched.
    /// </summary>
    public class BasicPlugin : IPlugin
    {
        public BasicPlugin()
        {
            Console.WriteLine("Hello World!");

            /// Please read:
            /// Its important to understand how Sirius emulator exactly works.
            /// All Room game instance logic is handled by a seperate task called Gaia. This is to support scaling and distributing load.
            /// Mixing services from the Sirius namespaces with Gaia namespaces is highly discouraged,
            /// While this may work for now, as they're all resolved from the same service provider, in the future these processes are further seperated and it won't work anymore.
            /// If you have to send callbacks or data between Sirius and Gaia, have a look at the plugins like SuperWireds.
        }
    }
}
