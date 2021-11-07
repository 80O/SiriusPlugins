using System.Threading.Tasks;
using Dapper;
using Sirius.Api.Database;
using Sirius.Api.Game.User;
using Sirius.Api.Messaging;
using Sirius.Api.Messaging.Handler;
using SuperWireds.Handlers.Messages;

namespace SuperWireds.Handlers
{
    /// <summary>
    /// This request handler will be loaded in Sirius and will receive the requests from Gaia.
    /// </summary>
    public class SuperWiredsGivePointsOnceRequestHandler : RequestHandler<SuperWiredsGivePointsOnceRequest>
    {
        private readonly IDatabaseConnectionProvider _database;
        private readonly IHabboCache _habboCache;

        public SuperWiredsGivePointsOnceRequestHandler(IDatabaseConnectionProvider database, IHabboCache habboCache)
        {
            _database = database;
            _habboCache = habboCache;
        }

        public override async Task<IMessageReplyPayload?> Handle(SuperWiredsGivePointsOnceRequest payload)
        {
            using var connection = _database.Connection();
            var result = await connection.ExecuteAsync("INSERT IGNORE INTO superwireds_rewards (item_id, user_id) VALUES (@itemId, @userId);", new { itemId = payload.ItemId, userId = payload.UserId });
            if (result != 0)
            {
                var habbo = _habboCache.FindHabboById(payload.UserId);
                habbo?.Inventory.Purse.AddPoints(payload.PointsType, payload.PointsAmount);
            }
            return new SuperWiredsGivePointsOnceReply { Given = result != 0 };
        }
    }
}