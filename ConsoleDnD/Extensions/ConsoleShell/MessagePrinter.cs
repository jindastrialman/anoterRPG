using ConsoleDnD.Framework.Common;
using System.Reflection;

namespace ConsoleDnD.Extensions.ConsoleShell
{
    static class MessagePrinter
    {
        public static void Print(string message)
        {
            Console.WriteLine(message);
        }
        public static IEnumerable<T> ChangeCollection<T>(IEnumerable<T> objects)
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
        private static IEnumerable<T> CreateNew<T>(IEnumerable<T> objects)
        {
            var objType = typeof(T);

            ConstructorInfo ctor = objType.GetConstructor(new Type[0]);
            var newObject = ctor.Invoke(null);

            foreach (var field in objType.GetProperties())
            {
                Console.WriteLine($"заполните поле {field.Name} типа {field.PropertyType.Name}");
                if (field.PropertyType == typeof(string))
                {
                    var loadedValue = Console.ReadLine();
                    field.SetValue(newObject, loadedValue);
                    continue;
                }

                if (field.PropertyType.IsValueType)
                {
                    var loadedValue = Console.ReadLine();
                    var fieldType = field.PropertyType;
                    try
                    {
                        //this one is trying to call parse for ints, doubles or other types (definetely will break if it is value type without parse)
                        var converted = fieldType.GetMethods()
                                                 .Where(x => x.Name == "Parse" && x.GetParameters().Length == 1)
                                                 .Single().Invoke(null, new object[1] { loadedValue });

                        field.SetValue(newObject, converted);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    continue;
                }
                if (field.PropertyType.GetInterfaces().Any(i => i.Name == typeof(ICollection<>).Name))
                {
                    Console.WriteLine("ещё не работает");
                }
                Console.WriteLine("что-то странное, непонятное");
            }

            Console.WriteLine(objType);

            return objects.Append((T)newObject);
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
