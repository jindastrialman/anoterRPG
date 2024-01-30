using ConsoleDnD.Framework.Common;
using ConsoleDnD.Framework.Interfaces;

namespace ConsoleDnD.Framework.Common
{
    class BasePlugin : IPlugin
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public ICollection<ItemDescription> ItemGlossary { get; set; }

    }
}
