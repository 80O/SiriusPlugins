using System.Threading.Tasks;
using Sirius.Api.Game.Notifications;
using Sirius.Api.Game.User;
using Sirius.Api.Messaging.Handler;
using SuperWireds.Handlers.Messages;

namespace SuperWireds.Handlers;

public class SuperWiredsInClientLinkNotificationHandler : NotificationHandler<SuperWiredsInClientLinkNotification>
{
    private readonly IAlertsService _alertsService;
    private readonly IHabboCache _habboCache;

    public SuperWiredsInClientLinkNotificationHandler(IAlertsService alertsService, IHabboCache habboCache)
    {
        _alertsService = alertsService;
        _habboCache = habboCache;
    }

    public override Task Handle(SuperWiredsInClientLinkNotification payload)
    {
        var habbo = _habboCache.FindHabboById(payload.UserId);
        if (habbo != null)
            return _alertsService.InClientLink(habbo, payload.Url);
        return Task.CompletedTask;
    }
}