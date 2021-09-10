namespace GameLib.Algorithms
{
    /// <summary>
    /// Represent an edge of a directed graph.
    /// </summary>
    class Edge<T>
    {
        /// <summary>
        /// Node at the end of the edge.
        /// </summary>
        public Node<T> Node { get; private set; }

        public float Value { get; set; }

        /// <param name="node">Node at the end of the edge.</param>
        public Edge(Node<T> node, float value)
        {
            Node = node;
            Value = value;
        }
    }
}
