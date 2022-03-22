using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects;

public class ToggleFreezeInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new ToggleFreezeAction(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_act_togglefreeze_user";
}

public class ToggleFreezeAction : WiredActionBehavior
{
    public ToggleFreezeAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
    {
    }

    protected override void Handle() { }

    protected override void Handle(Entity trigger)
    {
        if (trigger.CanWalk)
            trigger.DisAllowWalk();
        else
            trigger.AllowWalk();
    }

    protected override void Handle(FloorFurniObject trigger) { }

    public override WiredAction ActionType => WiredAction.Chat;
}