using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacterGenerator.Collections
{
    /// <summary>
    /// A directed acyclic graph that stores a single, unique element at each vertex, 
    /// no values on edges and whose enumeration returns a depth-first walk of the graph.
    /// Once assigned, values at each vertex are immutable.
    /// </summary>
    /// <remarks>
    /// This is hardly a brilliant graph implementation but is is adequate for this 
    /// application.
    /// </remarks>
    public class Graph<T> : IEnumerable<T>
        where T : class, IEquatable<T>
    {
        GraphNode<T> root;
        Dictionary<T, GraphNode<T>> elements;

        /// <summary>
        /// Create a new <see cref="Graph{T}"/>.
        /// </summary>
        /// <param name="rootData">
        /// The root node of the graph. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rootData"/> cannot be null.
        /// </exception>
        public Graph(T rootData)
        {
            if (rootData == null)
            {
                throw new ArgumentException("root");
            }

            this.root = new GraphNode<T>(rootData);

            elements = new Dictionary<T, GraphNode<T>>();
            elements.Add(rootData, this.root);
        }

        /// <summary>
        /// The root element.
        /// </summary>
        public T Root
        {
            get
            {
                return root.Data;
            }
        }

        /// <summary>
        /// Add a new node to the graph with the value <paramref name="child"/>
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void AddChild(T parent, T child)
        {
            if (parent == null)
            {
                throw new ArgumentException("parent");
            }
            if (child == null)
            {
                throw new ArgumentException("data");
            }
            //if(Contains(child))
            //{
            //    throw new ArgumentException("Already contains child", "child");
            //}

            GraphNode<T> parentGraphNode;
            GraphNode<T> childGraphNode;

            if (!elements.TryGetValue(parent, out parentGraphNode))
            {
                throw new ArgumentException("parent not in graph", "parent");
            }

            try
            {
                childGraphNode = new GraphNode<T>(child);
                parentGraphNode.AddChild(childGraphNode);
            }
            catch(Exception)
            {
                if (elements.Keys.Contains(child))
                {
                    elements.Remove(child);
                }
                if (parentGraphNode.Children.Contains(childGraphNode))
                {
                    // Remove the new child
                }

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public bool IsChild(T parent, T child)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Contains(T data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return a depth-first enumeration over the graph.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return root.Select(graphNode => graphNode.Data).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return root.Select(graphNode => graphNode.Data).GetEnumerator();
        }
    }
}
