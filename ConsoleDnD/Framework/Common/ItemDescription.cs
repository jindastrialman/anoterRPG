namespace ConsoleDnD.Framework.Common
{
    class ItemDescription
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Weight { get; init; }
        public ICollection<string> ItemTags { get; init; } = new List<string>();

        public override string ToString() => $"{Name}";
    }
}
