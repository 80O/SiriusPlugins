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

    public class ActorIsDancingInteractionBuilder : IWiredInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsDancingCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string FurnitureType => "wf_cnd_actor_dancing";
    }

    public class ActorIsDancingCondition : WiredConditionBehavior
    {
        public ActorIsDancingCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => false;

        public override bool Met(Entity trigger) => trigger.IsDancing;

        public override bool Met(FloorFurniObject trigger) => false;

        public override WiredCondition ConditionType => WiredCondition.ActorIsGroupMember;
    }

    public class ActorIsNotDancingInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsNotDancingCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_not_dancing";
    }

    public class ActorIsNotDancingCondition : ActorIsDancingCondition
    {
        public ActorIsNotDancingCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => !base.Met();

        public override bool Met(Entity trigger) => !base.Met(trigger);

        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
