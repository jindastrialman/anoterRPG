using ConsoleDnD.Extensions.ConsoleShell.Interfaces;
using ConsoleDnD.Framework.Interfaces;

namespace ConsoleDnD.Extensions.ConsoleShell
{
    abstract class BaseConsoledPlugin : IConsoledPlugin
    {
        public abstract IPlugin Plugin { get; }

        public abstract void HandleConsole();

        public override string ToString()
        {
            return Plugin.Name;
        }
    }
}
