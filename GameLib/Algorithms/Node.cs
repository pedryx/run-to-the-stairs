using System.Collections.Generic;


namespace GameLib.Algorithms
{
    /// <summary>
    /// Represent a node of a directed graph.
    /// </summary>
    class Node<T>
    {
        public List<Edge<T>> Edges { get; set; } = new();

        public T Object { get; set; }
    }
}
