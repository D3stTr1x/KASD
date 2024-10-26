using MyArray;

class MyPriorityQueue<T>
{
    private MyArrayList<T> queue;
    private int size;
    private Comparer<T> comparator;
    private void RestoringHeap(int i)// Восстановление свойства кучи
    {

        int parent = i;
        int leftChild;
        int rightChild;
        while (true)
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            if ((rightChild < size) && (queue.Get(rightChild) != null) && (queue.Get(parent) != null) && (comparator.Compare(queue.Get(rightChild), queue.Get(parent)))>0)
                parent = rightChild;
            if ((leftChild < size) && (queue.Get(leftChild) != null) && (queue.Get(parent) != null) && (comparator.Compare(queue.Get(leftChild), queue.Get(parent))) > 0)
                parent = leftChild;
            if (parent == i)
                break;
            T temp1 = queue.Get(parent);
            T temp2 = queue.Get(i);
            queue.Set(i, temp1);
            queue.Set(parent, temp2);
            i = parent;
        }
    }
    public MyPriorityQueue() //Конструктор для создания пустой очереди с приоритетами, начальной ёмкостью 11, размещающий элементы согласно естественному порядку сортировки
    {
        queue = new MyArrayList<T>(11);
        size = 0;
        comparator = Comparer<T>.Default;
    }
    public MyPriorityQueue(T[] array) //Конструктор для создания очереди с приоритетами, содержащей элементы масива а
    {
        queue = new MyArrayList<T>();
        comparator = Comparer<T>.Default;
        AddAll(array);
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
    }
    public MyPriorityQueue(int initialCapacity) //Создание пустой очереди с приоритетами
    {
        if (initialCapacity < 0)
            throw new ArgumentOutOfRangeException("initialCapacity < 0");
        comparator = Comparer<T>.Default;
        queue = new MyArrayList<T>(initialCapacity);
        size = 0;
    }
    public MyPriorityQueue(int initialCapacity, Comparer<T> сomparatr) // Пустая очередь с укананой начатьной ёмкостью и компаратором
    {
        if (initialCapacity < 0)
            throw new ArgumentOutOfRangeException("initialCapacity < 0");

        queue = new MyArrayList<T>(initialCapacity);
        size = initialCapacity;
        comparator = сomparatr;
    }
    public MyPriorityQueue(MyPriorityQueue<T> c) //очередь с приоритетами, содержащая элементы указанной очереди с приоритетами
    {
        T[] array = new T[c.size];
        for (int i = 0; i < c.size; i++)
            array[i] = c.queue.Get(i);
        queue = new MyArrayList<T>(c.size);
        size = c.size;
        for (int i = 0; i < c.size; i++)
            queue.Add(array[i]);
        comparator = Comparer<T>.Default;
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
    }
    public void Add(T e) //для добавления элементов в очередь(если размер очереди меньше 64 => +2, иначе => *1.5
    {
        if (size == queue.Size())
        {
            int newCapacity = size < 64 ? size + 2 : size + (size / 2);
            MyArrayList<T> newAr = new MyArrayList<T>(newCapacity);

            for (int i = 0; i < size; i++)
            {
                newAr.Add(queue.Get(i));
            }
            queue = newAr;
        }

        queue.Add(size, e);
        size++;
        RestoringHeap(size - 1);
    }
    public void AddAll(T[] array) //для добавления элементов из массива
    {
        foreach (T item in array)
            Add(item);
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
    }
    public void Clear() //для удаления всех элементов из очереди с приоритетами
    {
        queue.Clear();
        size = 0;
        comparator = Comparer<T>.Default;
    }
    public bool Contains(object o) //для проверки, находится ли указанный объект в очереди с приоритетами
    {
        return queue.Contains(o);
    }
    public bool ContainsAll(T[] array) //для проверки, содержатся ли указанные объекты в очереди с приоритетами
    {
        return queue.ContainsAll(array);
    }
    public bool IsEmpty() //для проверки, является ли очередь с приоритетами пустой
    {
        if (size == 0)
            return true;
        else return false;

    }
    public void Remove(object o) // для удаления указанного объекта из очереди с приоритетами, если он есть там
    {
        int ind = queue.IndexOf(o);
        if (ind == -1) return;
        queue.RemoveInd(ind);
        size--;
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
    }
    public void RemoveAll(T[] array) //для удаления указанных объектов из очереди с приоритетами
    {
        foreach (T item in array)
            Remove(item);
    }
    public void RetainAll(T[] array) //для оставления в очереди с приоритетами только указанных объектов
    {
        Clear();
        foreach (T item in array)
            Add(item);
    }
    public int Size() //для получения размера очереди с приоритетами в элементах
    {
        return size;
    }
    public T[] ToArray() // для возвращения массива объектов, содержащего все элементы очереди с приоритетами
    {
        return queue.ToArray();
    }
    public T[] ToArray(T[] array) //для возвращения массива объектов, содержащего все элементы очереди с приоритетами.Если аргумент a равен null, то создаётся новый массив, в который копируются элементы.
    {
        return queue.ToArray(array);
    }
    public T Element() //для возвращения элемента из головы очереди с приоритетами без его удаления
    {
        return queue.Get(0);
    }
    public bool Offer(T element) // для попытки добавления элемента obj в очередь с приоритетами.Возвращает true, если obj добавлен, и false в противном случае
    {
        if (size == queue.Size())
            return false;
        Add(element);
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
        return true;
    }
    public T Peek() //для возврата элемента из головы очереди с приоритетами без его удаления.Возвращает null, если очередь пуста
    {
        if (size == 0)
            return default(T);
        return queue.Get(0);
    }
    public T Poll()//для удаления и возврата элемента из головы очереди с приоритетами.Возвращает null, если очередь пуста
    {
        if (size == 0)
            return default(T);

        T element = queue.Get(0);
        queue.Set(0, queue.Get(size - 1));
        queue.RemoveInd(size - 1);
        size--;
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
        return element;
    }
    public void PrintQueue()
    {
        for (int i = 0; i < size; i++)
            Console.Write(queue.Get(i) + " ");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        
    }
}