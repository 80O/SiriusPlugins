using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Actions;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;
using System;

namespace SuperWireds.Effects
{

    public class InClientLinkInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
            furniObject.ActionBehavior = new InClientLinkAction(room, furniObject);
        }

        public string InteractionKey => "wf_act_inclient_link";
    }

    public class InClientLinkAction : WiredActionBehavior
    {
        public InClientLinkAction(Room room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
        }

        public event EventHandler<InClientLinkActionEventArgs> SendUrl;
        protected override void Handle()
        {
        }

        protected override void Handle(Entity trigger)
        {
            if (string.IsNullOrEmpty(Data.StringParam)) return;
            if (trigger is UserEntity user)
                SendUrl?.Invoke(this, new InClientLinkActionEventArgs(user.OwnerId, Data.StringParam));
        }

        protected override void Handle(FloorFurniObject trigger)
        {
        }

        public override WiredAction ActionType => WiredAction.Chat;
    }

    public class InClientLinkActionEventArgs : EventArgs
    {
        public uint UserId { get; }
        public string Url { get; }
        public InClientLinkActionEventArgs(uint userId, string url)
        {
            UserId = userId;
            Url = url;
        }
    }
}
