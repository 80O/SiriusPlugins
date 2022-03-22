using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Triggers;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Triggers
{
    public class UseDiceTriggerInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new UseDiceTrigger(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_trg_use_dice";
    }

    public class UseDiceTrigger : WiredTriggerBehavior
    {
        public UseDiceTrigger(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public override void SubscribeItem(FloorFurniObject furniObject)
        {
            if (furniObject.ClickBehavior is DiceClickBehavior clickBehavior)
            {
                clickBehavior.Thrown += OnThrown;
                base.SubscribeItem(furniObject);
            }

        }

        private void OnThrown(object? sender, DiceInteractionEventArgs e)
        {
            Triggered(e.Dice, e.Entity, WiredTriggerType.Entity);
        }

        public override void UnsubscribeItem(FloorFurniObject furniObject)
        {
            if (furniObject.ClickBehavior is DiceClickBehavior clickBehavior)
            {
                clickBehavior.Thrown -= OnThrown;
                base.UnsubscribeItem(furniObject);
            }
        }

        public override WiredTrigger TriggerType => WiredTrigger.UseFurni;
    }
}
