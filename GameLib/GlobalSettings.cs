using GameLib.Graphics;
using GameLib.Math;

using System.Numerics;
using System.Xml.Serialization;


namespace GameLib
{
    public static class GlobalSettings
    {
        public static float GameSpeed { get; set; } = 1;

        public static Vector2 Resolution { get; set; } = new Vector2(1920, 1080);

        [XmlIgnore]
        internal static IMathProvider MathProvider { get; set; }

        [XmlIgnore]
        internal static ITextureInfoProvider TextureInfoProvider { get; set; }
    }
}
