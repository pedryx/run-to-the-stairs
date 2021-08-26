using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace GameLib
{
    /// <summary>
    /// Represent a finder of types.
    /// </summary>
    public static class TypeFinder
    {
        private static readonly List<Assembly> registeredAssemblies_ = new();

        private static Dictionary<string, Type> componentTypes_;

        /// <summary>
        /// All types of components.
        /// </summary>
        public static IReadOnlyDictionary<string, Type> ComponentTypes => componentTypes_;

        static TypeFinder()
        {
            registeredAssemblies_.Add(typeof(TypeFinder).Assembly);
        }

        /// <summary>
        /// Register assembly for type finder.
        /// </summary>
        public static void RegisterAssembly(Assembly assembly)
            => registeredAssemblies_.Add(assembly);

        /// <summary>
        /// Search for types in registered assemblies.
        /// </summary>
        public static void Search()
        {
            componentTypes_ = new();

            foreach (var assembly in registeredAssemblies_)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (type.GetInterfaces().Contains(typeof(IComponent)))
                        componentTypes_.Add(type.Name.ToLower(), type);
                }
            }
        }
    }
}
