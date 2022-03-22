using System;
using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects
{
    public class SetRollerSpeedInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
            furniObject.ActionBehavior = new SetRollerSpeedAction(room, furniObject);
        }

        public string InteractionKey => "wf_act_roller_speed";
    }

    public class SetRollerSpeedAction : WiredActionBehavior
    {
        public SetRollerSpeedAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        protected override void Handle() => Set();

        protected override void Handle(Entity trigger) => Set();

        protected override void Handle(FloorFurniObject trigger) => Set();

        public override WiredAction ActionType => WiredAction.Chat;

        private void Set()
        {
            if (uint.TryParse(Data.StringParam, out var speed))
                Room.Data.Settings.RollerSpeed = Math.Clamp(speed, 0, 100);
        }
    }
}
