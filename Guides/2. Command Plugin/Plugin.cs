using System;
using System.Threading.Tasks;
using Sirius.Api.Game.Commands;
using Sirius.Api.Game.User;
using Sirius.Api.Plugin;

namespace Sirius.Examples.Plugin.CommandPlugin
{
    public class CommandPluginDefinition : IPluginDefinition
    {
        public string Name => "Command Example Plugin";
        public string Description => "Basic Plugin example to demonstrate developing plugins in Sirius";
        public Version Version => new(1, 0);

        public Type PluginClass() => typeof(CommandPlugin);
    }

    public class CommandPlugin : IPlugin
    {
    }

    /// <summary>
    /// To create a command on Sirius, you must implement the ICommand interface.
    /// </summary>
    public class PingCommand : ICommand
    {
        public string Key => "ping";

        public Task<CommandResult> Handle(IHabbo habbo, string[] parameters)
        {
            habbo.Alert("Pong!");
            return Task.FromResult(CommandResult.Ok);
        }
    }
}
