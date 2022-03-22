using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using System.Linq;
using Sirius.Api.Game.Rooms.Engine.Games.Freeze;

namespace SuperWireds.Conditions;

public class FreezeGameActiveInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new FreezeGameActiveCondition(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_cnd_freeze_active";
}

public class FreezeGameActiveCondition : WiredConditionBehavior
{
    public FreezeGameActiveCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
    public override bool Met()
    {
        if (Room.Game.HasActiveTimer)
        {
            var game = Room.Game.Games.FirstOrDefault(g => g.GetType() == typeof(FreezeGame));
            if (game != null && game is FreezeGame)
                return Room.ObjectHeightMap.FloorFurni.Values.Any(f => f.ClickBehavior is FreezeTileClickBehavior);
        }
        return false;
    }

    public override bool Met(Entity trigger) => Met();

    public override bool Met(FloorFurniObject trigger) => Met();

    public override WiredCondition ConditionType => WiredCondition.ActorIsGroupMember;
}

public class FreezeGameNotActiveInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new FreezeGameNotActiveCondition(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_cnd_not_freeze_active";
}

public class FreezeGameNotActiveCondition : FreezeGameActiveCondition
{
    public FreezeGameNotActiveCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
    {
    }

    public override bool Met() => !base.Met();

    public override bool Met(Entity trigger) => !base.Met(trigger);

    public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
}