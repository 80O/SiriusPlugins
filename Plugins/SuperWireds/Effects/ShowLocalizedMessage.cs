using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Chat;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects
{
    public class ShowLocalizedMessageActionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ShowLocalizedMessageAction(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_act_show_localized_message";
    }

    public class ShowLocalizedMessageAction : WiredActionBehavior
    {
        public ShowLocalizedMessageAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        protected override void Handle()
        {
        }

        protected override void Handle(Entity trigger)
        {
            if (trigger is UserEntity user)
                user.WhisperToLocalized(trigger, Data.StringParam, new() { { "username", user.OwnerName } }, ChatStyle.Wired, TalkSource.Wired);
        }

        protected override void Handle(FloorFurniObject trigger)
        {
        }

        public override WiredAction ActionType => WiredAction.Chat;
    }
}
