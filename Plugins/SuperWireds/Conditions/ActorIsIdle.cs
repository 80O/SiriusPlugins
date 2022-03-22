using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Conditions
{
    public class ActorIsIdleInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsIdleCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_is_idle";
    }

    public class NotActorIsIdleInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new NotActorIsIdleCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_not_is_idle";
    }

    public class ActorIsIdleCondition : WiredConditionBehavior
    {
        public ActorIsIdleCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override bool Met() => false;

        public override bool Met(Entity trigger) => trigger is UserEntity userEntity && userEntity.IsIdle;

        public override bool Met(FloorFurniObject trigger) => false;

        public override WiredCondition ConditionType => WiredCondition.ActorIsWearingEffect;
    }

    public class NotActorIsIdleCondition : ActorIsIdleCondition
    {
        public NotActorIsIdleCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override bool Met() => !base.Met();

        public override bool Met(Entity trigger) => !base.Met(trigger);

        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
