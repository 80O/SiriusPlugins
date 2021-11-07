using Microsoft.Extensions.Logging;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.UserDefinedRoomEvents.InteractionBuilders;
using Sirius.Api.Plugin;

namespace SuperWireds
{
    /// Notes:
    /// Due to the way most retros use different wired furniture names and interaction keys,
    /// all wired interactions have been registered using <see cref="IFurnitureInteractionBuilder"/> rather
    /// than <see cref="IWiredInteractionBuilder"/>
    ///
    /// Default wireds in Sirius have interaction "Wired" and its interaction is automatically resolved based on the name.
    /// This is the best practice and this plugin is just a demo.
    public class SuperWiredsPlugin : IPlugin
    {
        private readonly ILogger<SuperWiredsPlugin> _logger;

        public SuperWiredsPlugin(ILogger<SuperWiredsPlugin> logger)
        {
            _logger = logger;

            _logger.LogInformation("Super Wireds Loaded!");

            /// TODO:
            /// Effect progress achievement
            /// Effect give badge
            /// Effect take badge
            /// Effect lay
        }
    }
}
