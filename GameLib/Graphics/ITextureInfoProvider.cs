using System.Numerics;


namespace GameLib.Graphics
{
    public interface ITextureInfoProvider
    {
        Vector2 GetSize(string name);
    }
}
