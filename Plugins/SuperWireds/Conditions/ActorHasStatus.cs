using System.Linq;
using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using Sirius.Api.Game.UserDefinedRoomEvents.InteractionBuilders;
using Sirius.Api.Utils;

namespace SuperWireds.Conditions;

public class ActorHasStatusInteractionBuilder : IWiredInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new ActorHasStatusCondition(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string FurnitureType => "wf_cnd_actor_has_status";
}

public class ActorHasStatusCondition : WiredConditionBehavior
{
    public ActorHasStatusCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
    public override bool Met() => false;

    public override bool Met(Entity trigger) => trigger.Status.Actions.Keys.Any(k => Extensions.ConvertAvatarActionToStatusCode(k).Equals(Data.StringParam));

    public override bool Met(FloorFurniObject trigger) => false;

    public override WiredCondition ConditionType => WiredCondition.ActorIsWearingBadge;
}

public class ActorNotHasStatusInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new ActorNotHasStatusCondition(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_cnd_actor_not_has_status";
}

public class ActorNotHasStatusCondition : ActorHasStatusCondition
{
    public ActorNotHasStatusCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
    public override bool Met() => !base.Met();

    public override bool Met(Entity trigger) => !base.Met(trigger);

    public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
}