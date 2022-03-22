using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects;

public class UnFreezeUserInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new UnFreezeUserAction(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_act_unfreeze_user";
}

public class UnFreezeUserAction : WiredActionBehavior
{
    public UnFreezeUserAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
    {
    }

    protected override void Handle() { }

    protected override void Handle(Entity trigger) => trigger.AllowWalk();

    protected override void Handle(FloorFurniObject trigger) { }

    public override WiredAction ActionType => WiredAction.Chat;
}