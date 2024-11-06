using MyQueue;
class MyArrayDeque<T>
{
    private MyPriorityQueue<T> elements;
    private int head;
    private int tail;

    // 1
    public MyArrayDeque()
    {
        elements = new MyPriorityQueue<T>(16);
        head = 0;
        tail = 0;
    }
    // 2
    public MyArrayDeque(T[] a) 
    {
        MyPriorityQueue<T> newArray = new MyPriorityQueue<T>(a.Length);
        head = 0;
        tail = a.Length;
        newArray.AddAll(a);        
        elements = newArray;
    }
    // 3
    public MyArrayDeque(int numElements)
    {
        MyPriorityQueue<T> newArray = new MyPriorityQueue<T>(numElements);
        head = 0;
        tail = numElements;
        elements = newArray;
    }
    // 4
    public void Add(T item)
    {
        if (tail == elements.Size()) 
        {
            MyPriorityQueue<T> newArray = new MyPriorityQueue<T>((tail * 2) +1);
            for (int i = 0; i < tail; i++)
                newArray.Add(elements.GetElem(i));
            newArray.Add(item);
            elements = newArray;
            tail++;
        }
        else
        {
            elements.Replace(tail, item);
        }
    }
    // 5
    public void AddAll(T[] array)
    {
        foreach (T item in array)
            Add(item);
    }
    // 6
    public void Clear()
    {
        head = 0;
        tail = 0;
        elements = new  MyPriorityQueue<T>();
    }
    // 7
    public bool Contains(T item) => elements.Contains(item);
    
    // 8
    public bool ContainsAll(T[] array) => elements.ContainsAll(array);
    
    // 9
    public bool IsEmpty()
    {
        if (tail == 0)
            return true;
        else return false;
    }
    // 10
    public void Remove(T item)
    {
        if (Contains(item))
        {
            elements.Remove(item);
            tail--;
        }
    }
    // 11
    public void RemoveAll(T[] array)
    {
        elements.RemoveAll(array);
        tail -= array.Length;
    }
    // 12
    public void RetainAll(T[] array)
    {
        elements.RetainAll(array);
        tail = array.Length;
    }
    // 13
    public int Size() => tail;
    // 14
    public T[] ToArray() => elements.ToArray();
    // 15
    public T[] ToArray(T[] array) => elements.ToArray(array);    
    // 16
    public T Element() => elements.GetElem(head);
    // 17
    public bool Offer(T item)
    {
        AddLast(item);
        if (Contains(item)) return true;
        else return false;
    }
    // 18
    public T Peek() => elements.Peek();
    // 19
    public T Poll() => elements.Poll();
    // 20
    public void AddFirst(T item)
    {
        MyPriorityQueue<T> newArray = new MyPriorityQueue<T>(tail+1);
        newArray.Add(item);
        for (int i = 1; i < elements.Size()+1; i++)
            newArray.Add(elements.GetElem(i-1));        
        elements = newArray;
        tail++;
    }
    // 21
    public void AddLast(T item) => Add(item);
    
    // 22
    public T GetFirst() => elements.GetElem(head);
    // 23
    public T GetLast() => elements.GetElem(tail-1);
    // 24
    public bool OfferFirst(T item)
    {
        AddFirst(item);
        if (Contains(item)) return true;
        else return false;
    }
    // 25
    public bool OfferLast(T item)
    {
        AddLast(item);
        if (Contains(item)) return true;
        else return false;
    }
    // 26
    public T Pop()
    {
        T item = elements.GetElem(head);
        elements.RemoveInd(head);
        tail--;
        return item;
    }
    // 27
    public void Push(T item) => AddFirst(item);
    // 28
    public T PeekFirst()
    {
        if (tail == 0)
            return default(T);
        return GetFirst();
    }

