namespace GameLib
{
    /// <summary>
    /// Represent handler for component releated events.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void ComponentEventHandler(object sender, ComponentEventArgs e);

    /// <summary>
    /// Represent an arguments for component releated events.
    /// </summary>
    public class ComponentEventArgs
    {
        public IComponent Component { get; private set; }

        public Entity Entity { get; set; }

        public ComponentEventArgs(IComponent component, Entity entity)
        {
            Component = component;
            Entity = entity;
        }
    }
}
