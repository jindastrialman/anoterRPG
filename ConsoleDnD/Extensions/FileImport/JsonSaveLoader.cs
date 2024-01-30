using ConsoleDnD.Framework.Interfaces;
using Newtonsoft.Json;
using System.Reflection;

namespace ConsoleDnD.Framework.Common.Importers
{
    class JsonSaveLoader : ISaveLoader
    {
        public void Save(object thingToSave)
        {
            var saveObject = new SaveObject() {thing = thingToSave, type = thingToSave.GetType(), key = thingToSave.GetType().FullName };
            var json = JsonConvert.SerializeObject(saveObject);

            string path = $"{saveObject.key}";
            File.WriteAllText(path, json);
        }
        public object Load(Type type)
        {
            try
            {
                string path = $"{type.GetType().FullName}";
                string loadedJson = File.ReadAllText(path);

                var loaded = (SaveObject)JsonConvert.DeserializeObject(loadedJson, typeof(SaveObject));
                return loaded.thing;
            }
            catch
            {
                ConstructorInfo ctor = type.GetConstructor(new Type[0]);
                return ctor.Invoke(null);
            }
        }
        private class SaveObject
        {
            public object thing { get; set; }
            public string key { get; set; }
            public Type type { get; set; }
        }
    }
}
