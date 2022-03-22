using System.Threading.Tasks;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Messaging.Handler;
using SuperWireds.Handlers.Messages;

namespace SuperWireds.Handlers;

public class SuperWiredsAlertRoomNotificationHandler : NotificationHandler<SuperWiredsAlertRoomNotification>
{
    private readonly IRoomInstanceManager _roomInstanceManager;

    public SuperWiredsAlertRoomNotificationHandler(IRoomInstanceManager roomInstanceManager)
    {
        _roomInstanceManager = roomInstanceManager;
    }

    public override Task Handle(SuperWiredsAlertRoomNotification payload)
    {
        var room = _roomInstanceManager.Get(payload.RoomId);
        if (room != null)
            foreach (var habbo in room.ConnectedHabbos.Values)
                habbo.Alert(payload.Message);
        return Task.CompletedTask;
    }
}