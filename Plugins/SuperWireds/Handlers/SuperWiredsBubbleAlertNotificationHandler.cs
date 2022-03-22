using System.Threading.Tasks;
using Sirius.Api.Game.Notifications;
using Sirius.Api.Game.User;
using Sirius.Api.Messaging.Handler;
using SuperWireds.Handlers.Messages;

namespace SuperWireds.Handlers
{
    public class SuperWiredsBubbleAlertNotificationHandler : NotificationHandler<SuperWiredsBubbleAlertNotification>
    {
        private readonly IAlertsService _alertsService;
        private readonly IHabboCache _habboCache;

        public SuperWiredsBubbleAlertNotificationHandler(IAlertsService alertsService, IHabboCache habboCache)
        {
            _alertsService = alertsService;
            _habboCache = habboCache;
        }

        public override Task Handle(SuperWiredsBubbleAlertNotification payload)
        {
            var habbo = _habboCache.FindHabboById(payload.UserId);
            if (habbo != null)
                return _alertsService.BubbleAlert(habbo, payload.Type, payload.Title, payload.Message);
            return Task.CompletedTask;
        }
    }
}