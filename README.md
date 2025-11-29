# DataStructuresToolkit

## Overview
**DataStructuresToolkit** is a comprehensive C# capstone project that demonstrates the design, implementation, and testing of core **data structures** and **time complexity behaviors**.  
It combines algorithmic exploration with practical console-based visualization and formal unit testing.

The project includes:
- Custom **MyStack&lt;T&gt;** (LIFO) and **MyQueue&lt;T&gt;** (FIFO)
- **ComplexityTester** to benchmark O(1), O(n), and O(n²) runtimes
- **ArrayStringListHelpers** to compare array, string, and list operations
- Full **NUnit test suite** validating correctness and performance
- **ConsoleDevUI** program that runs everything with clear, readable output

This project was built as part of a capstone focused on **Data Structures and Algorithms in C#**.

---

### Project Structure

DataStructuresToolkit/
- ConsoleDevUI/
    - Program.cs
    - ConOutputHelper.cs

- DataStructuresToolkit/
    - Node.cs
    - TreeToolkit.cs
    - TreeHelper.cs
    - AvlTree.cs
    - PriorityQueue.cs
    - MyStack.cs
    - MyQueue.cs
    - ArrayStringListHelpers.cs
    - ComplexityTester.cs
    - RecursionHelper.cs

- Tests/
    - TreeToolkitTests.cs
    - TreeHelperTests.cs
    - AvlTreeTests.cs
    - PriorityQueueTests.cs
    - MyStackTests.cs
    - MyQueueTests.cs
    - ComplexityTests.cs
    - RecursionHelperTests.cs

---

### Console Demonstration (ConsoleDevUI - Program.cs)

Program.cs

Program.cs is the “script” that walks through the demo:

1. BST vs. AVL
  * Builds an intentionally skewed BST using TreeToolkit (e.g., inserting sorted values like {10, 20, 30}) and prints its height.
  * Builds an AvlTree with the same values, prints its tree shape and height after rotations.
Shows how rotations restore height to O(log n).

2. Priority Queue
  * Enqueues a mix of positive/negative integers into PriorityQueue.
  * Shows that Peek() and Dequeue() always return the smallest value.
  * Dequeues everything to demonstrate sorted output from heap semantics.

3. Other Toolkit Demos (optional depending on how you wire it)
  * Stack vs. queue behavior.
  * Complexity tests (constant / linear / quadratic).
  * Recursion examples (factorial, Fibonacci, palindrome, power set).

ConOutputHelper.cs
ConOutputHelper keeps Program.cs readable by centralizing all console formatting:
 * Section headers, dividers, and simple tables.
 * Utilities to print an entire Node tree, including balance factors for AVL scenarios.
 * Named runners for:
   * BST vs. AVL comparisons
   * Priority queue demos
   * (Optionally) stack, queue, complexity, and recursion demos

The idea: Program.cs reads like a short lab script, and ConOutputHelper handles the noisy Console.WriteLine details.

---

### ComplexityTester (DataStructuresToolkit - Class Library)

The ComplexityTester benchmarks runtime growth across three algorithmic complexities using real data and Stopwatch.
Method	                            Description	                            Complexity
RunConstantScenario(int[] data)	    Accesses a single element repeatedly	O(1)
RunLinearScenario(int[] data)	    Iterates over the array once	        O(n)
RunQuadraticScenario(int[] data)	Nested loops visiting all pairs (i, j)	O(n²)

---

### ArrayStringListHelpers (DataStructuresToolkit - Class Library)

This utility class demonstrates basic data manipulation patterns and their complexities.
Included Methods
Method	                                            Description	                                    Complexity
InsertIntoArray(int[] arr, int index, int value)	Shifts elements and inserts value	            O(n)
DeleteFromArray(int[] arr, int index)	            Removes element and shifts left	                O(n)
ConcatenateNamesNaive(string[] names)	            Builds a string with += in a loop	            O(n²)
ConcatenateNamesBuilder(string[] names)	            Uses StringBuilder for efficient concatenation	O(n)
InsertIntoList(List<T> list, int index, T value)	Inserts into generic list	                    O(n)

---

### MyStack&lt;T&gt; (DataStructuresToolkit - Class Library)
An **array-backed Last-In-First-Out (LIFO)** data structure that grows dynamically using a doubling resize policy.

**Public API**
| Method | Description | Complexity |
|--------|--------------|-------------|
| `void Push(T item)` | Adds an item to the top; resizes if full | Amortized O(1) |
| `T Pop()` | Removes and returns the top item; throws if empty | O(1) |
| `T Peek()` | Returns top item without removing; throws if empty | O(1) |
| `int Count { get; }` | Gets number of elements | O(1) |

