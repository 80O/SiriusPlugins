using System;
using System.Threading.Tasks;
using Gaia.Api.Commands;
using Sirius.Api.Game.Commands;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
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
    /// To create a command on Sirius, you must implement the <see cref="ICommand"/> or <see cref="ITargetCommand"/> interface.
    /// These interfaces are used by Sirius and NOT by Gaia.
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

    /// <summary>
    /// Gaia commands can use the <see cref="IRoomCommand"/> or <see cref="IRoomTargetCommand"/> interfaces.
    /// </summary>
    public class LookAtMeCommand : IRoomTargetCommand
    {
        public string Key => "lookatme";
        public Task<CommandResult> Handle(Room room, Entity actor, Entity target, string[] parameters)
        {
            target.LookAt(actor);
            actor.Say($"Look at me, {target.OwnerName}! I'm the captain now!");
            return Task.FromResult(CommandResult.Ok);
        }
    }
}
