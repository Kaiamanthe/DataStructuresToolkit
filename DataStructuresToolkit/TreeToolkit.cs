using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Node in a binary tree and provides static
    /// traversal and metric for general trees.
    /// </summary>
    /// <remarks>
    /// <para><b>Height convention:</b> edges convention — empty = -1, leaf = 0.</para>
    /// <para><b>Complexity (traversals):</b> Each traversal visits every node once
    /// for <c>O(n)</c> time and uses <c>O(h)</c> auxiliary space (recursion stack),
    /// where <c>n</c> is the number of nodes and <c>h</c> is the tree height.</para>
    /// </remarks>
    public class TreeNode
    {
        /// <summary>
        /// Gets or sets the value stored at this node.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        public TreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        public TreeNode Right { get; set; }

        /// <summary>
        /// Initializes a new <see cref="TreeNode"/> with an integer value
        /// and optional left/right children.
        /// </summary>
        /// <param name="value">The value to store in the node.</param>
        /// <param name="left">Optional left child.</param>
        /// <param name="right">Optional right child.</param>
        public TreeNode(int value, TreeNode left = null, TreeNode right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Builds the fixed teaching tree used in lecture examples:
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
        public static TreeNode BuildTeachingTree()
        {
            var n3 = new TreeNode(3);
            var n9 = new TreeNode(9);
            var n27 = new TreeNode(27, n3, n9);
            var n43 = new TreeNode(43);
            var n38 = new TreeNode(38, n27, n43);
            return n38;
        }

        /// <summary>
        /// Performs an inorder traversal (Left, Root, Right) of a binary tree.
        /// </summary>
        /// <param name="root">The root of the tree to traverse (may be <c>null</c>).</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of node values in inorder.</returns>
        /// <remarks>
        /// Time: <c>O(n)</c>.  Extra space: <c>O(h)</c> recursion stack.
        /// </remarks>
        public static IEnumerable<int> Inorder(TreeNode root)
        {
            if (root == null) yield break;
            foreach (var v in Inorder(root.Left)) yield return v;
            yield return root.Value;
            foreach (var v in Inorder(root.Right)) yield return v;
        }

        /// <summary>
        /// Performs a preorder traversal (Root, Left, Right) of a binary tree.
        /// </summary>
        /// <param name="root">The root of the tree to traverse (may be <c>null</c>).</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of node values in preorder.</returns>
        /// <remarks>
        /// Time: <c>O(n)</c>.  Extra space: <c>O(h)</c> recursion stack.
        /// </remarks>
        public static IEnumerable<int> Preorder(TreeNode root)
        {
            if (root == null) yield break;
            yield return root.Value;
            foreach (var v in Preorder(root.Left)) yield return v;
            foreach (var v in Preorder(root.Right)) yield return v;
        }

        /// <summary>
        /// Performs a postorder traversal (Left, Right, Root) of a binary tree.
        /// </summary>
        /// <param name="root">The root of the tree to traverse (may be <c>null</c>).</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of node values in postorder.</returns>
        /// <remarks>
        /// Time: <c>O(n)</c>.  Extra space: <c>O(h)</c> recursion stack.
        /// </remarks>
        public static IEnumerable<int> Postorder(TreeNode root)
        {
            if (root == null) yield break;
            foreach (var v in Postorder(root.Left)) yield return v;
            foreach (var v in Postorder(root.Right)) yield return v;
            yield return root.Value;
        }

        /// <summary>
        /// Computes the height of a tree using the edges convention.
        /// </summary>
        /// <param name="root">The root of the tree (may be <c>null</c>).</param>
        /// <returns>
        /// <c>-1</c> if the tree is empty; <c>0</c> for a leaf; otherwise the
        /// length (in edges) of the longest root-to-leaf path.
        /// </returns>
        /// <remarks>
        /// Time: <c>O(n)</c> (visits every node). Extra space: <c>O(h)</c>.
        /// </remarks>
        public static int Height(TreeNode root)
        {
            if (root == null) return -1;
            int lh = Height(root.Left);
            int rh = Height(root.Right);
            return 1 + Math.Max(lh, rh);
        }

        /// <summary>
        /// Returns the depth (in edges from the root) of the first node
        /// whose value equals <paramref name="target"/>.
        /// </summary>
        /// <param name="root">The root of the tree to search.</param>
        /// <param name="target">The value to locate.</param>
        /// <returns>
        /// The depth (0 for root) if found; otherwise <c>-1</c>.
        /// </returns>
        /// <remarks>
        /// Time: <c>O(n)</c> in a general tree without ordering.
        /// Extra space: <c>O(h)</c> recursion stack.
        /// </remarks>
        public static int Depth(TreeNode root, int target)
        {
            return DepthDfs(root, target, 0);
        }

        private static int DepthDfs(TreeNode node, int target, int d)
        {
            if (node == null) return -1;
            if (node.Value == target) return d;
            int left = DepthDfs(node.Left, target, d + 1);
            if (left >= 0) return left;
            return DepthDfs(node.Right, target, d + 1);
        }
    }

    /// <summary>
    /// A minimal integer Binary Search Tree (BST) supporting insertion,
    /// membership testing, height measurement, and inorder enumeration.
    /// </summary>
    /// <remarks>
    /// <para><b>Search/Insert complexity:</b> Expected <c>O(log n)</c> on balanced trees;
    /// worst-case <c>O(n)</c> for skewed shapes (e.g., sorted insert order).</para>
    /// <para><b>Height convention:</b> edges convention — empty = -1, leaf = 0.</para>
    /// </remarks>
    public class Bst
    {
        /// <summary>Internal BST node.</summary>
        private class Node
        {
            public int Value;
            public Node Left, Right;
            public Node(int v) { Value = v; }
        }

        private Node _root;

        /// <summary>
        /// Gets a value indicating whether the BST contains no nodes.
        /// </summary>
        public bool IsEmpty => _root == null;

        /// <summary>
        /// Inserts a value into the BST if it is not already present.
        /// </summary>
        /// <param name="value">The value to insert.</param>
        /// <returns>
        /// <c>true</c> if the value was inserted; <c>false</c> if it was a duplicate.
        /// </returns>
        /// <remarks>
        /// Expected <c>O(log n)</c> time on balanced trees; worst-case <c>O(n)</c> when skewed.
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
        /// Determines whether the specified value exists in the BST.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns><c>true</c> if present; otherwise <c>false</c>.</returns>
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
        /// Computes the height of the BST using the edges convention.
        /// </summary>
        /// <returns>
        /// <c>-1</c> if empty; <c>0</c> for a single node; otherwise the
        /// length (in edges) of the longest path from root to leaf.
        /// </returns>
        /// <remarks>
        /// Time: <c>O(n)</c> over all nodes. Extra space: <c>O(h)</c> recursion.
        /// </remarks>
        public int Height() => Height(_root);

        /// <summary>
        /// Enumerates the values of the BST in sorted (inorder) order.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of values in ascending order.</returns>
        /// <remarks>
        /// Time: visits each node once ⇒ <c>O(n)</c>. Extra space: <c>O(h)</c> if implemented recursively.
        /// </remarks>
        public IEnumerable<int> Inorder() => Inorder(_root);

        // ---------- Private helpers ----------

        /// <summary>
        /// Recursively computes height of a subtree (edges convention).
        /// </summary>
        /// <param name="n">Subtree root.</param>
        /// <returns>Height in edges or <c>-1</c> if <paramref name="n"/> is <c>null</c>.</returns>
        private static int Height(Node n)
        {
            if (n == null) return -1;
            int lh = Height(n.Left);
            int rh = Height(n.Right);
            return 1 + Math.Max(lh, rh);
        }

        /// <summary>
        /// Recursively yields values from a subtree in inorder.
        /// </summary>
        /// <param name="n">Subtree root.</param>
        /// <returns>Inorder sequence of values for the subtree.</returns>
        private static IEnumerable<int> Inorder(Node n)
        {
            if (n == null) yield break;
            foreach (var v in Inorder(n.Left)) yield return v;
            yield return n.Value;
            foreach (var v in Inorder(n.Right)) yield return v;
        }
    }
}