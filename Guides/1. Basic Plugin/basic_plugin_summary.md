### Summary

To get started, create a new C# library project and add Sirius.Api as package reference.

To create a new plugin, you must implement the following two interfaces:

#### IPluginDefinition
This holds information about your plugin, author, version etc.
It also defines which class is used as a startup class.
Additional services for dependency injection can be registered in the `ConfigureServices` method.

#### IPlugin
This class is the entry point of your plugin, see it as the `Main()` method for a regular C# project.
As the class is resolved through dependency injection, you can inject additional services via the constructor.
