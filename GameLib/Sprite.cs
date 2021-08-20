namespace GameLib
{
    /// <summary>
    /// Represent a sprite.
    /// </summary>
    public class Sprite
    {
        public string Name { get; private set; }

        public Transform Transform { get; set; }

        public Sprite(string name)
        {
            Name = name;
        }
    }
}
