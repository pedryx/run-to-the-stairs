using GameLib.Managers.IO;

using SkiaSharp;


namespace RunToTheStairs.WPF.Managers
{
    public class TextureManager : IOManager<SKBitmap>
    {
        public TextureManager(string defaultPath = "Content/Entities", SKBitmap errorItem = null) 
            : base(defaultPath, errorItem) { }

        public override SKBitmap Load(string name, string file)
            => SKBitmap.Decode(file);
    }
}
