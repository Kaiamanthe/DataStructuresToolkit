using System;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Universal tree node used for tree structures
    /// (BST, AVL, teaching trees, etc.).
    /// </summary>
    public class Node
    {
        /// <summary>Value stored at this node.</summary>
        public int Value { get; set; }

        /// <summary>Left child (may be null).</summary>
        public Node Left { get; set; }

        /// <summary>Right child (may be null).</summary>
        public Node Right { get; set; }

        /// <summary>
        /// Node with value and optional children.
        /// </summary>
        public Node(int value, Node left = null, Node right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Empty one so it doesn't yell.
        /// </summary>
        public Node()
        {

        }
    }
}
