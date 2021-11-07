using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using System.Linq;

namespace SuperWireds.Conditions
{
    public class ActorHasRankInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new ActorHasRankCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_has_rank";
    }

    public class NotActorHasRankInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new NotActorHasRankCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_not_has_rank";
    }

    public class ActorHasRankCondition : WiredConditionBehavior
    {
        private int _rankId;

        public ActorHasRankCondition(Room room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override bool Met() => false;

        public override bool Met(Entity trigger) => trigger is UserEntity userEntity && userEntity.Rank >= _rankId;

        public override bool Met(FloorFurniObject trigger) => false;

        protected override void Store(Room room, Triggerable data)
        {
            base.Store(room, data);
            _rankId = data.IntParams.FirstOrDefault();
        }

        public override WiredCondition ConditionType => WiredCondition.ActorIsWearingEffect;
    }

    public class NotActorHasRankCondition : ActorHasRankCondition
    {
        public NotActorHasRankCondition(Room room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override bool Met() => !base.Met();

        public override bool Met(Entity trigger) => !base.Met(trigger);

        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
