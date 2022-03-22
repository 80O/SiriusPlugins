using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using System;
using Microsoft.Extensions.Logging;
using Sirius.Api.Game.Items.Behaviors.Click;

namespace SuperWireds.Effects;

public class GivePointsOnceInteractionBuilder : IFurnitureInteractionBuilder
{
    private readonly ILogger<GivePointsOnceInteractionBuilder> _logger;

    public GivePointsOnceInteractionBuilder(ILogger<GivePointsOnceInteractionBuilder> logger)
    {
        _logger = logger;
    }

    public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
    {
        _logger.LogInformation($"CREATED {furniObject.Id}");
        furniObject.ActionBehavior = new GivePointsOnceAction(room, furniObject);
        furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
    }

    public string InteractionKey => "wf_act_give_points_once";
}

public class GivePointsData : EventArgs
{
    public uint ItemId { get; }
    public uint UserId { get; }
    public uint PointsType { get; }
    public int Amount { get; }
    public GivePointsData(uint itemId, uint userId, uint pointsType, int amount)
    {
        ItemId = itemId;
        UserId = userId;
        PointsType = pointsType;
        Amount = amount;
    }
}

public class GivePointsOnceAction : WiredActionBehavior
{
    public event EventHandler<GivePointsData>? GivePoints;
    public event EventHandler? Reset;
    private readonly FloorFurniObject _wiredItem;

    private uint _pointsType;
    private int _pointsAmount;

    public GivePointsOnceAction(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
    {
        _wiredItem = wiredItem;
    }

    protected override void Handle()
    {
    }

    protected override void Handle(Entity trigger)
    {
        if (_pointsAmount > 0 && trigger is UserEntity)
            GivePoints?.Invoke(this, new GivePointsData(_wiredItem.Id, trigger.OwnerId, _pointsType, _pointsAmount));
    }

    protected override void Handle(FloorFurniObject trigger)
    {
    }

    protected override void Store(IRoom room, Triggerable data)
    {
        base.Store(room, data);
        if (string.IsNullOrEmpty(data.StringParam)) return;
        var parameters = data.StringParam.Split(",");
        if (parameters.Length < 2) return;
        if (uint.TryParse(parameters[0], out _pointsType) &&
            int.TryParse(parameters[1], out _pointsAmount)) return;
        _pointsType = 0;
        _pointsAmount = 0;
        data.StringParam = "PointType,Amount";
    }

    public override void OnPlace(IRoom room)
    {
        base.OnPlace(room);
        Reset?.Invoke(this, EventArgs.Empty);
    }

    public override WiredAction ActionType => WiredAction.Chat;
}