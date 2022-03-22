using System;
using System.Threading.Tasks;
using Gaia.Api.Rooms;
using Gaia.Api.Rooms.Modules;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Plugin;

namespace AvatarPoof
{
    public class AvatarPoofPlugin : IPlugin
    {
    }

    public class AvatarModule : IRoomSessionModule
    {
        private IRoomSession _session = null!;
        public Task Initialize(IRoomSession session)
        {
            _session = session;
            session.Room.Npcs.Players.PlayerEnteredRoom += SubscribeToPlayer;
            return Task.CompletedTask;
        }

        private void SubscribeToPlayer(object? sender, EntityEventArgs e)
        {
            if (e.Entity is UserEntity user)
            {
                if (_session.Room.ConnectedHabbos.TryGetValue(user.OwnerId, out var info))
                    info.FigureUpdated += (_, _) => e.Entity.Effect(108, TimeSpan.FromSeconds(1));
            }
        }
    }
}
