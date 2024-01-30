using ConsoleDnD.Framework.Interfaces;
using ConsoleDnD.Framework.Common;

namespace ConsoleDnD.Plugin.Dnd.Core
{
    class DnDCharacter : ICharacter
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public long HitPoints { get; set; } = 0;
        public Inventory Inventory { get; } = new Inventory();
    }
}