**Example:**
```csharp
var stack = new MyStack<string>();
stack.Push("A");
stack.Push("B");
Console.WriteLine(stack.Peek()); // "B"
Console.WriteLine(stack.Pop());  // "B"
Console.WriteLine(stack.Count);  // 1 
```
---

### MyQueue<T> (DataStructuresToolkit - Class Library)

Implements a First-In-First-Out (FIFO) queue using a circular array with dynamic resizing.
Method                  Description	                                           Complexity
void Enqueue(T item)	Adds an item to the back; resizes if full	           Amortized O(1)
T Dequeue()	            Removes and returns front item; throws if empty	       Amortized O(1)
T Peek()	            Returns front item without removing; throws if empty   O(1)
int Count { get; }	    Gets number of elements                                O(1)

---

### RecursionHelper (DataStructuresToolkit - Class Library)

Main three concepts (pure recursion, no loops in recursive core):

1. Mathematical recursion
Factorial(int n) — classic n! definition (base: 0! = 1; recursive: n * factorial(n-1))
Fibonacci(int n) — naive F(n) (base: F(0)=0, F(1)=1; recursive: F(n-1)+F(n-2))
SumArray(int[] arr, int index) — sums arr[index..] (base: index == len → 0; recursive: arr[index] + SumArray(...))

2. Problem-solving recursion
IsPalindrome(string s) — checks palindrome, ignoring case/punct (base: pointers cross; recursive: shrink ends)
PowerSet<T>(T[] items) — all subsets via include/exclude branching with backtracking

3. Structural recursion
TraverseDirectory(string path, int depthLimit, Action<string> onVisit) — walks a directory tree; visits once per path; depth-capped to keep output reasonable

They demonstrate three distinct recursion categories

---

### TreeToolkit
TreeToolkit.cs provides a general binary tree API and a minimal Binary Search Tree (BST), with full XML documentation and complexity notes.

TreeNode
 - static BuildTeachingTree() fixed 5-node tree for deterministic traversal outputs
 - Inorder(TreeNode) L, Root, R
 - Preorder(TreeNode) Root, L, R
 - Postorder(TreeNode) L, R, Root
 - Height(TreeNode) edges convention (empty = −1, leaf = 0)
 - Depth(TreeNode, int target) edges from root; −1 if not found

Complexity: traversals O(n) time, O(h) space; height/depth O(n) time, O(h) space.

Bst
 - bool Insert(int value) → no duplicates (returns false if duplicate)
 - bool Contains(int value)
 - int Height() → uses a private static helper on nodes (edges convention)
 - IEnumerable<int> Inorder() → in-order enumeration (sorted)

Complexity: expected O(log n) search/insert when balanced; O(n) worst-case when skewed; height O(n).

---

### TreeToolkit / TreeHelper / AvlTree / PriorityQueue

TreeHelper.cs
TreeHelper provides general algorithms over the shared Node type:
 * Height(Node root) — height in edges (empty = −1, leaf = 0).
 * Depth(Node root, int target) — edges from root to the node with target (or −1 if not found).
 * Traversals:
   *Inorder(Node) – Left, Root, Right
   *Preorder(Node) – Root, Left, Right
   *Postorder(Node) – Left, Right, Root
 * Rotations:
   * RotateLeft(Node root)
   * RotateRight(Node root)
 * Balance factor helpers used by the AVL tree.

---

### Traversals and height-based operations run in O(n) time, with O(h) recursion space where h is the tree height.

TreeToolkit.cs (Binary Search Tree + Teaching Tree)
TreeToolkit uses Node and TreeHelper to provide:
 * A minimal integer Binary Search Tree (BST):
   * bool Insert(int value) — no duplicates (returns false if duplicate).
   * bool Contains(int value) — standard BST search.
   * int Height() — uses TreeHelper.Height (edges convention).
   * IEnumerable<int> Inorder() — BST values in sorted order.
 * A fixed teaching tree:
   * static Node BuildTeachingTree() — deterministic 5-node tree with known structure, used in tests and console demos.

Complexity:
 * Insert / Contains:
   * Expected O(log n) when reasonably balanced, O(n) worst case when skewed.
 * Height / traversals: O(n) time, O(h) space.

AvlTree.cs (Self-Balancing AVL Tree)
AvlTree is a self-balancing BST that reuses Node and TreeHelper:
 * Maintains AVL balance by checking balance factors (height(left) − height(right)) as inserts unwind.
 * Performs LL, RR, LR, and RL rotations using TreeHelper.RotateLeft and TreeHelper.RotateRight.

Public surface (integer AVL):
 * bool Insert(int value) — inserts while rebalancing; returns false if duplicate.
 * bool Contains(int value) — search with AVL-controlled height.
 * IEnumerable<int> Inorder() — sorted traversal.
 * int Height — height in edges, via TreeHelper.Height.
 * Node Root — exposed for visualization.
 * void PrintTree() — console visualization wrapper (uses ConOutputHelper via a helper).

