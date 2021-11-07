using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using Sirius.Api.Game.UserDefinedRoomEvents.InteractionBuilders;

namespace SuperWireds.Conditions
{
    public class ActorCanWalkInteractionBuilder : IWiredInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorCanWalkCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string FurnitureType => "wf_cnd_actor_can_walk";
    }

    public class ActorCanWalkCondition : WiredConditionBehavior
    {
        public ActorCanWalkCondition(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => false;

        public override bool Met(Entity trigger) => trigger.CanWalk;

        public override bool Met(FloorFurniObject trigger) => false;

        public override WiredCondition ConditionType => WiredCondition.ActorIsGroupMember;
    }

    public class ActorCanNotWalkInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorCanNotWalkCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_cannot_walk";
    }

    public class ActorCanNotWalkCondition : ActorCanWalkCondition
    {
        public ActorCanNotWalkCondition(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => !base.Met();

        public override bool Met(Entity trigger) => !base.Met(trigger);

        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
