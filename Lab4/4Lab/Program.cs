public class MyArrayList<T>
{
    private int size;
    T[] elementData;
    public MyArrayList() // Создание пустого динамического массива
    {
        elementData = Array.Empty<T>();
        size = 0;
    }

    public MyArrayList(T[] a) //  создания динамического массива и заполнения его элементами из передаваемого массива a.
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        elementData = new T[(int)(a.Length)];
        for (int i = 0; i < a.Length; i++)
            elementData[i] = a[i];
        size = a.Length;
    }

    public MyArrayList(int capacity) //создания пустого динамического массива с внутренним массивом, размер которого будет равен значению параметра capacity.

    {
        elementData = new T[capacity];
        size = capacity;
    }

    public void Add(T e) // для добавления элемента в конец динамического массива.
    {
        if (elementData == null)
            elementData = new T[1];
        else if (size == elementData.Length)
        {
            T[] array = new T[(int)(elementData.Length*1.5) + 1];
            for (int i = 0; i < size; i++)
                array[i] = elementData[i];
            elementData = array;
        }
        elementData[size] = e;
        size++;
    }

    public void AddAll(T[] a) // для добавления элементов из массива
    {
        foreach (T item in a)
            Add(item);
    }

    public void Clear() //  для удаления всех элементов из динамического массива
    {
        elementData = Array.Empty<T>();
        size = 0;
    }

    public bool Contains(object o) //  для проверки, находится ли указанный объект в динамическом массиве.
    {
        if (elementData != null && size != 0)
        {
            for (int i = 0; i < size; i++)
            {
                if (o.Equals(elementData[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool ContainsAll(T[] a) // для проверки, содержатся ли указанные объекты в динамическом массиве.
    {
        bool f = false;
        foreach (T item in a)
            if (item != null)
                f = Contains(item);
        return f;
    }

    public bool IsEmpty() //для проверки, является ли динамический массив пустым.
    {
        if (size == 0)
            return true;
        else return false;
    }

    public void Remove(object o) // для удаления указанного объекта из динамического массива, если он есть там.
    {
        if (o == null || elementData == null)
        {
            return;
        }

        if (Contains(o))
        {
            int index = IndexOf(o);

            if (index >= 0)
            {
                for (int i = index; i < size - 1; i++)
                {
                    elementData[i] = elementData[i + 1];
                }
                elementData[size - 1] = default!; // Для значимых типов присваиваем значение по умолчанию
                size--;
                Remove(o);
            }
        }
    }

    public void RemoveAll(T[] a) //  для удаления указанных объектов из динамического массива.
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        foreach (T item in a)
            if (item != null)
                Remove(item);
    }

    public void RetainAll(T[] a) // для оставления в динамическом массиве только указанных объектов
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        T[] newArray = new T[size];
        int newSize = 0;
        foreach (T item in a)
            for (int i = 0; i < size; i++)
                if (item!=null)
                    if (item.Equals(elementData[i]))
                    {
                        newArray[newSize] = elementData[i];
                        newSize++;
                    }
        elementData = newArray;
        size = newSize;
    }

    public int Size() //  для получения размера динамического массива в элементах.
    {
        return size;
    }

    public object?[] ToArray() // для возвращения массива объектов, содержащего все элементы динамического массива
    {
        object?[] a = new object?[size];
        for (int i = 0; i < size; i++)
            if (elementData[i] != null)
                a[i] = elementData[i];
        return a;
    }

    public object?[] ToArray(T[] a) //для возвращения массива объектов, содержащего все элементы динамического массива.Если аргумент a равен null, то создаётся новыймассив, в который копируются элементы
    {
        if (a == null)
        {
            a = new T[size];
        }
        for (int i = 0; i < size && i < a.Length; i++)
            a[i] = elementData[i];
        return a.Cast<object?>().ToArray(); // Приводим T[] к object?[] и возвращаем
    }

    public void Add(int index, T e) //для добавления элемента в указанную позицию
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException("index");

        if (elementData == null)
            elementData = new T[1];

        if (size == elementData.Length)
        {
            T[] newArray = new T[size+size/2 + 1];
            for (int i = 0; i < size; i++)
                newArray[i] = elementData[i];
            elementData = newArray;
        }

        for (int i = size; i > index; i--)
            elementData[i] = elementData[i - 1];

        elementData[index] = e;
        size++;
    }

    public void AddAll(int index, T[] a) //для добавления элементов в указанную позицию
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));
        foreach (T item in a)
        {
            Add(index, item);
            index++;
        }
    }

    public T Get(int index) //для возвращения элемента в указанной позиции.
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
        return elementData[index];
    }

    public int IndexOf(object o) //для возвращения индекса указанного объекта, или -1, если его нет в динамическом массиве
    {
        for (int i = 0; i < size; i++)
            if (o.Equals(elementData[i]))
                return i;
        return -1;
    }

    public int LastIndexOf(object o) //для нахождения последнего вхождения указанного объекта, или -1, если его нет в динамическом массиве
    {
        for (int i = size - 1; i >= 0; i--)
            if (o.Equals(elementData[i]))
                return i;
        return -1;

    }

    public T RemoveInd(int index) //для удаления и возвращения элемента в указанной позиции
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
        T e = elementData[index];
        if (e!=null)
            Remove(e);
        return e;
    }

    public void Set(int index, T e)//для замены элемента в указанной позиции новым элементом
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (e == null)
            throw new ArgumentNullException();
        elementData[index] = e;
    }

    public MyArrayList<T> SubList(int fromIndex, int toIndex) //для возвращения части динамического массива, т.е. элементов в диапазоне [fromIndex; toIndex).
    {
        if (fromIndex > toIndex)
            throw new ArgumentException("fromIndex > toIndex");
        if (fromIndex < 0 || fromIndex >= size)
            throw new ArgumentOutOfRangeException("fromIndex");
        if (toIndex < 0 || toIndex >= size)
            throw new ArgumentOutOfRangeException("toIndex");
        MyArrayList<T> list = new MyArrayList<T>(toIndex - fromIndex);
        for (int i = 0; i < list.size; i++)
            list.Set(i, elementData[i + fromIndex]);
        return list;

    }

    public void Print()
    {
        if (size != 0)
        {
            for (int i = 0; i < size; i++)
                Console.Write($"{elementData[i]} ");
            Console.WriteLine();
        }
        else Console.WriteLine("Empty array");
    }
}

