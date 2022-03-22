using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects;

public class MakeRotateInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        furniObject.ActionBehavior = new MakeRotateAction(room, furniObject);
    }

    public string InteractionKey => "wf_act_make_rotate";
}

public class MakeRotateAction : WiredActionBehavior
{
    public MakeRotateAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
    protected override void Handle()
    {
    }

    protected override void Handle(Entity trigger)
    {
        if (!int.TryParse(Data.StringParam, out var rotationValue)) return;
        var rotation = RotationUtils.FromValue(rotationValue);
        trigger.SetRotation(rotation);
    }

    protected override void Handle(FloorFurniObject trigger)
    {
    }

    public override WiredAction ActionType => WiredAction.Chat;
}