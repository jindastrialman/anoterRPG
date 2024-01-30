using ConsoleDnD.Extensions.ConsoleShell.Interfaces;
using ConsoleDnD.Framework.Interfaces;

namespace ConsoleDnD.Extensions.ConsoleShell
{
    class ConsoleInteractor
    {
        private IConsoledPlugin currentPlugin;
        public ConsoleInteractor() { }

        public void Start(params IConsoledPlugin[] plugins)
        {
            while (true)
            {
                Console.WriteLine("Выберите плагин");
                currentPlugin = (IConsoledPlugin)MessagePrinter.SelectPaged(plugins);
                if (currentPlugin is null)
                {
                    Console.WriteLine("пакеда");
                    return;
                }
                currentPlugin.HandleConsole();
            }
        }
    }
}
