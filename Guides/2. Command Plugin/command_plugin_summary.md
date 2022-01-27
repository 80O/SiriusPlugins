### Summary

To create commands, there are a couple different flavours depending on where the command is executed. Either in Sirius or on Gaia.

Commands are custom logic that is executed by the player (actor) via chat. An command is identified by the prefix `:` and an key, example `:ban <username>`.
Commands require a permission level configured in the database in the `ranks` table. The following permissions are available:

(PermissionLevel Name / Database representatation)
- None / 0: No permission to run this command.
- Allowed / 1: Permission to run this command.
- AsRoomOwner / 2: Permission to run this command, only if the actor is owner of the room.
- GroupMemberRequired / 3: Permission to run this command, only if the actor is part of the room group.
- RoomRightsRequired / 4: Permission to run this command, only if the actor has room rights.

Commands registered via plugins are automatically added to the database with the default permission set to `None / 0` for all ranks.
Keep in mind that there is only one instance per command created, instances will be used. Avoid storing data in instances of commands.

#### Sirius: `ICommand` & `ITargetCommand`
To create a command for Sirius you can use the `ICommand` interface or the `ITargetCommand` interface.
Your command must result in an `CommandResult`. Predefined `CommandResult`s are:

- `Ok`: Indicates that the command executed succesfully with no additional status.
- `Failed`: The command failed to be executed.
- `CommandNotFound`: Command was not found. This status is used internally when trying to run a command that doesnt exist. Re-using this status is allowed.
- `NoPermission`: Actor has no permission to run this command. This status is used internally when trying to run a command without the correct permissions. Re-using this status is allowed.
- `TargetNotFound`: Target `IHabbo` was not found. This status is used internally when trying to run a command for which there was no matching Habbo by name found. Re-using this status is allowed.

##### `ICommand`
This interface implements `Task<CommandResult> Handle(IHabbo actor, string[] parameters);`. The `actor` is the `IHabbo` that executed the command along with the parameters used, excluding the command keyword.

##### `ITargetCommand`
This interface is used if your command is supposed to target any IHabbo used as first parameter. For example `:ban <username>`.

##### `IRoomCommand`
Equivalent to `ICommand` except it is only ran on Gaia hosts.
Instead of receiving an `IHabbo` instance as the actor, you will receive the `UserEntity` as actor.
In addition to the `UserEntity`, the `IRoom` instance the actor is in is also passed for convenience.

##### `IRoomTargetCommand`
Equivalent to `ITargetCommand` except it is only ran on Gaia hosts.

#### Tips:
You can use dependency injection in the contructors of your commands. Though keep in mind that some services are created per room or other scopes.
