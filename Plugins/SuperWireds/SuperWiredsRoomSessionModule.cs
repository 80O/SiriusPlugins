using Gaia.Api.Rooms;
using Gaia.Api.Rooms.Modules;
using Microsoft.Extensions.Logging;
using Sirius.Api.Game.Items;
using Sirius.Api.Game.Rooms.Engine.Map;
using SuperWireds.Effects;
using SuperWireds.Handlers.Messages;
using System;
using System.Threading.Tasks;

namespace SuperWireds
{
    /// <summary>
    /// In Sirius emulator all ROOM logic is running in a different process / task.
    /// This room logic does not include players inventories etc. Thus we need to send a message from Gaia to Sirius
    /// to handle giving out the credits for us.
    /// </summary>
    public class SuperWiredsRoomSessionModule : IRoomSessionModule
    {
        private readonly ILogger<SuperWiredsRoomSessionModule> _logger;
        private IRoomSession _session = null!;

        public SuperWiredsRoomSessionModule(ILogger<SuperWiredsRoomSessionModule> logger)
        {
            _logger = logger;
        }

        public Task Initialize(IRoomSession session)
        {
            _logger.LogInformation("Initialized Super Wireds Room Session Module");
            _session = session;

            _session.Room.ObjectHeightMap.FloorFurnitureAdded += OnFloorFurnitureAdded;
            _session.Room.ObjectHeightMap.FloorFurnitureRemoved += OnFloorFurnitureRemoved;

            foreach (var item in _session.Room.ObjectHeightMap.FloorFurni.Values)
                Register(item);

            return Task.CompletedTask;
        }
        private async void OnFloorFurnitureAdded(object? sender, FloorFurnitureEventArgs e)
        {
            Register(e.FloorFurniture);
            if (e.FloorFurniture.ActionBehavior is GivePointsOnceAction)
            {
                if (_session.Room.ObjectHeightMap.IsFurnitureLoaded)
                    try
                    {
                        await ResetData(e.FloorFurniture.Id);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogWarning(exception, "Failed to reset data");
                    }
            }
        }

        private void Register(FloorFurniObject item)
        {
            if (item.ActionBehavior is GivePointsOnceAction points)
                points.GivePoints += OnGivePoints;
            if (item.ActionBehavior is AlertUserAction alert)
                alert.Alert += OnAlert;
            if (item.ActionBehavior is AlertRoomAction roomAlert)
                roomAlert.Alert += OnRoomAlert;
            if (item.ActionBehavior is BubbleAlertAction bubbleAlert)
                bubbleAlert.Alert += OnBubbleAlert;
            if (item.ActionBehavior is InClientLinkAction inClientLink)
                inClientLink.SendUrl += OnInClientLink;
        }

        private void OnFloorFurnitureRemoved(object? sender, FloorFurnitureEventArgs e)
        {
            if (e.FloorFurniture.ActionBehavior is GivePointsOnceAction action)
                action.GivePoints -= OnGivePoints;
            if (e.FloorFurniture.ActionBehavior is AlertUserAction alert)
                alert.Alert -= OnAlert;
            if (e.FloorFurniture.ActionBehavior is AlertRoomAction roomAlert)
                roomAlert.Alert += OnRoomAlert;
            if (e.FloorFurniture.ActionBehavior is BubbleAlertAction bubbleAlert)
                bubbleAlert.Alert += OnBubbleAlert;
            if (e.FloorFurniture.ActionBehavior is InClientLinkAction inClientLink)
                inClientLink.SendUrl += OnInClientLink;
        }

        private async void OnInClientLink(object? sender, InClientLinkActionEventArgs e)
        {
            try
            {
                await _session.Send(new SuperWiredsInClientLinkNotification { UserId = e.UserId, Url = e.Url});
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send SuperWiredsInClientLinkNotification message");
            }
        }

        private async void OnBubbleAlert(object? sender, BubbleAlertEventArgs e)
        {
            try
            {
                await _session.Send(new SuperWiredsBubbleAlertNotification
                {
                    UserId = e.UserId,
                    Title = e.Title,
                    Type = e.Type,
                    Message = e.Message
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send SuperWiredsBubbleAlertNotification message");
            }
        }

        private async void OnGivePoints(object? sender, GivePointsData e)
        {
            try
            {
                await _session.Send<SuperWiredsGivePointsOnceReply>(
                    new SuperWiredsGivePointsOnceRequest
                    {
                        ItemId = e.ItemId,
                        UserId = e.UserId,
                        PointsAmount = e.Amount,
                        PointsType = e.PointsType
                    });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send SuperWiredsGivePointsOnceRequest message");
            }
        }

        private async void OnAlert(object? sender, AlertUserActionEventArgs e)
        {
            try
            {
                await _session.Send(new SuperWiredsAlertNotification {UserId = e.UserId, Message = e.Message});
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send SuperWiredsAlertNotification message");
            }
        }
        private async void OnRoomAlert(object? sender, AlertRoomActionEventArgs e)
        {
            try
            {
                await _session.Send(new SuperWiredsAlertRoomNotification { Message = e.Message});
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send SuperWiredsAlertRoomNotification message");
            }
        }

        private async Task ResetData(uint itemId)
        {
            await _session.Send(new SuperWiredsResetNotification
            {
                ItemId = itemId
            });
        }
    }
}
