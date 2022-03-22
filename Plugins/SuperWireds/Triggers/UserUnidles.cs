using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors.Click;
using Sirius.Api.Game.Items.Behaviors.Wired.Triggers;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Game.UserDefinedRoomEvents;

namespace SuperWireds.Triggers
{
    public class UserUnidlesInteractionBuilder : IFurnitureInteractionBuilder
    {
        public void AttachBehaviors(IRoom room, FloorFurniObject furniObject)
        {
            furniObject.ActionBehavior = new UserUnidlesTrigger(room, furniObject);
            furniObject.ClickBehavior = new WiredClickBehavior(furniObject);
        }

        public string InteractionKey => "wf_trg_user_unidles";
    }

    public class UserUnidlesTrigger : WiredTriggerBehavior
    {
        public UserUnidlesTrigger(IRoom room, FloorFurniObject wiredItem) : base(room, wiredItem)
        {
            room.Npcs.EntityAdded += OnEntityAdded;
            room.Npcs.EntityRemoved += OnEntityRemoved;
            foreach (var entity in room.Npcs.Entities.Values)
                if (entity.Type == EntityType.User)
                    entity.IdleStatusUpdated += OnIdleStatusUpdated;
        }


        private void OnEntityAdded(object? sender, EntityEventArgs e)
        {
            if (e.Entity is UserEntity)
                e.Entity.IdleStatusUpdated += OnIdleStatusUpdated;
        }
        private void OnEntityRemoved(object? sender, EntityEventArgs e)
        {
            if (e.Entity is UserEntity)
                e.Entity.IdleStatusUpdated -= OnIdleStatusUpdated;
        }

        private void OnIdleStatusUpdated(object? sender, EntityIdleStatusUpdatedEventArgs e)
        {
            if (!e.IsIdle)
                Triggered(null, e.Entity, WiredTriggerType.Entity);
        }

        public override void OnRemoved(IRoom room)
        {
            room.Npcs.EntityAdded -= OnEntityAdded;
            room.Npcs.EntityRemoved -= OnEntityRemoved;
            foreach (var entity in room.Npcs.Entities.Values)
                entity.IdleStatusUpdated -= OnIdleStatusUpdated;
            base.OnRemoved(room);
        }

        public override WiredTrigger TriggerType => WiredTrigger.AvatarEntersRoom;
    }
}
