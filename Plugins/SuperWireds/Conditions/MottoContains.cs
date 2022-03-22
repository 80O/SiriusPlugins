using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Conditions
{
    public class MottoContainsInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new MottoContainsCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_motto_contains";
    }

    public class MottoContainsCondition : WiredConditionBehavior
    {
        private string _motto = string.Empty;
        public MottoContainsCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => false;

        public override bool Met(Entity trigger) => trigger is UserEntity user && user.Data.Motto.Contains(_motto);

        public override bool Met(FloorFurniObject trigger) => false;

        protected override void Store(IRoom room, Triggerable data)
        {
            base.Store(room, data);
            _motto = data.StringParam;
        }

        public override WiredCondition ConditionType => WiredCondition.ActorIsWearingBadge;
    }
    public class MottoNotContainsInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new MottoNotContainsCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_not_motto_contains";
    }

    public class MottoNotContainsCondition : MottoContainsCondition
    {
        public MottoNotContainsCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => !base.Met();

        public override bool Met(Entity trigger) => !base.Met(trigger);

        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
