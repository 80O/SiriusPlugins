using System;
using Sirius.Api.Plugin;

namespace AvatarPoof
{
    public class AvatarPoofDefinition : IPluginDefinition
    {
        public Type PluginClass() => typeof(AvatarPoofPlugin);

        public string Name => "Avatar Poof";
        public string Description => "Shows a cloud when changing looks";
        public Version Version => new(1, 0, 0);
    }
}