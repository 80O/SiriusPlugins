using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using System;

namespace SuperWireds.Effects
{
    public class AlertRoomInteractionBuilder : IFurnitureInteractionBuilder

    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new AlertRoomAction(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_act_roomalert";
    }

    public class AlertRoomAction : WiredActionBehavior
    {
        public event EventHandler<AlertRoomActionEventArgs> Alert;
        public AlertRoomAction(Room room, FloorFurniObject wiredItem) : base(room, wiredItem) { }

        protected override void Handle()
        {
            if (!string.IsNullOrEmpty(Data.StringParam))
                Alert?.Invoke(this, new() {Message = Data.StringParam});
        }

        protected override void Handle(Entity trigger) => Handle();

        protected override void Handle(FloorFurniObject trigger) => Handle();

        public override WiredAction ActionType => WiredAction.Chat;
    }

    public class AlertRoomActionEventArgs : EventArgs
    {
        public string Message { get; init; }
    }
}
