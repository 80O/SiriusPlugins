using System.Threading.Tasks;
using Sirius.Api.Game.User;
using Sirius.Api.Messaging.Handler;
using SuperWireds.Handlers.Messages;

namespace SuperWireds.Handlers;

public class SuperWiredsAlertNotificationHandler : NotificationHandler<SuperWiredsAlertNotification>
{
    private readonly IHabboCache _habboCache;

    public SuperWiredsAlertNotificationHandler(IHabboCache habboCache)
    {
        _habboCache = habboCache;
    }
    public override Task Handle(SuperWiredsAlertNotification payload)
    {
        var user = _habboCache.FindHabboById(payload.UserId);
        user?.Alert(payload.Message);
        return Task.CompletedTask;
    }
}