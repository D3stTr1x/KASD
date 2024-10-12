class MyVector<T>
{
    protected int elementCount;
    protected int capacityIncrement;
    protected T[]? elementData;

    public MyVector(int initialCapacity, int initialcapacityIncrement) //для создания пустого вектора с начальной ёмкостью initialCapacity и значением приращения ёмкости capacityIncrement
    {
        elementData = new T[initialCapacity];
        elementCount = initialCapacity;
        capacityIncrement = initialcapacityIncrement;
    }
    public MyVector(int initialCapacity) //для создания пустого вектора с начальной ёмкостью initialCapacity и значением приращения ёмкости по умолчанию
    {
        elementData = new T[initialCapacity];
        capacityIncrement = 0;
    }
    public MyVector() //для создания пустого вектора с начальной ёмкостью по умолчанию и значением приращения ёмкости по умолчанию
    {
        elementData = null;
        capacityIncrement = 0;
        elementCount = 0;
    }
    public MyVector(T[] a) // для создания вектора и заполнения его элементами из передаваемого массива a
    {
        elementData = new T[(int)(a.Length)];
        for (int i = 0; i < a.Length; i++)
            elementData[i] = a[i];
        elementCount = a.Length;
    }
    public void Add(T e) // для добавления элемента в конец вектора. Если размер вектора больше текущей ёмкости, необходимо увеличить ёмкость вектора на значение capacityIncrement(если оно не равно 0) или удвоить текущую ёмкость
    {
        if (elementData == null)
        {
            elementData = new T[capacityIncrement > 0 ? capacityIncrement : 10]; // Используем начальную ёмкость, если она не задана
        }

        if (elementCount == elementData.Length)
        {
            T[]? newArray = null;
            if (capacityIncrement != 0) newArray = new T[elementCount + capacityIncrement];
            else newArray = new T[elementCount*2];
            for (int i = 0; i < elementCount; i++) newArray[i] = elementData[i];
            elementData = newArray;
        }
        elementData[elementCount] = e;
        elementCount++;
    }
    public void AddAll(T[] a) // для добавления элементов из массива
    {
        foreach (T item in a)
            Add(item);
    }
    public void Clear() // для удаления всех элементов из вектора
    {
        elementData = null;
        elementCount = 0;
    }
    public bool Contains(object o) //  для проверки, находится ли указанный объект в векторе
    {
        if (elementData != null)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (o.Equals(elementData[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool ContainsAll(T[] a) // для проверки, содержатся ли указанные объекты в векторе
    {
        bool f = false;
        foreach (T item in a)
            if (item != null)
                f = Contains(item);
        return f;
    }
    public bool IsEmpty() // для проверки, является ли вектор пустым
    {
        if (elementData == null)
            return true;
        return false;
    }
    public void Remove(object o) // Удаление указанного объекта из вектора
    {
        if (o == null || elementData == null)
        {
            return;
        }

        int index = IndexOf(o);
        if (index >= 0)
        {
            RemoveInd(index); // Используем RemoveInd для удаления по индексу
        }
    }
    public void RemoveAll(T[] a) // для удаления указанных объектов из вектора
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        foreach (T item in a)
            if (item != null)
                Remove(item);
    }
    public void RetainAll(T[] a) // для оставления в векторе только указанных объектов
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        T[] newVector = new T[elementCount];
        int newSize = 0;
        foreach (T item in a)
            for (int i = 0; i < elementCount; i++)
                if (item != null)
                    if (item.Equals(elementData[i]))
                    {
                        newVector[newSize] = elementData[i];
                        newSize++;
                    }
        elementData = newVector;
        elementCount = newSize;
    }
    public int Size() // для получения размера вектора в элементах
    {
        return elementCount;
    }
    public object?[] ToArray() // для возвращения массива объектов, содержащего все элементы вектора
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");
        object?[] a = new object?[elementCount];
        for (int i = 0; i < elementCount; i++)
            a[i] = elementData[i];
        return a;
    }
    public object? ToArray(T[] a) // для возвращения массива объектов, содержащего все элементы вектора.Если аргумент a равен null, то создаётся новый массив, в который копируются элементы
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");

        if (a == null)
        {
            a = new T[elementCount];
        }
        for (int i = 0; i < elementCount && i < a.Length; i++)
            a[i] = elementData[i];
        return a.Cast<object?>().ToArray();
    }
    public void Add(int index, T e) // для добавления элемента в указанную позицию
    {
        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException("index");

        if (elementData == null)
            elementData = new T[capacityIncrement > 0 ? capacityIncrement : 10];

        if (elementCount == elementData.Length)
        {
            T[] newArray = new T[elementCount + 1];
            for (int i = 0; i < elementCount; i++)
                newArray[i] = elementData[i];
            elementData = newArray;
        }

        for (int i = elementCount; i > index; i--)
            elementData[i] = elementData[i - 1];

        elementData[index] = e;
        elementCount++;
    }
    public void AddAll(int index, T[] a) // для добавления элементов в указанную позицию
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        foreach (T item in a)
        {
            Add(index, item);
            index++;
        }
    }
    public T Get(int index) // для возвращения элемента в указанной позиции
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");

        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException(nameof(index));
        return elementData[index];
    }
    public int IndexOf(object o) //
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");

        for (int i = 0; i < elementCount; i++)
            if (o!=null && o.Equals(elementData[i]))
                return i;
        return -1;
    }
    public int LastIndexOf(object o) // для нахождения последнего вхождения указанного объекта, или -1, если его нет в векторе
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");

        for (int i = elementCount - 1; i >= 0; i--)
            if (o.Equals(elementData[i]))
                return i;
        return -1;
    }
    public T RemoveInd(int index) // Удаление элемента по индексу
    {
        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");

        T e = elementData[index];

        for (int i = index; i < elementCount - 1; i++)
        {
            elementData[i] = elementData[i + 1];
        }

        elementData[elementCount - 1] = default!; // Для значимых типов присваиваем значение по умолчанию
        elementCount--;

        return e; // Возвращаем удаленный элемент
    }
    public void Set(int index, T e) // для замены элемента в указанной позиции новым элементом
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");
        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (e == null)
            throw new ArgumentNullException();
        elementData[index] = e;
    }
    public MyVector<T> SubList(int fromIndex, int toIndex) // для возвращения части вектора
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");
        if (fromIndex > toIndex)
            throw new ArgumentException("fromIndex > toIndex");
        if (fromIndex < 0 || fromIndex >= elementCount)
            throw new ArgumentOutOfRangeException("fromIndex");
        if (toIndex < 0 || toIndex > elementCount)
            throw new ArgumentOutOfRangeException("toIndex");
        int size = toIndex - fromIndex;
        MyVector<T> subList = new MyVector<T>(size); // Инициализируем с правильной ёмкостью
        for (int i = 0; i < size; i++)
        {
            subList.Add(elementData[fromIndex + i]); // Добавляем элементы от fromIndex до toIndex (исключая toIndex)
        }

        return subList;
    }

    public T FirstElement() // для обращения к первому элементу вектора
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");
        return elementData[0];
    }

    public T LastElement()  // для обращения к последнему элементу вектора
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");
        return elementData[elementCount - 1];
    }

    public void RemoveElementAt(int pos)  // для удаления элемента в заданной позиции
    {
        if (elementData != null)
        {
            if (pos < 0 || pos >= elementCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (elementData[pos] != null)
            {
                for (int i = pos; i < elementCount - 1; i++)
                {
                    elementData[i] = elementData[i + 1];
                }
                elementData[elementCount - 1] = default!;
                elementCount--;
            }
        }
    }

    public void RemoveRange(int begin, int end)  //для удаления нескольких подряд идущих элементов
    {
        if (begin < 0 || end >= elementCount)
            throw new ArgumentOutOfRangeException("fromIndex");
        if (begin < 0 || end >= elementCount)
            throw new ArgumentOutOfRangeException("toIndex");
        int len = end - begin;
        if (len < 0)
            throw new ArgumentException("begin > end");
        if (elementData != null)
        {
            for (int i = begin; i < end - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }
            elementCount -= len;
        }
    }

    public void Print()
    {
        if (elementData != null)
        {
            for (int i = 0; i < elementCount; i++)
                Console.Write($"{elementData[i]} ");
            Console.WriteLine();
        }
        else Console.WriteLine("Empty vector");
    }
}

