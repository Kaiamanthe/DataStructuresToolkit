using System;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Universal Node
    /// (BST, AVL, teaching trees, etc.).
    /// </summary>
    public class Node
    {
        /// <summary>Val stored at this node.</summary>
        public int Value { get; set; }

        /// <summary>Left child (nullable).</summary>
        public Node Left { get; set; }

        /// <summary>Right child (nullable).</summary>
        public Node Right { get; set; }

        /// <summary>
        /// Node with val and optional children.
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

    /// <summary>
    /// Generic singly linked list node.
    /// </summary>
    /// <typeparam name="T">Element type stored in the node.</typeparam>
    public class ListNode<T>
    {
        /// <summary>Val stored at this node.</summary>
        public T Value { get; set; }

        /// <summary>Next node in the list (nullable at end).</summary>
        public ListNode<T> Next { get; set; }

        /// <summary>
        /// Creates a node with given val.
        /// </summary>
        /// <param name="value">Val to store.</param>
        public ListNode(T value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Generic doubly linked list node.
    /// </summary>
    /// <typeparam name="T">Element type stored in node.</typeparam>
    public class DoublyListNode<T>
    {
        /// <summary>Val stored at this node.</summary>
        public T Value { get; set; }

        /// <summary>Next node in list (toward tail).</summary>
        public DoublyListNode<T> Next { get; set; }

        /// <summary>Previous node in list (toward head).</summary>
        public DoublyListNode<T> Prev { get; set; }

        /// <summary>
        /// Creates a node with given val.
        /// </summary>
        /// <param name="value">Val to store.</param>
        public DoublyListNode(T value)
        {
            Value = value;
        }
    }
}
