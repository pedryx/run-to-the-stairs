using System.Collections.Generic;
using System.Xml.Serialization;


namespace GameLib.Components
{
    /// <summary>
    /// Represent an apperance of an entitiy.
    /// </summary>
    public class Apperance : IComponent
    {
        [XmlArrayItem("Sprite")]
        public List<SpriteDesc> Sprites { get; set; }
    }
}
