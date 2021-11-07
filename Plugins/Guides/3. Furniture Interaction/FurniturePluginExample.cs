using Sirius.Api.Game.Items;
using Sirius.Api.Game.Items.Behaviors;
using Sirius.Api.Game.Items.Behaviors.Walk;
using Sirius.Api.Game.Items.InteractionBuilders;
using Sirius.Api.Game.Rooms;
using Sirius.Api.Game.Rooms.Engine.Furni;
using Sirius.Api.Game.Rooms.Engine.Unit;
using Sirius.Api.Plugin;
using System;

namespace Sirius.Examples.Plugin.FurnitureInteraction
{
    public class FurniturePluginExample : IPluginDefinition
    {
        public string Name => "Furniture Interaction Plugin Example";
        public string Description => "Plugin to demonstrate how to create custom furniture interaction.";
        public Version Version => new(1, 0);

        public Type PluginClass() => typeof(FurniturePlugin);
    }

    public class FurniturePlugin : IPlugin
    {
        public FurniturePlugin()
        {
            Console.WriteLine("Hello World!");
        }
    }

    /// <summary>
    /// In order to create our furniture, we need some way to register it to the emulator.
    /// We do this by using an <see cref="IFurnitureInteractionBuilder"/>. By implementing
    /// this interface, the emulator will pickup on your interactions and creates the interaction
    /// based on the <see cref="InteractionKey"/>
    ///
    /// A furniture interaction is compromised of 3 behaviors:
    /// - Walk Interaction
    /// - Click Interaction
    /// - "Other" Action Interaction
    ///
    /// The <see cref="IWalkBehavior"/> can be used to define if an <see cref="Entity"/> can walk
    /// onto a furniture, events that are raised for walk on or off furniture.
    ///
    /// The <see cref="IClickBehavior"/> can be used to react based on click interactions.
    ///
    /// The <see cref="IActionBehavior"/> can be used to react to other events like placing, moving and picking up a furniture.
    ///
    /// Any behavior can also implement the <see cref="ICycleable"/> class to hook into the room cycle. Its preferred to use this over running tasks
    /// and using Task.Delay
    /// You can also schedule tasks using Room.Cycle.ScheduleTask(Action, delay)
    /// </summary>
    public class InteractShowMessageFurnitureInteractionBuilder : IFurnitureInteractionBuilder
    {
        /// Define the interaction key here so that you can refer to that in your items database table.
        public string InteractionKey => "showmessage";

        /// This method must be implemented which is used to attach non default behaviors.
        public void AttachBehaviors(Room room, FloorFurniObject furniObject)
        {
            /// In this example a custom IWalkBehavior is attached.
            furniObject.WalkBehavior = new WalkOnShowMessageWalkBehavior(furniObject);

            /// As well as a custom IClickBehavior.
            furniObject.ClickBehavior = new ClickShowMessageBehavior(furniObject);

            // You don't need to implement every behavior. Anything unassigned gets the default behavior:
            // DefaultWalkBehavior, DefaultClickBehavior and DefaultActionBehavior
            // furniObject.ActionBehavior = DefaultActionBehavior
        }
    }

    /// In order to implement the interactions, you've got two options to implement:
    ///
    /// - Implement the IWalkBehavior interface
    /// - Extend an existing behavior.
    ///
    /// For the walk behavior we'll extend the default walk behavior as it has already a lot of
    /// logic implemented for you so you don't need to worry about implementing the whole interface
    /// and we can just override the methods we want to change.
    public class WalkOnShowMessageWalkBehavior : DefaultWalkBehavior
    {
        public WalkOnShowMessageWalkBehavior(FloorFurniObject furniObject) : base(furniObject)
        {

        }

        public override void OnWalkOn(Room room, Entity entity)
        {
            base.OnWalkOn(room, entity);
            entity.WhisperTo(entity, "You walked onto me!");
        }
    }

    /// As explained before, you can also implement the specific interface to have full behavioral control.
    ///
    /// Make sure to fire any events as other logic relies on it, example Wireds triggers etc.
    public class ClickShowMessageBehavior : IClickBehavior
    {
        private readonly FloorFurniObject _furniObject;

        public ClickShowMessageBehavior(FloorFurniObject furniObject)
        {
            _furniObject = furniObject;
        }

        public event EventHandler<FurniClickedEventArgs> Clicked;
        public FurnitureInteractionError? CanClick(Room room, Entity entity)
        {
            return null;
        }

        public void OnClick(Room room, Entity entity, int? param = null)
        {
            entity.WhisperTo(entity, "You clicked me!");
            Clicked?.Invoke(this, new FurniClickedEventArgs(entity, _furniObject));
            room.Cycle.Schedule(() =>
            {
                entity.WhisperTo(entity, "And here is another chat, 2 room cycles (1 second) later!");
            }, 2);
        }
    }
}
