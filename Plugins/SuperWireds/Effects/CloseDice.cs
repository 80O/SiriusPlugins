using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects
{
    public class CloseDiceWiredInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new CloseDiceAction(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_act_close_dice";
    }

    public class CloseDiceAction : WiredActionBehavior
    {
        public CloseDiceAction(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
        protected override void Handle() => Close();

        protected override void Handle(Entity trigger) => Close();
        protected override void Handle(FloorFurniObject trigger) => Close();
        public override WiredAction ActionType => WiredAction.CallAnotherStack;

        private void Close()
        {
            foreach (var itemId in Data.StuffIds)
                if (Room.ObjectHeightMap.FloorFurni.TryGetValue(itemId, out var item))
                {
                    if (item.ClickBehavior is DiceClickBehavior diceClickBehavior)
                        diceClickBehavior.Close(null);
                }
        }
    }
}
