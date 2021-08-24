using System.IO;
using System.Xml.Serialization;

//todo: change format from xml to xaml
namespace GameLib.Managers.IO
{
    public class EntityManager : IOManager<Entity>
    {
        private readonly Game game_;

        public EntityManager(Game game, string defaultPath = "Content/Entities",
            Entity errorItem = null)
            : base(defaultPath, errorItem) 
        {
            game_ = game;
        }

        protected override void LoadedAll()
        {
            foreach (var entity in Items.Values)
            {
                entity.Update();
            }
        }

        public override Entity Load(string name, string file)
        {
            var serializer = new XmlSerializer(typeof(Entity));
            Entity entity;

            using (var reader = new StreamReader(file))
                entity = (Entity)serializer.Deserialize(reader);

            if (entity.AddToPool)
                game_.Pool.Add(name, entity);

            return entity;
        }

        public override void Save(Entity entity, string file)
        {
            var serializer = new XmlSerializer(typeof(Entity));

            using var writter = new StreamWriter(file);
            serializer.Serialize(writter, entity);
        }
    }
}
