namespace ConsoleDnD.Framework.Interfaces
{
    interface ISaveLoader
    {
        public void Save(object thingToSave);
        public object Load(Type type);
    }
}
