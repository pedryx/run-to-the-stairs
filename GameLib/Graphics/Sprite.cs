﻿namespace GameLib
{
    /// <summary>
    /// Represent a sprite.
    /// </summary>
    public class Sprite
    {
        public string Name { get; set; }

        public Transform Transform { get; set; } = new Transform();

        public Rectangle? Clip { get; set; }
    }
}