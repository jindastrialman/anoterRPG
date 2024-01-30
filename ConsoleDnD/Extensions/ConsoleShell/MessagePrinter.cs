using ConsoleDnD.Framework.Common;
using ConsoleDnD.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDnD.Extensions.ConsoleShell
{
    static class MessagePrinter
    {
        public static void Print(string message)
        {
            Console.WriteLine(message);
        }
        public static IEnumerable<object> ChangeCollection(IEnumerable<object> objects)
        {
            while (true)
            {
                Console.WriteLine("Меню выбора Действия");

                var enumValuesInObjectsAsEnumerable = EnumUtils.EnumAsEnumerableObject(typeof(MenuAction));
                MenuAction? action = (MenuAction?)SelectPaged(enumValuesInObjectsAsEnumerable);

                if (action is null)
                {
                    return objects;
                }

                switch (action)
                {
                    case MenuAction.ModifyAndRead:

                        break;
                    case MenuAction.Create:
                        objects = CreateNew(objects);
                        break;
                    case MenuAction.Delete:

                        break;
                }
            }

        }
        private static IEnumerable<object> CreateNew(IEnumerable<object> objects)
        {
            var objType = objects.FirstOrDefault().GetType();

            Console.WriteLine(objType);

            return objects;
        }

        private enum MenuAction
        {
            ModifyAndRead,
            Create,
            Delete
        }
        public static object SelectPaged(IEnumerable<object> collection)
        {
            int selectedPage = 0, pagesCount = collection.Count() / ConsoleOptions.PageSize;
            while (true)
            {
                var tmpCollection = collection.Skip(selectedPage * ConsoleOptions.PageSize).Take(ConsoleOptions.PageSize);

                Print($"Страница {selectedPage + 1}");

                for (int i = 1; i <= ConsoleOptions.PageSize && i <= tmpCollection.Count(); i++)
                {
                    Print($"{i} - {tmpCollection.Skip(i-1).First()}");
                }

                //добавить фильтр надо

                if (selectedPage > 0)
                {
                    Print("< - для предыдущей страницы");
                }
                if (selectedPage < pagesCount)
                {
                    Print("> - для следующей страницы");
                }

                Print("0 - выход из меню выбора");

                var consoleInput = Console.ReadLine();

                if (int.TryParse(consoleInput, out int result))
                {
                    if (result == 0)
                    {
                        return null;
                    }
                    if (result < ConsoleOptions.PageSize)
                    {
                        return tmpCollection.Skip(result - 1).First();
                    }
                }

                if (consoleInput == "<" && selectedPage > 0) { selectedPage--; }

                if (consoleInput == ">" && selectedPage < pagesCount) { selectedPage++; }
            }
        }
    }
}
