using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects
{

    public class GiveHandItemInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new GiveHandItemAction(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_act_give_handitem";
    }

    public class GiveHandItemAction : WiredActionBehavior
    {
        public GiveHandItemAction(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        protected override void Handle()
        {
        }

        protected override void Handle(Entity trigger)
        {
            if (uint.TryParse(Data.StringParam, out var handItem))
                trigger.HandItem = handItem;
        }

        protected override void Handle(FloorFurniObject trigger)
        {
        }

        public override WiredAction ActionType => WiredAction.Chat;
    }
}
