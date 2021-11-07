using System.Linq;
using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.DataFormat;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Conditions
{
    public class DiceHasValueInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior= new DiceHasValueCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_dice_value";
    }

    public class DiceHasValueCondition : WiredConditionBehavior
    {
        private int _number;
        public DiceHasValueCondition(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met()
        {
            if (_number > 0)
                return SelectedItems.All(d => d.ClickBehavior is DiceClickBehavior && d.ExtraData is LegacyDataFormat data && data.Data.Equals(_number.ToString()));
            return false;
        }

        public override bool Met(Entity trigger) => Met();

        public override bool Met(FloorFurniObject trigger) => Met();

        protected override void Store(Room room, Triggerable data)
        {
            base.Store(room, data);
            _number = data.IntParams.FirstOrDefault();
        }

        public override WiredCondition ConditionType => WiredCondition.ActorIsWearingEffect;
    }
    public class NotDiceHasValueInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new NotDiceHasValueCondition(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_cnd_not_dice_value";
    }

    public class NotDiceHasValueCondition : DiceHasValueCondition
    {
        public NotDiceHasValueCondition(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        public override bool Met() => !base.Met();

        public override bool Met(Entity trigger) => !base.Met(trigger);

        public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
    }
}
