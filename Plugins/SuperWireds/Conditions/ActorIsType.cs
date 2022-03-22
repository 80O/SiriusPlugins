using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.Rooms.Engine.Unit.Bots;
using Sirius.Api.Game.Rooms.Engine.Unit.Pets;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Conditions
{
    public class ActorIsUserInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsTypeCondition<UserEntity>(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_is_user";
    }

    public class ActorIsNotUserInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsNotTypeCondition<UserEntity>(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_is_not_user";
    }

    public class ActorIsBotInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsTypeCondition<BotEntity>(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_is_bot";
    }

    public class ActorIsNotBotInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsNotTypeCondition<BotEntity>(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_is_not_bot";
    }

    public class ActorIsPetInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsTypeCondition<PetEntity>(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_is_pet";
    }

    public class ActorIsNotPetInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorIsNotTypeCondition<PetEntity>(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_actor_is_not_pet";
    }

    public class ActorIsTypeCondition<TEntityType> : WiredConditionBehavior where TEntityType : Entity
    {
        public ActorIsTypeCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => false;

        public override bool Met(Entity trigger) => trigger is TEntityType;

        public override bool Met(FloorFurniObject trigger) => false;

        public override WiredCondition ConditionType => WiredCondition.ActorIsGroupMember;
    }

    public class ActorIsNotTypeCondition<TEntityType> : ActorIsTypeCondition<TEntityType> where TEntityType : Entity
    {
        public ActorIsNotTypeCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override bool Met() => !base.Met();
        public override bool Met(Entity trigger) => !base.Met(trigger);
        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
