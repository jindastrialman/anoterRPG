using ConsoleDnD.Framework.Common;

namespace ConsoleDnD.Framework.Interfaces
{
    interface IPlugin
    {
        public string Name { get; }
        public string Description { get; }
        public ICollection<ItemDescription> ItemGlossary { get; set; }
    }
}
