using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Triggers;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Triggers
{
    public class RoomLoadedTriggerInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new RoomLoadedTrigger(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_trg_room_loaded";
    }

    public class RoomLoadedTrigger : WiredTriggerBehavior
    {
        public RoomLoadedTrigger(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override WiredTrigger TriggerType => WiredTrigger.AvatarCaught;

        public override void OnRoomLoad(IRoom room)
        {
            base.OnRoomLoad(room);
            Triggered(null, null, WiredTriggerType.None);
        }
    }
}
