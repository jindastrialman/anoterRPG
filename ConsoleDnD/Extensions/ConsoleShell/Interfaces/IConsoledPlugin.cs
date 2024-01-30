using ConsoleDnD.Framework.Interfaces;

namespace ConsoleDnD.Extensions.ConsoleShell.Interfaces
{
    interface IConsoledPlugin
    {
        public IPlugin Plugin { get; }

        public void HandleConsole();

    }
}