class Programm
{ 
    static void Main(string[] args)
    {
        MyArrayList<int> array = new MyArrayList<int>(7);
        array.Print();
        int[] a = new int[20];
        Random rand = new Random();
        for(int i =0; i<a.Length; i++)
        {
            a[i]=rand.Next(0, 100);
        }
        array.AddAll(a);
        array.Print();
        
        int size = array.Size();
        Console.WriteLine(size);
        bool cont = array.Contains(25);
        Console.WriteLine(cont);
        array.Add(3, 25);
        array.Add(15, 25);
        array.Print();
        int ind = array.LastIndexOf(25);
        Console.WriteLine(ind);
        array.Remove(25);
        array.Print();
        array.Clear();
        array.Print();
        for (int i = 0; i < a.Length; i++)
        {
            a[i] = rand.Next(0, 100);
        }
        array.AddAll(a);
        array.Print();
        try
        {
            array = array.SubList(4, 12);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message); 
        }
        array.Print();
        array.RemoveInd(3);
        array.Print();
        try
        {
           array.Set(4, 505);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        array.Print();
        int w = array.LastIndexOf(505);
        Console.WriteLine(w);
        object t;
        try
        {
            t = array.Get(5);
            Console.WriteLine(t);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        object?[] r = array.ToArray();
        Console.Write("Array of object: ");
        for (int i =0; i<r.Length; i++)
            Console.Write($"{r[i]} ");
        Console.WriteLine();
        bool emp = array.IsEmpty();
        Console.WriteLine(emp);
    }
}