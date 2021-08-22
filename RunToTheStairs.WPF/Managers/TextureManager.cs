using GameLib.Graphics;
using GameLib.Managers.IO;

using SkiaSharp;

using System.Numerics;

namespace RunToTheStairs.WPF.Managers
{
    public class TextureManager : IOManager<SKBitmap>, ITextureInfoProvider
    {
        private const string errorTextureName = "error";

        public TextureManager(string defaultPath = "Content/Textures") 
            : base(defaultPath, null) { }

        protected override void LoadedAll()
        {
            ErrorItem = this[errorTextureName];
        }

        public override SKBitmap Load(string name, string file)
            => SKBitmap.Decode(file);

        public Vector2 GetSize(string name)
        {
            SKBitmap bitmap = this[name];

            return new Vector2(bitmap.Width, bitmap.Height);
        }
    }
}