Complexity:
 * Insert: O(log n) time, O(h) recursion space.
 * Contains: O(log n) time.
 * Inorder: O(n) time, O(h) space.

PriorityQueue.cs (Min-Heap Priority Queue)
PriorityQueue implements an integer min-heap using List<int>:
 * _heap[i] is the node.
 * Children of index i are 2 * i + 1 and 2 * i + 2.
 * Heap property: parent ≤ children.

Public API:
 * int Count { get; }
 * void Enqueue(int value) — append then bubble-up to restore heap property.
 * int Dequeue() — remove and return smallest:
 * Save root, move last element to root, shrink list, then bubble-down.
 * Throws InvalidOperationException if empty.
 * int Peek() — return smallest without removing it (throws if empty).

Complexity:
 * Enqueue / Dequeue: O(log n) time.
 * Peek / Count: O(1) time

---

### Unit Testing (NUnit)

All tests follow the Arrange → Act → Assert (AAA) pattern.

MyStackTest
* Push increases Count; Peek returns last pushe
* Pop returns last pushed, decrements Coun
* Pop/Peek on empty throw InvalidOperationExceptio
* Stress test for resizing with large inpu

MyQueueTest
* Enqueue increases Count; Peek returns first enqueue
* Dequeue returns first enqueued, decrements Coun
* Dequeue/Peek on empty throw InvalidOperationExceptio
* Wraparound test for circular indexin
* Growth test ensures correct resizin

ComplexityTest
* Verifies relative performance patterns for O(1), O(n), and O(n²)
* Ensures benchmark methods produce consistent timing results

RecursionHelperTests
* Factorial: factorial(0) == 1; small positives (1, 5); negative input throws
* Fibonacci: base cases F(0), F(1); known values (5 → 5, 10 → 55); negative input throws
* SumArray: empty array from index 0 → 0; partial sums from different indices; bad index throws
* Contains: present/absent values; empty array returns false; bad index throws
* IsPalindrome: true cases (e.g., “racecar”, punctuation phrase, empty string); false cases; null throws
* PowerSet: empty input → one empty subset; 2 items → 4 subsets; 3 items → 8 subsets; null throws
* TraverseDirectory: depth 0 → only root; depth 1 → root + immediate children (no duplicates, no deeper files); null args throw
* Safety: class-level timeout [Timeout(10000)] to fail fast if anything ever recurses infinitely

TreeHelperTest
  * Height:
    * Empty tree → −1.
    * Single node → 0.
    * Three-level left chain → height 2 (edges).
  * Depth:
    * Finds existing values (root and children).
    * Returns −1 for missing.
  * Traversals:
    * Inorder / Preorder / Postorder on a simple 3-node BST → exact expected sequences.
  * Rotations:
    * Left-heavy and right-heavy two-node trees rebalance to expected shapes.
  * Balance factor:
    * Null node → 0.
    * Leaf → 0.
    * Left-heavy → positive.
    * Right-heavy → negative.

TreeToolkitTests
  * Teaching tree:
    * BuildTeachingTree_ConstructsExpectedShape checks root, children, and leaf values.
  * BST behavior:
    * Insert accepts uniques and rejects duplicates; inorder is sorted.
    * Contains finds present and rejects missing (including an empty BST).
    * Height shows skewed insertion order > more balanced insertion order for the same values.

AvlTreeTests
  * Empty tree: IsEmpty is true; Height is −1; Contains is false; Inorder() is empty.
  * Single insert: insert returns true; height becomes 0; value is found; inorder contains a single element.
  * Duplicate insert: second insert of same value returns false; inorder remains with one element.
  * Rotation cases:
    * {10, 20, 30} → RR rotation, 20 at root, 10 left, 30 right.
    * {30, 20, 10} → LL rotation, 20 at root.
    * {30, 10, 20} → LR rotation.
    * {10, 30, 20} → RL rotation.
  * Inorder is always sorted for arbitrary insertion sequences; Contains is true for inserted values and false for non-members.

PriorityQueueTests
  * New queue starts with Count == 0.
  * Single insert: Enqueue one value → Count == 1; Peek() returns that value.
  * Multiple unordered values: Dequeue() returns them in sorted ascending order.
  * Duplicates: dequeued values form a non-decreasing sequence.
  * Mixed positive/negative values: Peek() / Dequeue() follow correct min-order (e.g., −10, −3, 0, 2, 5).
  * Peek() does not remove the element and does not change Count.
  * Dequeue() and Peek() on an empty queue throw InvalidOperationException.


---