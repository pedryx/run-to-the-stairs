namespace GameLib
{
    public interface IScript
    {
        string Name { get; }

        void Invoke();
    }
}
