using ConsoleDnD.Extensions.ConsoleShell.Interfaces;
using ConsoleDnD.Framework.Interfaces;
using ConsoleDnD.Framework.Common;
using ConsoleDnD.Plugin.Dnd;
using System.Security.Cryptography.X509Certificates;
using ConsoleDnD.Plugin.Dnd.Core;

namespace ConsoleDnD.Extensions.ConsoleShell.PluginExtensions.Dnd
{
    class DnDConsoledPlugin : BaseConsoledPlugin
    {
        private DnDPlugin _plugin;
        private Dictionary<MenuItem, ICollection<object>> CollectionDictionary = new Dictionary<MenuItem, ICollection<object>>();
        public override IPlugin Plugin { get { return _plugin; } }
        public DnDConsoledPlugin(ISaveLoader loader)
        {
            _plugin = new DnDPlugin(loader);

        }
        public override void HandleConsole()
        {
            ItemsMenu();
        }

        private void ItemsMenu()
        {
            while (true)
            {
                Console.WriteLine("Доступные меню");
                var enumValuesInObjectsAsEnumerable = EnumUtils.EnumAsEnumerableObject(typeof(MenuItem));
                MenuItem? item = (MenuItem?)MessagePrinter.SelectPaged(enumValuesInObjectsAsEnumerable);

                if (item is null) { return; }
                switch (item)
                {
                    case MenuItem.ItemDictionary:
                        _plugin.ItemGlossary = MessagePrinter.ChangeCollection(_plugin.ItemGlossary).Cast<ItemDescription>().ToList();
                        break;

                    case MenuItem.Characters:
                        _plugin.Characters = MessagePrinter.ChangeCollection(_plugin.Characters).Cast<DnDCharacter>().ToList();
                        break;
                }
            }
        }
        private enum MenuItem
        {
            ItemDictionary,
            Characters,
        }
    }
}
