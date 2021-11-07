using System.Threading.Tasks;
using Dapper;
using Sirius.Api.Database;
using Sirius.Api.Messaging.Handler;
using SuperWireds.Handlers.Messages;

namespace SuperWireds.Handlers
{
    public class SuperWiredsResetNotificationHandler : NotificationHandler<SuperWiredsResetNotification>
    {
        private readonly IDatabaseConnectionProvider _database;

        public SuperWiredsResetNotificationHandler(IDatabaseConnectionProvider database)
        {
            _database = database;
        }

        public override Task Handle(SuperWiredsResetNotification payload)
        {
            using var connection = _database.Connection();
            return connection.ExecuteAsync("DELETE FROM superwireds_rewards WHERE item_id = @itemId", new
            {
                itemId = payload.ItemId
            });
        }
    }
}