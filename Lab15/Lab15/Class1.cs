using MyQueue;

namespace MyDeque
{
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
                MyPriorityQueue<T> newArray = new MyPriorityQueue<T>((tail * 2) + 1);
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
            elements = new MyPriorityQueue<T>();
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
            MyPriorityQueue<T> newArray = new MyPriorityQueue<T>(tail + 1);
            newArray.Add(item);
            for (int i = 1; i < elements.Size() + 1; i++)
                newArray.Add(elements.GetElem(i - 1));
            elements = newArray;
            tail++;
        }
        // 21
        public void AddLast(T item) => Add(item);

        // 22
        public T GetFirst() => elements.GetElem(head);
        // 23
        public T GetLast() => elements.GetElem(tail - 1);
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
                T element = elements.GetElem(tail - 1);
                elements.RemoveInd(tail - 1);
                tail--;
                return element;
            }
        }
        // 32
        public T RemoveLast()
        {
            T element = elements.GetElem(tail - 1);
            elements.RemoveInd(tail - 1);
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
        public T GetElement(int index) => elements.GetElem(index);
        public void RemoveInd(int index) => elements.RemoveInd(index);
        
        // Print Deque
        public void PrintDeque()
        {
            for (int i = 0; i < Size(); i++)
            {
                Console.Write(elements.GetElem(i) + " ");
            }
            Console.WriteLine();
        }
    }
}
