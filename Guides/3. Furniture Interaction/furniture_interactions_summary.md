### Furniture Interactions Summary
Furniture interactions are at the heart of building rooms. With furniture interactions you will be add in additional logic and make it more fun for your players to enjoy their rooms.

Examples of custom furniture interactions are vendingmachines, teleporters and wireds.

This plugin example shows you just the basics to get started.

#### Creating an interaction
To create a new furniture you need to implement the `IFurnitureInteractionBuilder` interface for your interaction.
This interface defines the name (`InteractionKey`) of your interaction and allows you to assign custom interaction behavior components to your furniture.

Furniture interactions are composed of 3 interaction behavior components and can each be implemented via their respective interface:
- Click Behavior: `IClickBehavior`
- Walk Behavior: `IWalkBehavior`
- Generic Actions Behavior: `IActionBehavior`

Note that some interfaces have a default implementation and are not mandatory to be implemented.
By not assigning any behavior, the default version is automatically assigned. (`DefaultClickBehavior`, `DefaultWalkBehavior`, `DefaultActionsBehavior`)
By dividing these behaviors, its relatively easy to re-use them for multiple interactions.

##### Clicking
Click actions can be handled via the `IClickBehavior` interface.

Clicks can occur via the following mechanisms:

- Player manually clicks a furniture.
- Wired Toggle Furniture
- Plugins or custom logic.

If you only want to do something simple, it is often simpler to extend `DefectClickBehavior`.

##### Walking
Walk actions and conditions are handled via the `IWalkBehavior` interface.

##### Actions
Different actions not covered by `IClickBehavior` and `IWalkBehavior`. 

- Placing / Picking furniture
- Rotating / Moving furniture
- Room loading

#### Assigning an interaction
You can assign furniture interactions via the database in the `items_base` table. Find the correct furniture and in the `interaction` column you can asign the key of your interaction.
