using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects
{
    public class WalkToFurniInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new WalkToFurniAction(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_act_walk_to_furni";
    }

    public class WalkToFurniAction : WiredActionBehavior
    {
        public WalkToFurniAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }

        protected override void Handle()
        {
        }

        protected override void Handle(Entity trigger)
        {
            var location = GetRandomTeleportLocation();
            if (location != null)
                trigger.WalkTo(location);
        }

        protected override void Handle(FloorFurniObject trigger)
        {
        }

        public override WiredAction ActionType => WiredAction.Teleport;
    }
}
