using System;
using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Effects
{
    public class BubbleAlertInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new BubbleAlertAction(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_act_bubblealert";
    }

    public class BubbleAlertAction : WiredActionBehavior
    {
        public EventHandler<BubbleAlertEventArgs> Alert;

        private string _title = "title";
        private string _type  = "type";
        private string _message = "message";
        public BubbleAlertAction(Room room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        protected override void Handle()
        {
        }

        protected override void Handle(Entity trigger)
        {
            if (trigger is UserEntity)
                Alert?.Invoke(this, new(trigger.OwnerId, _title, _type, _message));
        }

        protected override void Handle(FloorFurniObject trigger)
        {
        }

        protected override void Store(Room room, Triggerable data)
        {
            var parameters = data.StringParam.Split(";");
            if (parameters.Length == 3)
            {
                _title = parameters[0];
                _type = parameters[1];
                _message = parameters[2];
            }
            data.StringParam = $"{_title};{_type};{_message}";
            base.Store(room, data);
        }

        public override WiredAction ActionType => WiredAction.Chat;
    }

    public class BubbleAlertEventArgs : EventArgs
    {
        public uint UserId { get; }
        public string Title { get; }
        public string Type { get; }
        public string Message { get; }
        public BubbleAlertEventArgs(uint userId, string title, string type, string message)
        {
            UserId = userId;
            Title = title;
            Type = type;
            Message = message;
        }
    }
}
