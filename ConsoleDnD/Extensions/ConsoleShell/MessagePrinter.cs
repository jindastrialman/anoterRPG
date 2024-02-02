using ConsoleDnD.Framework.Common;
using Microsoft.VisualBasic.FileIO;
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
                        objects = ModifyAndRead(objects);
                        break;
                    case MenuAction.Create:
                        objects = CreateNew(objects);
                        break;
                    case MenuAction.Delete:
                        objects = Delete(objects);
                        break;
                }
            }

        }
        private static IEnumerable<T> ModifyAndRead<T>(IEnumerable<T> objects)
        {

            while (true)
            {
                var selectedToDelete = (T)SelectPaged(objects.Cast<object>());
                if (selectedToDelete is null) return objects;

                objects = objects.Where(x => !x.Equals(selectedToDelete));
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
                string inputValue;

                if (field.PropertyType.Name == typeof(ICollection<>).Name)
                {
                    Console.WriteLine($"Введите пустую строку, чтобы закончить добавление новых элементов");

                    do
                    {
                        inputValue = Console.ReadLine();
                        var methodInfo = field.PropertyType.GetMethods().Where(x => x.Name == "Add").Single();
                        methodInfo.Invoke(field.GetValue(newObject), new object[1] { ConvertConsoleInputToType(inputValue, field.PropertyType.GenericTypeArguments.First()) });
                    }
                    while (inputValue != "");

                    continue;
                }

                inputValue = Console.ReadLine();
                try
                {
                    field.SetValue(newObject, ConvertConsoleInputToType(inputValue, field.PropertyType));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("что-то сломалось, надо заново всё делать");
                    return objects;
                }
            }

            return objects.Append((T)newObject);
        }

        private static object? ConvertConsoleInputToType(string inputString, Type type)
        {
            if (type == typeof(string))
            {
                return inputString;
            }

            if (type.IsValueType)
            {
                return type.GetMethods()
                       .Where(x => x.Name == "Parse" && x.GetParameters().Length == 1)
                       .Single().Invoke(null, new object[1] { inputString });
            }

            return null;
        }
        private static IEnumerable<T> Delete<T>(IEnumerable<T> objects)
        {
            while (true)
            {
                var selectedToDelete = (T)SelectPaged(objects.Cast<object>());
                if (selectedToDelete is null) return objects;

                objects = objects.Where(x => !x.Equals(selectedToDelete));
            }
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
