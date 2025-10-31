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
    - MyStack.cs
    - MyQueue.cs
    - ArrayStringListHelpers.cs
    - ComplexityTester.cs
    - RecursionHelper.cs

- Tests/
    - MyStackTests.cs
    - MyQueueTests.cs
    - ComplexityTests.cs
    - RecursionHelperTests.cs



---

### Console Demonstration (ConsoleDevUI - Program.cs)

The ConsoleDevUI project serves as a live demonstration environment for all toolkit features.

Sections:

1. Complexity Test Table – Displays performance for O(1), O(n), O(n²).

2. Array, String, and List Operations – Compares array and list manipulation costs.

3. Stack and Queue Demonstration – Visualizes LIFO and FIFO orderings.

4. Resursion
    a. Mathmatical
    b. Structural
    c. Problem-solving

---

### ConOutputHelper (ConsoleDevUI)

Why it’s here: keep Program.cs clean. All console formatting and demo wrappers live in one spot so the main program reads like a script.
What it does: headers/dividers, simple tables, pretty printers, and demo runners for all recursion features (factorial, Fibonacci, sum, contains, palindrome, power set, and directory traversal).

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


---