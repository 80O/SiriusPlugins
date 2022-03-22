using System;
using Sirius.Api.Plugin;

namespace SuperWireds
{
    public class SuperWiredsPluginDefinition : IPluginDefinition
    {
        public Type PluginClass() => typeof(SuperWiredsPlugin);

        public string Name => "Super Wireds Plugin";
        public string Description => "Additional wired types.";
        public string Author => "The General";
        public Version Version => new(1, 0, 0);
    }
}