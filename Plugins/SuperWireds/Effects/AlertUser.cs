using System;
using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects;

public class AlertUserInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        furniObject.ActionBehavior = new AlertUserAction(room, furniObject);
    }

    public string InteractionKey => "wf_act_alert";
}

public class AlertUserAction : WiredActionBehavior
{
    public AlertUserAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
    {
    }

    public event EventHandler<AlertUserActionEventArgs>? Alert;
    protected override void Handle()
    {
    }

    protected override void Handle(Entity trigger)
    {
        if (string.IsNullOrEmpty(Data.StringParam)) return;
        if (trigger is UserEntity user)
            Alert?.Invoke(this, new AlertUserActionEventArgs(user.OwnerId, Data.StringParam));
    }

    protected override void Handle(FloorFurniObject trigger)
    {
    }

    public override WiredAction ActionType => WiredAction.Chat;
}

public class AlertUserActionEventArgs : EventArgs
{
    public uint UserId { get; }
    public string Message { get; }
    public AlertUserActionEventArgs(uint userId, string message)
    {
        UserId = userId;
        Message = message;
    }
}