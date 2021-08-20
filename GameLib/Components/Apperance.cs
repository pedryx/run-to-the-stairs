using System.Collections.Generic;


namespace GameLib.Components
{
    /// <summary>
    /// Represent an apperance of an entitiy.
    /// </summary>
    public class Apperance : IComponent
    {
        public List<Sprite> Sprites { get; set; }
    }
}
