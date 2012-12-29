using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Collections
{
    /// <summary>
    /// A node or vertex in a directed acyclic graph whose enumeration returns a depth-first walk of the graph.
    /// The node contains a single piece of data that, once assigned, is immutable.
    /// </summary>
    /// <remarks>
    /// This is hardly a brilliant graph implementation. It assumes a given value of T only 
    /// only occurs once in the graph (defined by IEquatable{T}) without restricting that on
    /// insert (for performance).
    /// </remarks>
    public class GraphNode<T> : IEnumerable<GraphNode<T>>
        where T : IEquatable<T>
    {
        private T data;
        private List<GraphNode<T>> children;
        private List<GraphNode<T>> parents;

        /// <summary>
        /// Create a new <see cref="GraphNode{T}"/> using the default
        /// comparer for items of that type.
        /// </summary>
        /// <param name="data">
        /// Data for this node.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// data cannot be null.
        /// </exception>
        public GraphNode(T data)
            : this(data, EqualityComparer<T>.Default)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="GraphNode&lt;T&gt;"/>.
        /// </summary>
        /// <param name="data">
        /// Data for this node.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{T}"/> to use to see if two elements of type T
        /// are equal.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// data cannot be null.
        /// </exception>
        public GraphNode(T data, IEqualityComparer<T> comparer)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            Comparer = comparer;
            this.data = data;
            this.children = new List<GraphNode<T>>();
            this.parents = new List<GraphNode<T>>();
        }

        /// <summary>
        /// The comparer used to match <typeparamref name="T"/>.
        /// </summary>
        public IEqualityComparer<T> Comparer
        {
            get;
            private set;
        }

        /// <summary>
        /// Called by AddChild to add this node to the new child's list of parents.
        /// </summary>
        /// <param name="graphNode">
        /// The new parent.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// graph cannot be null.
        /// </exception>
        internal void AddParent(GraphNode<T> graphNode)
        {
            if (graphNode == null)
            {
                throw new ArgumentNullException("graphNode");
            }

            parents.Add(graphNode);
        }

        /// <summary>
        /// Add a child to this <see cref="GraphNode{T}"/>.
        /// </summary>
        /// <remarks>
        /// Adding a node with the same data as an existing child does nothing.
        /// This means that any additional parents or children must be transferred
        /// to the existing child.
        /// </remarks>
        /// <param name="graphNode">
        /// The new child.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// graph cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Adding a node with the given data would create a cycle.
        /// </exception>
        public GraphNode<T> AddChild(GraphNode<T> graphNode)
        {
            if (graphNode == null)
            {
                throw new ArgumentNullException("graphNode");
            }
            if (Equals(graphNode))
            {
                throw new ArgumentException("Cannot add self as child", "graphNode");
            }
            if (graphNode.IsDescedant(this))
            {
                throw new ArgumentException("Cyclic reference", "graphNode");
            }
            // TODO: Remove the if(!IsChild... below and replace it with this
            //if (!IsChild(graphNode))
            //{
            //    throw new ArgumentException("Already a child", "graphNode");
            //}

            if (!IsChild(graphNode))
            {
                graphNode.AddParent(this);
                children.Add(graphNode);
            }

            return graphNode;
        }

        /// <summary>
        /// The data at this node.
        /// </summary>
        public T Data
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// The node's children
        /// </summary>
        public IList<GraphNode<T>> Children
        {
            get
            {
                return children.AsReadOnly();
            }
        }

        /// <summary>
        /// Construct a depth first recursion of the graph.
        /// </summary>
        /// <returns>
        /// </returns>
        public IEnumerable<GraphNode<T>> GetDepthFirstEnumerator()
        {
            List<GraphNode<T>> result;

            result = new List<GraphNode<T>>();

            result.Add(this);

            foreach (GraphNode<T> child in children)
            {
                result.AddRange(child.GetDepthFirstEnumerator());
            }

            return result.AsEnumerable();
        }

        /// <summary>
        /// Is the given node a child of his node?
        /// </summary>
        /// <param name="graphNode">
        /// The <see cref="GraphNode{T}"/> to check.
        /// </param>
        /// <returns>
        /// True if a node with that data is a child, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="graphNode"/> cannot be null.
        /// </exception>
        public bool IsChild(GraphNode<T> graphNode)
        {
            if (graphNode == null)
            {
                throw new ArgumentNullException("graphNode");
            }

            return children.Exists(child => child.Equals(graphNode));
        }

        /// <summary>
        /// Is the given node a descandant of this node?
        /// </summary>
        /// <remarks>
        /// A descendant is a child node, a child of a child node and so on.
        /// A node is not a descendant of itself.
        /// </remarks>
        /// <param name="graphNode">
        /// The <see cref="GraphNode{T}"/> to check.
        /// </param>
        /// <returns>
        /// True if it is a descendant, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="graphNode"/> cannot be null.
        /// </exception>
        public bool IsDescedant(GraphNode<T> graphNode)
        {
            if (graphNode == null)
            {
                throw new ArgumentNullException("graphNode");
            }

            bool result;

            result = false;
            if (Equals(graphNode))
            {
                result = false;
            }
            else
            {
                foreach (GraphNode<T> childNode in Children)
                {
                    if (childNode.Equals(graphNode))
                    {
                        result = true;
                        break;
                    }
                    else if (childNode.IsDescedant(graphNode))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Is the given element a parent of this node?
        /// </summary>
        /// <param name="graphNode">
        /// The <see cref="GraphNode{T}"/> to test.
        /// </param>
        /// <returns>
        /// True if it is a parent of this node, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="graphNode"/> cannot be null.
        /// </exception>
        public bool IsParent(GraphNode<T> graphNode)
        {
            if (graphNode == null)
            {
                throw new ArgumentNullException("graphNode");
            }

            return parents.Exists(parent => parent.Equals(graphNode));
        }

        /// <summary>
        /// The node's parents.
        /// </summary>
        public IList<GraphNode<T>> Parents
        {
            get
            {
                return parents.AsReadOnly();
            }
        }

        /// <summary>
        /// Provide a human readable description.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString("B");
        }

        /// <summary>
        /// Provide a human readable description.
        /// </summary>
        /// <param name="format">
        /// "B" for bare format (just GraphNode: (data))
        /// "D" for deep format (as "B" but with children shown)
        /// </param>
        /// <returns>
        /// A human readable representation of the graph.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// format cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Unknown format option.
        /// </exception>
        public string ToString(string format)
        {
            string result;

            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            else if (format.Equals("B", StringComparison.InvariantCultureIgnoreCase))
            {
                result = "GraphNode: '" + Data.ToString() + "'";
            }
            else if (format.Equals("D", StringComparison.InvariantCultureIgnoreCase))
            {
                result = DeepToString(string.Empty);
            }
            else
            {
                throw new ArgumentException(string.Format("'{0}' unknown", format), "format");
            }

            return result;
        }

        /// <summary>
        /// Construct a string showing a depth first recursion of the graph.
        /// </summary>
        /// <remarks>
        /// Should we merge this with the depth first code above?
        /// </remarks>
        /// <param name="indent">
        /// The current indent.
        /// </param>
        /// <returns></returns>
        internal string DeepToString(string indent)
        {
            StringBuilder stringBuilder;
            string newIndent;

            newIndent = indent + "\t";

            stringBuilder = new StringBuilder();
            stringBuilder.Append(indent + ToString() + Environment.NewLine);
            foreach(GraphNode<T> child in Children)
            {
                stringBuilder.Append(child.DeepToString(newIndent));
            }
            
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<GraphNode<T>> GetEnumerator()
        {
            HashSet<GraphNode<T>> visited;
            List<GraphNode<T>> result;

            visited = new HashSet<GraphNode<T>>();
            result = new List<GraphNode<T>>();
            foreach (GraphNode<T> element in GetDepthFirstEnumerator())
            {
                Enumerate(element, visited, x => result.Add(x));
            }

            return result.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            HashSet<GraphNode<T>> visited;
            List<GraphNode<T>> result;

            visited = new HashSet<GraphNode<T>>();
            result = new List<GraphNode<T>>();
            foreach (GraphNode<T> element in GetDepthFirstEnumerator())
            {
                Enumerate(element, visited, x => result.Add(x));
            }

            return result.GetEnumerator();
        }

        /// <summary>
        /// If 'element' has not been visited, i.e. is a key in 'visited', visit each of its parents
        /// then call func passing element.Data to it then add element to visited. Do nothing if 
        /// element has already been visited.
        /// </summary>
        /// <param name="element">
        /// The element to visit.
        /// </param>
        /// <param name="visited">
        /// The list of visited nodes.
        /// </param>
        /// <param name="func">
        /// The function to call on each node.
        /// </param>
        private static void Enumerate(GraphNode<T> element, HashSet<GraphNode<T>> visited, Action<GraphNode<T>> func)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (visited == null)
            {
                throw new ArgumentNullException("visited");
            }
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            // Technically, if element has already been visited, all of its parents should also 
            // already have been visited.
            if (!visited.Contains(element))
            {
                foreach (GraphNode<T> parent in element.Parents)
                {
                    Enumerate(parent, visited, func);
                }

                func(element);

                visited.Add(element);
            }
        }
    }
}