class MyStack<T> : MyVector<T>
{
    public MyStack()
    {
        new MyVector<T>();
    }
    public void Push(T item)
    {
        Add(item);
    }
    public void Pop()
    {
        if (Size() == 0)
            throw new InvalidOperationException("Stack is empty.");
        RemoveInd(Size()-1);
    }
    public T Peek()
    {
        if (Size() == 0) 
            throw new Exception("Stack if empty.");
        else return Get(Size()-1);
    }
    public bool Empty()
    {
        if (Size() == 0) 
            return false;
        return true;
    }
    public int Search(T item)
    {
        if (Size() == 0)
            return -1;

        for (int i = Size()-1; i >= 0; i--)
        {
            if (Get(i).Equals(item))
            {
                return Size() - i;
            }
        }
        return -1;
    }

    public void PrintStack()
    {
        Console.WriteLine("Stack: ");
        for (int i = Size()-1; i >= 0; i--)
            Console.WriteLine(Get(i));
    }
}

class Programm
{
    static void Main(string[] args)
    {
        MyStack<int> st = new MyStack<int>();
        int[] a = new int[15];
        Random rand = new Random();
        Console.WriteLine("Рандомный массив:");
        for(int i =0; i<a.Length; i++)
        {
            a[i] = rand.Next(0, 10);
            Console.Write($"{a[i]} ");
            st.Push(a[i]);
        }
        Console.WriteLine();
        Console.WriteLine();
        st.PrintStack();
        for (int i = 0; i <= rand.Next(1, 5); i++)
        {
            st.Pop();
        }
        st.PrintStack();
        int tp = st.Peek();
        Console.WriteLine($"Верхний элемент: {tp}");
        Console.WriteLine($"Элемнет 5 на позиции: {st.Search(5)}");
        Console.WriteLine(st.Size()-1);
    }
}