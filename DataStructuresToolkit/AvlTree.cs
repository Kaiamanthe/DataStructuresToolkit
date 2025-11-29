using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{
    /*
     * Reflection (AVL vs. PriorityQ)
     * ------------------------------------------------
    * How you identified when the AVL tree became unbalanced.
    * I found the unbalanced AVL tree by tacking height and balance factor of each node after each insert.
    * With new inserts, I looked at the difference between the heights of left and right subtrees. If balance
    * factor went outside [-1, 1], then the node is off. Sequences like {10, 20, 30} made it obvious, because
    * the plain BST turned into a straight line, while AVL version detected the imbalance and triggered
    * rotations to regain balance to maintain O(log n).
    * 
    * What you learned about rotations vs. heapify.
    * AVL rotations and heapify are somewhat similar. Both are local fixes that maintain the global rule after
    * update. In AVL trees, the tracking variable is balance factor in [-1, 1], and rotations are the update
    * rebalancing when a node is unbalanced. The heap based PriorityQueue, the tracking variable is whether
    * the parent is equal to or less than the child, and heapify bubbles up or down to reorder when a value
    * is inserted or removed. Both offer O(log n) updates, but they are working with different principles.
    * 
    * Which structure you would choose in a real project, and why.
    * In a professional project, I would choose an AVL-style structure or a built-in balanced tree when lookups
    * across many keys is a concern, because Contains and search stays O(log n). I would use PriorityQueue when
    * what is the next smallest (or highest-priority) item is an issue scheduling or processing tasks by
    * urgency. With both implemented on top of arrays and nodes made those trade-offs more concrete than
    * reading the big-O table.
     */

    /// <summary>
    /// Self-balancing AVL tree of integers built with shared <see cref="Node"/> type.
    /// Supports Insert and Contains with O(log n) height on balanced trees.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Complexity:</b> In a properly balanced AVL tree, insert and contains run in
    /// O(log n) time because the height of the tree is O(log n). This implementation
    /// use shared <see cref="TreeHelper.Height(Node)"/> logic to compute balance
    /// factors and shared <see cref="TreeHelper.RotateLeft(Node)"/> and
    /// <see cref="TreeHelper.RotateRight(Node)"/> helpers methods to perform LL, RR, LR, and RL
    /// rotations.
    /// </para>
    /// </remarks>
    public class AvlTree
    {
        private Node _root;

        public bool IsEmpty => _root == null;

        /// <summary>
        /// Get height of the AVL tree using edges
        /// (empty = -1, leaf = 0), via <see cref="TreeHelper.Height(Node)"/>.
        /// </summary>
        public int Height => TreeHelper.Height(_root);

        /// <summary>
        /// Inserts a value into the AVL tree, rebalancing as needed.
        /// </summary>
        /// <param name="value">The value to insert.</param>
        /// <returns>
        /// <c>true</c> if the value was inserted; <c>false</c> if it was a duplicate.
        /// </returns>
        /// <remarks>
        /// Expected time: O(log n) on a balanced AVL tree. Extra space: O(h) due to recursion,
        /// where h is the height of the tree.
        /// </remarks>
        public bool Insert(int value)
        {
            bool inserted;
            _root = Insert(_root, value, out inserted);
            return inserted;
        }

        private static Node Insert(Node node, int value, out bool inserted)
        {
            if (node == null)
            {
                inserted = true;
                return new Node(value);
            }

            if (value < node.Value)
            {
                node.Left = Insert(node.Left, value, out inserted);
            }
            else if (value > node.Value)
            {
                node.Right = Insert(node.Right, value, out inserted);
            }
            else
            {
                // Duplicate: do not insert again.
                inserted = false;
                return node;
            }

            // Rebalance on the way back up the recursion.
            return Rebalance(node);
        }

        /// <summary>
        /// Determines whether the specified value exists in the AVL tree.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
        /// <remarks>
        /// Expected time: O(log n) on a balanced AVL tree. Extra space: O(1).
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
        /// Enumerates the values of the AVL tree in sorted (inorder) order.
        /// </summary>
        public IEnumerable<int> Inorder()
        {
            return TreeHelper.Inorder(_root);
        }

        // ---------- AVL balancing helpers ----------

        /// <summary>
        /// Rebalances the subtree rooted at <paramref name="node"/> if needed,
        /// using LL, RR, LR, or RL rotations based on the balance factors.
        /// </summary>
        private static Node Rebalance(Node node)
        {
            int balance = TreeHelper.BalFactor(node);

            // Left-heavy (LL or LR).
            if (balance > 1)
            {
                // LR case: left child is right-heavy.
                if (TreeHelper.BalFactor(node.Left) < 0)
                {
                    node.Left = TreeHelper.RotateLeft(node.Left);
                }

                // LL rotation.
                node = TreeHelper.RotateRight(node);
            }
            // Right-heavy (RR or RL).
            else if (balance < -1)
            {
                // RL case: right child is left-heavy.
                if (TreeHelper.BalFactor(node.Right) > 0)
                {
                    node.Right = TreeHelper.RotateRight(node.Right);
                }

                // RR rotation.
                node = TreeHelper.RotateLeft(node);
            }

            return node;
        }

        // ---------- Debug / visualization helpers ----------

        /// <summary>
        /// Exposes the root node for visualization and console demos.
        /// Callers should treat the returned node as read-only.
        /// </summary>
        public Node Root => _root;
    }
}
