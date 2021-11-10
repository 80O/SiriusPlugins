using Sirius.Api.DependencyInjection;
using Sirius.Api.Game.Authentication;
using Sirius.Api.Game.User;

namespace Services
{
    public interface IPrimaryService
    {
        void SomeOtherPublicFunction();
    }

    public class PrimaryService : IPrimaryService, IStartable
    {
        private readonly ISecondaryService _secondaryService;
        private readonly IAuthenticator _authenticator;

        /// <summary>
        /// Note that generally speaking all services that you inject are already completely constructed.
        /// Its best practice and recommended to do data loading in the constructor synchronously.
        /// </summary>
        public PrimaryService(ISecondaryService secondaryService, IAuthenticator authenticator)
        {
            _secondaryService = secondaryService;
            _authenticator = authenticator;
            _authenticator.Authenticated += OnHabboAuthenticated;
        }

        private void OnHabboAuthenticated(object? sender, HabboEventArgs e)
        {
            _secondaryService.DoSomethingWithPlayerId(e.Habbo.Info.Id);
        }

        /// <summary>
        /// Using the startable interface you get an callback when the emulator has loaded.
        /// </summary>
        public void Start()
        {
            _secondaryService.DoSomething();
        }

        public void SomeOtherPublicFunction()
        {

        }
    }
}
