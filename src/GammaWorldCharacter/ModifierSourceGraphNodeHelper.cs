using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Collections;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Helper methods for dealing with a graph (<see cref="GraphNode{ModifierSource}"/>).
    /// </summary>
    public static class ModifierSourceGraphNodeHelper
    {
        /// <summary>
        /// Construct a depdenency graph of the <see cref="Character"/>'s <see cref="ModifierSource"/>s.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to generate the dependency graph for. This cannot be null.
        /// </param>
        /// <param name="modifierSources">
        /// The already extracted list of <see cref="ModifierSource"/>s to use. This cannot be null.
        /// </param>
        /// <returns>
        /// A <see cref="GraphNode{ModifierSource}"/> that is the root node of a dependency graph. The root
        /// node can be ignored and is not part of the graph itself.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        internal static GraphNode<ModifierSource> ConstructDependencyGraph(Character character, IList<ModifierSource> modifierSources)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (modifierSources == null)
            {
                throw new ArgumentNullException("modifierSources");
            }

            GraphNode<ModifierSource> root;
            IEqualityComparer<ModifierSource> comparer;
            Dictionary<ModifierSource, GraphNode<ModifierSource>> graphNodeLookup;

            // Rather than use "EqualityComparer<ModifierSource>.Default", use identity equality.
            // This allows two instances of the same modifier source with the same name to be used
            // within the graph. This can occur frequently with powers.
            comparer = new IdentityEqualityComparer<ModifierSource>();

            // Optimization for GraphNode.Find().
            graphNodeLookup = new Dictionary<ModifierSource, GraphNode<ModifierSource>>();

            // Construct a dependency graph.
            root = new GraphNode<ModifierSource>(new NullModifierSource(), comparer);
            foreach (ModifierSource modifierSource in modifierSources)
            {
                List<KeyValuePair<ModifierSource, ModifierSource>> dependencies;
                GraphNode<ModifierSource> parentGraphNode;
                GraphNode<ModifierSource> childGraphNode;

                // Get the dependencies
                dependencies = modifierSource.GetDependencies(character);

                // For each dependency, find the source ModifierSource and add
                // the destination under it.
                foreach (KeyValuePair<ModifierSource, ModifierSource> dependency in dependencies)
                {
                    // Find the parent of the dependency
                    if (!graphNodeLookup.TryGetValue(dependency.Key, out parentGraphNode))
                    {
                        parentGraphNode = new GraphNode<ModifierSource>(dependency.Key, comparer);
                        root.AddChild(parentGraphNode);
                        graphNodeLookup[dependency.Key] = parentGraphNode;
                    }

                    // Find the child of the dependency
                    if (!graphNodeLookup.TryGetValue(dependency.Value, out childGraphNode))
                    {
                        childGraphNode = new GraphNode<ModifierSource>(dependency.Value, comparer);
                        graphNodeLookup[dependency.Value] = childGraphNode;
                    }

                    // Add the child to the parent
                    parentGraphNode.AddChild(childGraphNode);
                }
            }

            return root;
        }
    }
}
