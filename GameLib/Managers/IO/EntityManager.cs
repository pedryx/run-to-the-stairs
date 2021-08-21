using System.IO;
using System.Xml.Serialization;


namespace GameLib.Managers.IO
{
    public class EntityManager : IOManager<Entity>
    {
        public EntityManager(string defaultPath = "Content/Entities", Entity errorItem = null)
            : base(defaultPath, errorItem) { }

        public override Entity Load(string name, string file)
        {
            var serializer = new XmlSerializer(typeof(Entity));
            Entity entity;

            using (var reader = new StreamReader(file))
                entity = (Entity)serializer.Deserialize(reader);

            return entity;
        }

        public override void Save(Entity entity, string file)
        {
            var serializer = new XmlSerializer(typeof(Entity));

            using (var writter = new StreamWriter(file))
                serializer.Serialize(writter, entity);
        }
    }
}
