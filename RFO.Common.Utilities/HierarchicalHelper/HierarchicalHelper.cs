
using System;
using System.Collections.Generic;
using System.Linq;

namespace RFO.Common.Utilities.HierarchicalHelper
{
    /// <summary>
    /// A generic extension method that converts objects into hierarchy form
    /// </summary>
    public static class HierarchicalHelper
    {
        /// <summary>
        /// Converts objects into hierarchy form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <param name="elements">The elements.</param>
        /// <param name="topMostKey">The top most key.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="parentKeySelector">The parent key selector.</param>
        /// <param name="orderingKeySelector">The ordering key selector.</param>
        /// <returns></returns>
        public static IEnumerable<Node<T>> Hierarchize<T, TKey, TOrderKey>(
            this IEnumerable<T> elements,
            TKey topMostKey,
            Func<T, TKey> keySelector,
            Func<T, TKey> parentKeySelector,
            Func<T, TOrderKey> orderingKeySelector)
        {
            var families = elements.ToLookup(parentKeySelector);
            var childrenFetcher = default(Func<TKey, IEnumerable<Node<T>>>);
            childrenFetcher = parentId => families[parentId]
                .OrderBy(orderingKeySelector)
                .Select(x => new Node<T>(x, childrenFetcher(keySelector(x))));

            return childrenFetcher(topMostKey);
        }

        /// <summary>
        /// Dumps the hierarchical.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hierarchy">The hierarchy.</param>
        /// <returns></returns>
        public static IEnumerable<T> DumpHierarchical<T>(this IEnumerable<Node<T>> hierarchy)
        {
            var nodeValues = new List<T>();

            // The recursive function to dump hierarchical tree
            Action<Node<T>> dumpAction = null;
            dumpAction = node =>
            {
                nodeValues.Add(node.Value);
                foreach (var child in node.Children)
                {
                    dumpAction(child);
                }
            };

            // Dump hierarchical tree starting from first record
            var nodes = hierarchy as IList<Node<T>> ?? hierarchy.ToList();
            if (hierarchy != null)
            {
                // Loop all root nodes
                foreach (var node in nodes)
                {
                    dumpAction(node);
                }
            }

            return nodeValues;
        }
    }

    /// <summary>
    /// Present a hierarchical node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="children">The children.</param>
        public Node(T value, IEnumerable<Node<T>> children)
        {
            this.Value = value;
            this.Children = new List<Node<T>>(children);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; private set; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IList<Node<T>> Children { get; private set; }
    }
}