    // 29
    public T PeekLast()
    {
        if (tail == 0)
            return default(T);
        return GetLast();
    }
    // 30
    public T PollFirst()
    {
        if (tail == 0)
            return default(T);
        else
        {
            T element = elements.GetElem(head);
            elements.RemoveInd(head);
            tail--;
            return element;
        }
    }
    // 31
    public T PollLast()
    {
        if (tail == 0)
            return default(T);
        else
        {
            T element = elements.GetElem(tail-1);
            elements.RemoveInd(tail-1);
            tail--;
            return element;
        }
    }
    // 32
    public T RemoveLast()
    {
        T element = elements.GetElem(tail - 1);
        elements.RemoveInd(tail-1);
        tail--;
        return element;
    }
    // 33
    public T RemoveFirst()
    {
        T element = elements.GetElem(head);
        elements.RemoveInd(head);
        tail--;
        return element;
    }
    // 34
    public bool RemoveLastOccurrence(T element)
    {
        int index = -1;
        for (int i = 0; i < tail; i++)
        {
            if (elements.Size() != 0)
            {
                if (elements.GetElem(i).Equals(element))
                {
                    index = i;
                }
            }
        }
        if (index > -1)
        {
            elements.RemoveInd(index);
            tail--;
            return true;
        }
        return false;
    }
    // 35
    public bool RemoveFirstOccurrence(T element)
    {
        int index = -1;
        for (int i = 0; i < tail; i++)
        {
            if (elements.Size() != 0)
            {
                if (elements.GetElem(i).Equals(element))
                {
                    index = i;
                    break;
                }
            }
        }
        if (index > -1)
        {
            elements.RemoveInd(index);
            tail--;
            return true;
        }
        return false;
    }
    // Print Deque
    public void PrintDeque()
    {
        for(int i =0; i<Size(); i++)
        {
            Console.Write(elements.GetElem(i) + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Instantiate MyArrayDeque with default constructor
        MyArrayDeque<int> deque = new MyArrayDeque<int>();

        // Test 1: Add, AddFirst, AddLast, and size verification
        deque.Add(10);
        deque.PrintDeque();
        deque.AddFirst(5);
        deque.PrintDeque();
        deque.AddLast(15);
        Console.WriteLine("Size after adding 3 elements: " + deque.Size()); // Expected: 3
        deque.PrintDeque();

        // Test 2: Peek, PeekFirst, PeekLast
        Console.WriteLine("Peek first element: " + deque.PeekFirst()); // Expected: 5
        Console.WriteLine("Peek last element: " + deque.PeekLast());   // Expected: 15
        Console.WriteLine("Peek any element: " + deque.Peek());        // Expected: 5

        // Test 3: Contains and ContainsAll
        Console.WriteLine("Contains 10: " + deque.Contains(10));       // Expected: True
        Console.WriteLine("Contains 20: " + deque.Contains(20));       // Expected: False
        Console.WriteLine("Contains all [5, 10, 15]: " + deque.ContainsAll(new int[] { 5, 10, 15 })); // Expected: True

        deque.PrintDeque();
        // Test 4: PollFirst, PollLast, and size adjustment
        Console.WriteLine("Poll first element: " + deque.PollFirst()); // Expected: 5
        Console.WriteLine("Poll last element: " + deque.PollLast());   // Expected: 15
        Console.WriteLine("Size after polling: " + deque.Size());      // Expected: 1
        deque.PrintDeque();

        // Test 5: AddAll, ToArray, Clear
        deque.AddAll(new int[] { 1, 2, 3 });
        int[] array = deque.ToArray();
        Console.WriteLine("Array contents after AddAll [1, 2, 3]: " + string.Join(", ", array)); // Expected: 10, 1, 2, 3
        deque.Clear();
        Console.WriteLine("Is deque empty after Clear: " + deque.IsEmpty()); // Expected: True

        // Test 6: Remove and RemoveAll
        deque.AddAll(new int[] { 1, 2, 3, 4, 5 });
        deque.Remove(3);
        Console.WriteLine("Contains 3 after Remove: " + deque.Contains(3)); // Expected: False
        deque.RemoveAll(new int[] { 1, 2 });
        Console.WriteLine("Contains 1 and 2 after RemoveAll: " + (deque.Contains(1) || deque.Contains(2))); // Expected: False

        // Test 7: RetainAll
        deque.AddAll(new int[] { 4, 5, 6 });
        deque.RetainAll(new int[] { 5 });
        Console.WriteLine("Contents after RetainAll [5]: " + string.Join(", ", deque.ToArray())); // Expected: 5

        // Test 8: GetFirst, GetLast, and size verification
        deque.AddFirst(0);
        deque.AddLast(10);
        Console.WriteLine("Get first element: " + deque.GetFirst());  // Expected: 0
        Console.WriteLine("Get last element: " + deque.GetLast());    // Expected: 10
        Console.WriteLine("Size after adding first and last: " + deque.Size()); // Expected: 3

        // Test 9: Offer, OfferFirst, OfferLast
        Console.WriteLine("Offer 20 to the end: " + deque.Offer(20));           // Expected: True
        deque.PrintDeque();
        Console.WriteLine("OfferFirst 30: " + deque.OfferFirst(30));            // Expected: True
        deque.PrintDeque();
        Console.WriteLine("OfferLast 40: " + deque.OfferLast(40));              // Expected: True
        deque.PrintDeque();
        Console.WriteLine("Contents after offers: "); // Expected: 30, 0, 5, 10, 20, 40
        deque.PrintDeque();

        // Test 10: RemoveFirstOccurrence, RemoveLastOccurrence
        deque.AddAll(new int[] { 5, 5, 5 });
        Console.WriteLine("RemoveFirstOccurrence of 5: " + deque.RemoveFirstOccurrence(5)); // Expected: True
        Console.WriteLine("RemoveLastOccurrence of 5: " + deque.RemoveLastOccurrence(5));   // Expected: True
        Console.WriteLine("Contents after removing occurrences of 5: ");
        deque.PrintDeque();

        // Test 11: Pop and Push
        deque.Push(100);
        Console.WriteLine("Pop element after push 100: " + deque.Pop());       // Expected: 100
        Console.WriteLine("Contents after Pop: ");
        deque.PrintDeque();
    }
}