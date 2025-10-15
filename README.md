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
│
├── ConsoleDevUI/
│ ├── Program.cs # Entry point; runs all demos (complexity, arrays, stack/queue)
│
├── DataStructuresToolkit/
│ ├── MyStack.cs # LIFO stack using array doubling policy
│ ├── MyQueue.cs # FIFO queue using circular array and doubling
│ ├── ArrayStringListHelpers.cs # Helper methods for arrays, strings, and lists
│ ├── ComplexityTester.cs # Benchmarks constant, linear, and quadratic operations
│
├── Tests/
│ ├── MyStackTests.cs # NUnit tests for MyStack<T>
│ ├── MyQueueTests.cs # NUnit tests for MyQueue<T>
│ ├── ComplexityTests.cs # NUnit tests verifying ComplexityTester timing patterns

---

### Console Demonstration (Program.cs)

The ConsoleDevUI project serves as a live demonstration environment for all toolkit features.

Sections:

1. Complexity Test Table – Displays performance for O(1), O(n), O(n²).

2. Array, String, and List Operations – Compares array and list manipulation costs.

3. Stack and Queue Demonstration – Visualizes LIFO and FIFO orderings.

---

### ComplexityTester

The ComplexityTester benchmarks runtime growth across three algorithmic complexities using real data and Stopwatch.
Method	                            Description	                            Complexity
RunConstantScenario(int[] data)	    Accesses a single element repeatedly	O(1)
RunLinearScenario(int[] data)	    Iterates over the array once	        O(n)
RunQuadraticScenario(int[] data)	Nested loops visiting all pairs (i, j)	O(n²)

---

### ArrayStringListHelpers

This utility class demonstrates basic data manipulation patterns and their complexities.
Included Methods
Method	                                            Description	                                    Complexity
InsertIntoArray(int[] arr, int index, int value)	Shifts elements and inserts value	            O(n)
DeleteFromArray(int[] arr, int index)	            Removes element and shifts left	                O(n)
ConcatenateNamesNaive(string[] names)	            Builds a string with += in a loop	            O(n²)
ConcatenateNamesBuilder(string[] names)	            Uses StringBuilder for efficient concatenation	O(n)
InsertIntoList(List<T> list, int index, T value)	Inserts into generic list	                    O(n)

---

### MyStack&lt;T&gt;
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

### MyQueue<T>

Implements a First-In-First-Out (FIFO) queue using a circular array with dynamic resizing.
Method                  Description	                                           Complexity
void Enqueue(T item)	Adds an item to the back; resizes if full	           Amortized O(1)
T Dequeue()	            Removes and returns front item; throws if empty	       Amortized O(1)
T Peek()	            Returns front item without removing; throws if empty   O(1)
int Count { get; }	    Gets number of elements                                O(1)

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

---