using Priority_Queue;

using System;
using System.Collections.Generic;


namespace GameLib.Algorithms
{
    /// <summary>
    /// Represent a class for graph-releated alghoritms.
    /// </summary>
    /// <typeparam name="T">Type of object used as nodes.</typeparam>
    public class Graph<T>
    {
        private Dictionary<T, Node<T>> nodes_ = new();
        private Dictionary<Node<T>, Node<T>> cameFrom_;

        /// <summary>
        /// After setting new goal you must recalculate dijkstra map by calling
        /// <see cref="Dijkstra"/>. Goal must be part of the graph.
        /// </summary>
        public Node<T> Goal { get; set; }

        /// <summary>
        /// Add node to the graph.
        /// </summary>
        /// <param name="obj">Object which represent node.</param>
        public void AddNode(T obj)
        {
            Node<T> node = new Node<T>();
            node.Object = obj;
            nodes_.Add(obj, node);
        }

        /// <summary>
        /// Calculate dijkstra map.
        /// </summary>
        public void Dijkstra()
        {
            if (Goal == null)
                throw new NullReferenceException($"{nameof(Goal)} cannot be null!");
            if (!nodes_.ContainsValue(Goal))
                throw new Exception($"{nameof(Goal)} must be part of the graph!");

            var frontier = new SimplePriorityQueue<Node<T>>();
            var cost = new Dictionary<Node<T>, float>();
            cameFrom_ = new Dictionary<Node<T>, Node<T>>();

            frontier.Enqueue(Goal, 0);
            cost[Goal] = 0;
            cameFrom_[Goal] = null;

            while (frontier.Count != 0)
            {
                Node<T> current = frontier.Dequeue();

                foreach (var edge in current.Edges)
                {
                    float newCost = cost[current] + edge.Value;
                    if (!cost.ContainsKey(edge.Node) || newCost < cost[edge.Node])
                    {
                        cost[edge.Node] = newCost;
                        cameFrom_[edge.Node] = current;
                        frontier.Enqueue(edge.Node, newCost);
                    }
                }
            }
        }

        /// <summary>
        /// Get path from some node to the goal.
        /// </summary>
        /// <param name="obj">Object which represents a node.</param>
        public IList<T> GetPath(T obj)
        {
            if (cameFrom_ == null)
                throw new Exception($"You must call {nameof(Dijkstra)} first!");

            var path = new List<T>();
            Node<T> current = nodes_[obj]; ;

            while (current != null)
            {
                path.Add(current.Object);
                current = cameFrom_[current];
            }

            return path;
        }
    }
}
