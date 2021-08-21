using GameLib.Managers.IO;

using SkiaSharp;


namespace RunToTheStairs.WPF.Managers
{
    public class TextureManager : IOManager<SKBitmap>
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
    }
}
