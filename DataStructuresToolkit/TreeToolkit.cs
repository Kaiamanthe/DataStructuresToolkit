using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{

    /// <summary>
    /// A minimal integer Binary Search Tree (BST) supporting insertion,
    /// membership testing, height measurement, and inorder enumeration.
    /// </summary>
    /// <remarks>
    /// Uses the shared <see cref="Node"/> type and <see cref="TreeHelper"/>
    /// for traversals and metrics.
    /// </remarks>
    public class TreeToolkit
    {

        /// <summary>
        /// Builds fixed teaching tree used in lecture examples:
        /// <code>
        ///          38
        ///        /    \
        ///      27      43
        ///     /  \
        ///    3    9
        /// </code>
        /// </summary>
        /// <returns>The root node of the constructed tree.</returns>
        /// <remarks>
        /// Returns a deterministic shape so traversal orders are predictable in tests.
        /// </remarks>
        public static Node BuildTeachingTree()
        {
            var n3 = new Node(3);
            var n9 = new Node(9);
            var n27 = new Node(27, n3, n9);
            var n43 = new Node(43);
            var n38 = new Node(38, n27, n43);
            return n38;
        }
        /// <summary>Root node BST (nullable).</summary>
        private Node _root;

        /// <summary>
        /// Gets a value indicating if BST has nodes.
        /// </summary>
        public bool IsEmpty => _root == null;

        /// <summary>
        /// Inserts a value into BST if it is not already present.
        /// </summary>
        /// <param name="value">The value to insert.</param>
        /// <returns>
        /// <c>true</c> if the value was inserted; <c>false</c> if it was a duplicate.
        /// </returns>
        /// <remarks>
        /// Expected <c>O(log n)</c> time on balanced trees worst-case <c>O(n)</c> when skewed.
        /// Extra space: <c>O(1)</c>.
        /// </remarks>
        public bool Insert(int value)
        {
            if (_root == null) { _root = new Node(value); return true; }
            Node cur = _root;
            while (true)
            {
                if (value == cur.Value) return false; // no duplicates
                if (value < cur.Value)
                {
                    if (cur.Left == null) { cur.Left = new Node(value); return true; }
                    cur = cur.Left;
                }
                else
                {
                    if (cur.Right == null) { cur.Right = new Node(value); return true; }
                    cur = cur.Right;
                }
            }
        }

        /// <summary>
        /// Determines whether specified value exists in BST.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns><c>true</c> if present otherwise <c>false</c>.</returns>
        /// <remarks>
        /// Expected <c>O(log n)</c> time on balanced trees; worst-case <c>O(n)</c> when skewed.
        /// Extra space: <c>O(1)</c>.
        /// </remarks>
        public bool Contains(int value)
        {
            Node cur = _root;
            while (cur != null)
            {
                if (value == cur.Value) return true;
                cur = value < cur.Value ? cur.Left : cur.Right;
            }
            return false;
        }

        /// <summary>
        /// Computes height of the BST using edges convention.
        /// </summary>
        /// <returns>
        /// <c>-1</c> if empty; <c>0</c> for a single node; otherwise the
        /// length (in edges) of the longest path from root to leaf.
        /// </returns>
        public int Height() => Height(_root);


        /// <summary>
        /// Recursively computes height of a subtree (edges convention).
        /// </summary>
        private static int Height(Node n) => TreeHelper.Height(n);


        /// <summary>
        /// Enumerates values of BST in sorted (inorder) order.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of values in ascending order.</returns>
        public IEnumerable<int> Inorder() => Inorder(_root);

        /// <summary>
        /// Recursively yields values from a subtree in inorder.
        /// </summary>
        /// <param name="n">Subtree root.</param>
        /// <returns>Inorder sequence of values for the subtree.</returns>
        private static IEnumerable<int> Inorder(Node n) => TreeHelper.Inorder(n);
    }
}
