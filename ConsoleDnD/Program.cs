using ConsoleDnD.Extensions.ConsoleShell.PluginExtensions.Dnd;
using ConsoleDnD.Framework.Common.Importers;
using ConsoleDnD.Extensions.ConsoleShell;

var consoleInteractor = new ConsoleInteractor();

consoleInteractor.Start(new DnDConsoledPlugin(new JsonSaveLoader()));
    