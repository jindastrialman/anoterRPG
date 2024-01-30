namespace ConsoleDnD.Framework.Common
{
    class Inventory
    {
        public ICollection<Item> Items{get;} = new List<Item>();
    }
}
