using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Conditions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Games.Banzai;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using System.Linq;

namespace SuperWireds.Conditions;

public class BanzaiGameActiveInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new BanzaiGameActiveCondition(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_cnd_banzai_active";
}

public class BanzaiGameActiveCondition : WiredConditionBehavior
{
    public BanzaiGameActiveCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem) { }
    public override bool Met()
    {
        if (Room.Game.HasActiveTimer)
        {
            var game = Room.Game.Games.FirstOrDefault(g => g.GetType() == typeof(BanzaiGame));
            if (game != null && game is BanzaiGame banzaiGame)
                return banzaiGame.BanzaiTileFiller.HasTiles;
        }
        return false;
    }

    public override bool Met(Entity trigger) => Met();

    public override bool Met(FloorFurniObject trigger) => Met();

    public override WiredCondition ConditionType => WiredCondition.ActorIsGroupMember;
}

public class BanzaiGameNotActiveInteractionBuilder : IFurnitureInteractionBuilder
{
    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        furniObject.ActionBehavior = new BanzaiGameNotActiveCondition(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_cnd_not_banzai_active";
}

public class BanzaiGameNotActiveCondition : BanzaiGameActiveCondition
{
    public BanzaiGameNotActiveCondition(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
    {
    }

    public override bool Met() => !base.Met();

    public override bool Met(Entity trigger) => !base.Met(trigger);

    public override bool Met(FloorFurniObject trigger) => !base.Met(trigger);
}