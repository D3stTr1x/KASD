public class MyLinkedList<T>
{
    private class LinkElement<T>
    {
        public T value;
        public LinkElement<T>? next;
        public LinkElement<T>? pred;

        public LinkElement(T element)
        {
            next = null;
            pred = null;
            value = element;
        }
    }
    private LinkElement<T>? first;
    private LinkElement<T>? last;
    private int size;

    // 1
    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }

    // 2
    public MyLinkedList(T[] a)
    {
        foreach (T item in a)
            Add(item);
    }
    // 3
    public void Add(T e)
    {
        LinkElement<T> element = new LinkElement<T>(e);
        if (size == 0)
        {
            first = element;
            last = element;
        }
        else
        {
            last.next = element;
            element.pred = last;
            last = element;
        }
        size++;
    }

    // 4
    public void AddAll(T[] a)
    {
        foreach (T item in a)
            Add(item);
    }
    // 5
    public void Clear()
    {
        first = null;
        last = null;
        size = 0;
    }
    // 6
    public bool Contains(T o)
    {
        LinkElement<T>? step = first;
        while (step != null)
        {
            if (step.value.Equals(o))
                return true;
            step = step.next;
        }
        return false;
    }
    // 7
    public bool ContainsAll(T[] a)
    {
        foreach (T item in a)
        {
            if (!Contains(item))
                return false;
        }
        return true;
    }
    // 8
    public bool IsEmpty() => size == 0;
    // 9
    public void Remove(T o)
    {
        LinkElement<T>? current = first;
        while (current != null)
        {
            if (current.value.Equals(o))
            {
                if (current.pred != null)
                    current.pred.next = current.next;
                else
                    first = current.next;

                if (current.next != null)
                    current.next.pred = current.pred;
                else
                    last = current.pred;

                size--;
                return;
            }
            current = current.next;
        }
    }
    // 10
    public void RemoveAll(T[] a)
    {
        foreach (T item in a)
            Remove(item);
    }
    // 11
    public void RetainAll(T[] a)
    {
        LinkElement<T>? current = first;
        while (current != null)
        {
            if (!Array.Exists(a, element => element.Equals(current.value)))
            {
                LinkElement<T>? next = current.next;
                Remove(current.value);
                current = next;
            }
            else
            {
                current = current.next;
            }
        }
    }
    // 12
    public int Size() => size;
    // 13
    public T[] ToArray()
    {
        T[] newArray = new T[size];
        LinkElement<T>? step = first;
        int i = 0;

        while (step != null)
        {
            newArray[i++] = step.value;
            step = step.next;
        }

        return newArray;
    }
    // 14
    public T[] ToArray(T[] a)
    {
        if (a == null)
            return ToArray();

        T[] newArray = new T[a.Length + size];
        a.CopyTo(newArray, 0);

        LinkElement<T>? step = first;
        int i = a.Length;

        while (step != null)
        {
            newArray[i++] = step.value;
            step = step.next;
        }

        return newArray;
    }
    // 15
    public void Add(int index, T e)
    {
        if (index < 0 || index > size)
            throw new IndexOutOfRangeException("Index out of range");

        LinkElement<T> newElement = new LinkElement<T>(e);

        if (index == 0)
        {
            newElement.next = first;
            if (first != null) first.pred = newElement;
            first = newElement;
            if (last == null) last = newElement;
        }
        else if (index == size)
        {
            newElement.pred = last;
            if (last != null) last.next = newElement;
            last = newElement;
        }
        else
        {
            LinkElement<T>? current = first;
            for (int i = 0; i < index; i++)
                current = current.next;

            newElement.next = current;
            newElement.pred = current.pred;
            current.pred.next = newElement;
            current.pred = newElement;
        }
        size++;
    }
    // 16
    public void AddAll(int index, T[] a)
    {
        foreach (T item in a)
            Add(index++, item);
    }
    // 17
    public T Get(int index)
    {
        if (index < 0 || index >= size)
            throw new IndexOutOfRangeException();

        LinkElement<T>? step = first;
        for (int i = 0; i < index; i++)
            step = step.next;

        return step.value;
    }
    // 18
    public int IndexOf(T o)
    {
        int i = 0;
        LinkElement<T>? step = first;
        while (step != null)
        {
            if (step.value.Equals(o))
                return i;
            i++;
            step = step.next;
        }
        return -1;
    }
    // 19
    public int LastIndexOf(T o)
    {
        int i = 0;
        int retI = -1;
        LinkElement<T>? step = first;
        while (step != null)
        {
            if (step.value.Equals(o))
                retI = i;
            i++;
            step = step.next;
        }
        return retI;
    }
    // 20
    public T RemoveInd(int index)
    {
        T item = Get(index);
        Remove(item);
        return item;
    }
    // 21
    public void Set(int index, T e)
    {
        LinkElement<T>? step = first;
        for (int i = 0; i < index; i++)
            step = step.next;

        step.value = e;
    }
    // 22
    public MyLinkedList<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex >= size || fromIndex > toIndex)
            throw new ArgumentOutOfRangeException();

        MyLinkedList<T> subList = new MyLinkedList<T>();
        LinkElement<T>? current = first;

        for (int i = 0; i < fromIndex; i++)
            current = current.next;

        for (int i = fromIndex; i <= toIndex; i++)
        {
            subList.Add(current.value);
            current = current.next;
        }

        return subList;
    }
    // 23
    public T Element() => first.value;
    // 24
    public bool Offer(T obj)
    {
        Add(obj);
        return true;
    }
    // 25
    public T? Peek()
    {
        return first != null ? first.value : default(T);
    }

    // 26
    public T? Poll()
    {
        if (first == null) return default;
        T value = first.value;
        Remove(value);
        return value;
    }
    // 27
    public void AddFirst(T obj) => Add(0, obj);
    //28
    public void AddLast(T obj) => Add(size, obj);
    // 29
    public T GetFirst()
    {
        if (first == null)
            throw new NullReferenceException();
        return first.value;
    }
    // 30
    public T GetLast()
    {
        if (first == null)
            throw new NullReferenceException();
        return last.value;
    }
    // 31
    public bool OfferFirst(T obj)
    {
        AddFirst(obj);
        if (Contains(obj))
            return true;
        return false;
    }
    // 32
    public bool OfferLast(T obj)
    {
        AddLast(obj);
        if (Contains(obj))
            return true;
        return false;
    }
    // 33
    public T Pop()
    {
        T item = first.value;
        Remove(item);
        return item;
    }
    // 34
    public void Push(T obj) => AddFirst(obj);
    // 35
    public T? PeekFirst() => first != null ? first.value : default(T);

    // 36
    public T? PeekLast() => last != null ? last.value : default(T);

    // 37
    public T? PollFirst()
    {
        if (first == null) return default(T);
        T value = first.value;
        Remove(value);
        return value;
    }
    // 38
    public T? PollLast()
    {
        if (last == null) return default(T);
        T value = last.value;
        Remove(value);
        return value;
    }
    // 39
    public T RemoveLast()
    {
        if (last == null)
            throw new InvalidOperationException("Список пуст.");

        T item = last.value;

        if (size == 1)
        {
            first = null;
            last = null;
        }
        else
        {
            last = last.pred;
            last.next = null;
        }
        size--;
        return item;
    }
    // 40
    public T RemoveFirst()
    {
        if (first == null)
            throw new InvalidOperationException("Список пуст.");

        T item = first.value;

        if (size == 1)
        {
            first = null;
            last = null;
        }
        else
        {
            first = first.next;
            first.pred = null;
        }
        size--;
        return item;
    }
    // 41
    public bool RemoveLastOccurrence(T obj)
    {
        int index = LastIndexOf(obj);
        if (index != -1)
        {
            RemoveInd(index);
            return true;
        }
        return false;
    }
    // 42
    public bool RemoveFirstOccurrence(T obj)
    {
        int index = IndexOf(obj);
        if (index != -1)
        {
            RemoveInd(index);
            return true;
        }
        return false;
    }
    public void Print()
    {
        if (first != null)
        {
            LinkElement<T> step = new LinkElement<T>(first.value);
            step = first;
            while (step != null)
            {
                Console.WriteLine($"{step.value}");
                step = step.next;
            }
        }
        else Console.WriteLine("Empty list");
    }
}

