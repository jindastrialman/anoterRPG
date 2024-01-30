using ConsoleDnD.Framework.Interfaces;
using ConsoleDnD.Framework.Common;
using ConsoleDnD.Plugin.Dnd.Core;

namespace ConsoleDnD.Plugin.Dnd
{
    class DnDPlugin : BasePlugin
    {
        public ICollection<DnDCharacter> Characters { get; set; }

        protected ISaveLoader SaveLoader;
        public DnDPlugin(ISaveLoader loader)
        {
            SaveLoader = loader;
            Name = "Dungeons and Dragons";
            Description = "aaaaaaaaaa";

            ItemGlossary = (List<ItemDescription>)SaveLoader.Load(typeof(List<ItemDescription>));
            Characters = (List<DnDCharacter>)SaveLoader.Load(typeof(List<DnDCharacter>));
        }
        public void SaveChanges()
        {
            SaveLoader.Save(ItemGlossary);
            SaveLoader.Save(Characters);
        }
        public void DiscardChanges()
        {
            ItemGlossary = (List<ItemDescription>)SaveLoader.Load(typeof(List<ItemDescription>));
            Characters = (List<DnDCharacter>)SaveLoader.Load(typeof(List<DnDCharacter>));
        }
    }
}
