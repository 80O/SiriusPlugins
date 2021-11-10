using Microsoft.Extensions.Logging;
using Sirius.Api.Game.User;

namespace Services
{
    /// <summary>
    /// Interfaces are automatically resolved to the implementation by the default matching scheme
    /// of I + class name.
    /// </summary>
    public interface ISecondaryService
    {
        void DoSomething();
        void DoSomethingWithPlayerId(uint playerId);
    }

    public class SecondaryService : ISecondaryService
    {
        private readonly IHabboCache _habboCache;
        private readonly ILogger<SecondaryService> _logger;

        public SecondaryService(IHabboCache habboCache, ILogger<SecondaryService> logger)
        {
            _habboCache = habboCache;
            _logger = logger;
        }

        public void DoSomething()
        {
            _logger.LogInformation("I did something!");
        }

        public void DoSomethingWithPlayerId(uint playerId)
        {
            var habbo = _habboCache.FindHabboById(playerId);
            if (habbo != null)
                habbo.Inventory.Purse.Credits += 100;
        }
    }
}