class Program
{
    static void Main()
    {
        // Initialize MyLinkedList instance
        MyLinkedList<int> list = new MyLinkedList<int>();

        // Testing Add method
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Print();
        Console.WriteLine("Expected: 10 20 30");

        // Testing AddFirst and AddLast
        list.AddFirst(5);
        list.AddLast(35);
        list.Print();
        Console.WriteLine("Expected: 5 10 20 30 35");

        // Testing Remove
        list.Remove(20);
        list.Print();
        Console.WriteLine("Expected: 5 10 30 35");

        // Testing RemoveFirst and RemoveLast
        list.RemoveFirst();
        list.RemoveLast();
        list.Print();
        Console.WriteLine("Expected: 10 30");

        // Testing Contains
        Console.WriteLine("Contains 10: " + list.Contains(10)); // Expected: True
        Console.WriteLine("Contains 20: " + list.Contains(20)); // Expected: False

        // Testing Get and IndexOf
        Console.WriteLine("Element at index 1: " + list.Get(1)); // Expected: 30
        Console.WriteLine("Index of 30: " + list.IndexOf(30));   // Expected: 1

        // Testing Clear
        list.Clear();
        Console.WriteLine("List is empty: " + list.IsEmpty());   // Expected: True

        // Testing AddAll
        list.AddAll(new int[] { 1, 2, 3, 4 });
        list.Print();
        Console.WriteLine("Expected: 1 2 3 4");

        // Testing Size
        Console.WriteLine("Size of list: " + list.Size());       // Expected: 4

        // Testing ToArray
        int[] array = list.ToArray();
        Console.WriteLine("Array contents: " + string.Join(", ", array)); // Expected: 1, 2, 3, 4

        // Testing SubList
        MyLinkedList<int> subList = list.SubList(1, 2);
        subList.Print();
        Console.WriteLine("Expected: 2 3");

        // Testing Poll, Peek, Offer
        Console.WriteLine("Peek first element: " + list.Peek()); // Expected: 1
        Console.WriteLine("Poll first element: " + list.Poll()); // Expected: 1
        list.Offer(5);
        list.Print();
        Console.WriteLine("Expected after offering 5: 2 3 4 5");

        // Testing OfferFirst and OfferLast
        list.OfferFirst(0);
        list.OfferLast(6);
        list.Print();
        Console.WriteLine("Expected: 0 2 3 4 5 6");

        // Testing LastIndexOf
        list.Add(2);
        Console.WriteLine("Last index of 2: " + list.LastIndexOf(2)); // Expected: 6

        // Testing RemoveFirstOccurrence and RemoveLastOccurrence
        list.RemoveFirstOccurrence(2);
        list.Print();
        Console.WriteLine("Expected after removing first occurrence of 2: 0 3 4 5 6 2");
        list.RemoveLastOccurrence(2);
        list.Print();
        Console.WriteLine("Expected after removing last occurrence of 2: 0 3 4 5 6");

        // Testing Push and Pop
        list.Push(10);
        Console.WriteLine("Peek after Push: " + list.Peek()); // Expected: 10
        list.Print();
        Console.WriteLine("Pop: " + list.Pop()); // Expected: 10

        // Testing PeekFirst and PeekLast
        Console.WriteLine("Peek First: " + list.PeekFirst()); // Expected: 0
        Console.WriteLine("Peek Last: " + list.PeekLast());   // Expected: 6

        // Testing PollFirst and PollLast
        Console.WriteLine("Poll First: " + list.PollFirst()); // Expected: 0
        Console.WriteLine("Poll Last: " + list.PollLast());   // Expected: 6

        // Print final state of the list
        list.Print();
    }
}