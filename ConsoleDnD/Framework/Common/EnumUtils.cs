namespace ConsoleDnD.Framework.Common
{
    static class EnumUtils
    {
        public static IEnumerable<object> EnumAsEnumerableObject(Type enumType) => Enum.GetValues(enumType).Cast<object>();
    }
}
