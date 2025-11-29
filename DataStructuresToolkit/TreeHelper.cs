using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Helper methods common to tree structures (BST, AVL, etc.).
    /// </summary>
    /// <remarks>
    /// Uses the edges convention for height: empty = -1, leaf = 0.
    /// </remarks>
    public static class TreeHelper
    {
        // Metrics

        /// <summary>
        /// Computes height using the edges.
        /// </summary>
        public static int Height(Node root)
        {
            if (root == null) return -1;
            int lh = Height(root.Left);
            int rh = Height(root.Right);
            return 1 + Math.Max(lh, rh);
        }

        /// <summary>
        /// Computes depth of first node whose value
        /// matches <paramref name="target"/>. Returns -1 if not found.
        /// </summary>
        public static int Depth(Node root, int target)
        {
            return DepthDfs(root, target, 0);
        }

        private static int DepthDfs(Node node, int target, int depth)
        {
            if (node == null) return -1;
            if (node.Value == target) return depth;

            int left = DepthDfs(node.Left, target, depth + 1);
            if (left >= 0) return left;

            return DepthDfs(node.Right, target, depth + 1);
        }

        // Traversals

        /// <summary>Inorder traversal (Left, Root, Right).</summary>
        public static IEnumerable<int> Inorder(Node root)
        {
            if (root == null) yield break;
            foreach (var v in Inorder(root.Left)) yield return v;
            yield return root.Value;
            foreach (var v in Inorder(root.Right)) yield return v;
        }

        /// <summary>Preorder traversal (Root, Left, Right).</summary>
        public static IEnumerable<int> Preorder(Node root)
        {
            if (root == null) yield break;
            yield return root.Value;
            foreach (var v in Preorder(root.Left)) yield return v;
            foreach (var v in Preorder(root.Right)) yield return v;
        }

        /// <summary>Postorder traversal (Left, Right, Root).</summary>
        public static IEnumerable<int> Postorder(Node root)
        {
            if (root == null) yield break;
            foreach (var v in Postorder(root.Left)) yield return v;
            foreach (var v in Postorder(root.Right)) yield return v;
            yield return root.Value;
        }

        // Rotations (for AVL / self-balancing trees)

        /// <summary>
        /// Performs a single left rotation around <paramref name="root"/>.
        /// Returns the new root of this subtree.
        /// </summary>
        public static Node RotateLeft(Node root)
        {
            if (root == null) return null;
            Node newRoot = root.Right;
            if (newRoot == null) return root; // nothing to rotate

            root.Right = newRoot.Left;
            newRoot.Left = root;
            return newRoot;
        }

        /// <summary>
        /// Performs a single right rotation around <paramref name="root"/>.
        /// Returns new root of this subtree.
        /// </summary>
        public static Node RotateRight(Node root)
        {
            if (root == null) return null;
            Node newRoot = root.Left;
            if (newRoot == null) return root;

            root.Left = newRoot.Right;
            newRoot.Right = root;
            return newRoot;
        }

        // AVL helpers

        /// <summary>
        /// Computes AVL-style balance factor for a node:
        /// height(left) - height(right), using edges convention.
        /// </summary>
        /// <param name="root">The node whose balance factor being computed.</param>
        /// <returns>
        /// An integer in range [-∞, +∞], where values &gt; 1 indicate left-heavy,
        /// values &lt; -1 indicate right-heavy, and -1..1 is considered balanced.
        /// </returns>
        public static int BalFactor(Node root)
        {
            if (root == null) return 0;
            int lh = Height(root.Left);
            int rh = Height(root.Right);
            return lh - rh;
        }
    }
}
