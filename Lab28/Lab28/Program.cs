using IterLib;
using MyTreeMapRB;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
public interface MyCollection<T>
{
    void Add(T e);
    void AddAll(MyCollection<T> c);
    void Clear();
    bool Contains(object o);
    bool ContainsAll(MyCollection<T> c);
    bool IsEmpty();
    bool Remove(object o);
    bool RemoveAll(MyCollection<T> c);
    bool RetainAll(MyCollection<T> c);
    int Size();
    T[] ToArray();
    T[] ToArray(T[] a);
}

// Расширение для списка
public interface MyListIt<T> : MyCollection<T>
{
    void Add(int index, T e);
    void AddAll(int index, MyCollection<T> c);
    T Get(int index);
    int IndexOf(object o);
    int LastIndexOf(object o);
    MyListIterator<T> ListIterator();
    MyListIterator<T> ListIterator(int index);
    T RemoveAt(int index);
    T Set(int index, T e);
    MyListIt<T> SubList(int fromIndex, int toIndex);
}

// Реализация списка MyArrayList
public class MyArrayList<T> : MyListIt<T>
{
    private T[] elements;
    private int size;

    public MyArrayList()
    {
        elements = new T[10];
        size = 0;
    }

    public MyArrayList(int capacity) //создания пустого динамического массива с внутренним массивом, размер которого будет равен значению параметра capacity.
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));
        elements = new T[capacity];
        size = 0;
    }

    public MyArrayList(MyCollection<T> c) : this()
    {
        AddAll(0, c);
    }

    public void Add(T e)
    {
        if (size == elements.Length)
        {
            int newCapacity = elements.Length == 0 ? 1 : elements.Length * 2;
            T[] newData = new T[newCapacity];
            Array.Copy(elements, newData, size);
            elements = newData;
        }
        elements[size++] = e;
    }

    public void AddAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(item);
        }
    }

    public void Add(int index, T e)
    {
        if (index < 0 || index > size) throw new IndexOutOfRangeException();
        EnsureCapacity();
        Array.Copy(elements, index, elements, index + 1, size - index);
        elements[index] = e;
        size++;
    }

    public void AddAll(int index, MyCollection<T> c)
    {
        if (index < 0 || index > size) throw new IndexOutOfRangeException();
        foreach (var item in c.ToArray())
            Add(index++, item);
    }

    public void Clear()
    {
        elements = new T[10];
        size = 0;
    }

    public bool Contains(object o)
    {
        return IndexOf(o) >= 0;
    }

    public bool ContainsAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            if (!Contains(item)) return false;
        }
        return true;
    }
    public bool IsEmpty() //для проверки, является ли динамический массив пустым.
    {
        if (size == 0)
            return true;
        else return false;
    }

    public int IndexOf(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (Equals(elements[i], o))
            {
                return i;
            }
        }
        return -1;
    }

    public int LastIndexOf(object o)
    {
        for (int i = size - 1; i >= 0; i--)
        {
            if (Equals(elements[i], o))
            {
                return i;
            }
        }
        return -1;
    }

    public int Size()
    {
        return size;
    }

    public T[] ToArray()
    {
        T[] result = new T[size];
        Array.Copy(elements, result, size);
        return result;
    }

    public T[] ToArray(T[] a)
    {
        if (a.Length < size)
        {
            return elements.Take(size).OfType<T>().ToArray();
        }
        Array.Copy(elements, a, size);
        if (a.Length > size)
        {
            a[size] = default;
        }
        return a;
    }

    private void EnsureCapacity()
    {
        if (size == elements.Length)
        {
            Array.Resize(ref elements, elements.Length * 2);
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= size) throw new IndexOutOfRangeException();
        return elements[index];
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= size) throw new IndexOutOfRangeException();
        T removedElement = elements[index];
        Array.Copy(elements, index + 1, elements, index, size - index - 1);
        elements[--size] = default;
        return removedElement;
    }

    public bool Remove(object o)
    {
        int index = IndexOf(o);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }
        return false;
    }

    public T Set(int index, T e)
    {
        if (index < 0 || index >= size) throw new IndexOutOfRangeException();
        T oldValue = elements[index];
        elements[index] = e;
        return oldValue;
    }

    public bool RemoveAll(MyCollection<T> c)
    {
        bool modified = false;
        foreach (var item in c.ToArray())
        {
            while (Remove(item))
            {
                modified = true;
            }
        }
        return modified;
    }

    public bool RetainAll(MyCollection<T> c)
    {
        bool modified = false;
        for (int i = 0; i < size; i++)
        {
            if (!c.Contains(elements[i]))
            {
                RemoveAt(i);
                i--;
                modified = true;
            }
        }
        return modified;
    }

    public MyListIt<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
        {
            throw new IndexOutOfRangeException();
        }
        MyArrayList<T> subList = new MyArrayList<T>();
        for (int i = fromIndex; i < toIndex; i++)
        {
            subList.Add(elements[i]);
        }
        return subList;
    }

    public MyListIterator<T> ListIterator()
    {
        return new MyArrayListIterator(this);
    }

    public MyListIterator<T> ListIterator(int index)
    {
        if (index < 0 || index > Size())
            throw new IterArgumentOutOfRangeException(nameof(index));

        MyArrayListIterator iterator = new MyArrayListIterator(this);
        // Инициализируем курсор итератора на указанной позиции
        while (iterator.HasNext() && iterator.NextIndex() < index)
        {
            iterator.Next();
        }
        return iterator;
    }

    private class MyArrayListIterator : MyListIterator<T>
    {
        private readonly MyArrayList<T> _collection;
        private int _cursor = 0; // Указатель на текущий элемент
        private bool canRemoveOrSet = false; // Контролирует допустимость вызова Remove() или Set()

        public MyArrayListIterator(MyArrayList<T> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public bool HasNext()
        {
            return _cursor < _collection.Size();
        }

        public T Next()
        {
            if (!HasNext())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(_cursor++);
        }

        public void Remove()
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot remove before calling Next()");

            _cursor--;
            _collection.RemoveAt(_cursor);
            canRemoveOrSet = false; // После удаления нельзя снова вызывать Remove() или Set()
        }

        public bool HasPrevious()
        {
            return _cursor > 0;
        }

        public T Previous()
        {
            if (!HasPrevious())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(--_cursor);
        }

        public int NextIndex()
        {
            return _cursor;
        }

        public int PreviousIndex()
        {
            return _cursor - 1;
        }

        public void Set(T element)
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot set before calling Next()");

            _collection.Set(_cursor - 1, element);
        }

        public void Add(T element)
        {
            _collection.Add(_cursor++, element);
            canRemoveOrSet = false; // После добавления нельзя вызывать Remove() или Set()
        }
    }
}

// Реализация списка MyVector

public class MyVector<T> : MyListIt<T>
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
        if (elementCount == 0)
        {
            elementData = new T[capacityIncrement > 0 ? capacityIncrement : 10];
        }

        if (elementCount == elementData.Length)
        {
            T[]? newArray = null;
            if (capacityIncrement != 0)
                newArray = new T[elementCount + capacityIncrement];
            else
                newArray = new T[elementCount * 2];
            Array.Copy(elementData, newArray, elementCount);
            elementData = newArray;
        }
        elementData[elementCount] = e;
        elementCount++;
    }

    private void EnsureCapacity(int minCapacity)
    {
        if (elementData.Length < minCapacity)
        {
            int newCapacity = (capacityIncrement > 0) ? elementData.Length + capacityIncrement : elementData.Length * 2;
            if (newCapacity < minCapacity) newCapacity = minCapacity;
            T[] newArray = new T[newCapacity];
            Array.Copy(elementData, newArray, elementCount);
            elementData = newArray;
        }
    }

    public void AddAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(item);
        }
    }

    public void AddAll(int index, MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(index, item);
            index++;
        }
    }
    public void Clear() // для удаления всех элементов из вектора
    {
        elementData = null;
        elementCount = 0;
    }
    public bool Contains(object o) //  для проверки, находится ли указанный объект в векторе
    {
        if (elementCount != 0)
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
    public bool ContainsAll(MyCollection<T> c) // для проверки, содержатся ли указанные объекты в векторе
    {
        bool f = false;
        foreach (T item in c.ToArray())
            if (item != null)
                f = Contains(item);
        return f;
    }
    public bool IsEmpty() // для проверки, является ли вектор пустым
    {
        if (elementCount == 0)
            return true;
        return false;
    }
    public bool Remove(object o) // Удаление указанного объекта из вектора
    {
        if (o == null || elementCount == 0)
        {
            return false;
        }

        int index = IndexOf(o);
        if (index >= 0)
        {
            RemoveAt(index); // Используем RemoveInd для удаления по индексу
        }
        return true;
    }

    public bool RemoveAll(MyCollection<T> c)
    {
        bool modified = false;
        foreach (var item in c.ToArray())
        {
            while (Remove(item))
            {
                modified = true;
            }
        }
        return modified;
    }
    public bool RetainAll(MyCollection<T> c)
    {
        bool modified = false;
        for (int i = 0; i < elementCount; i++)
        {
            if (!c.Contains(elementData[i]))
            {
                RemoveAt(i);
                i--;
                modified = true;
            }
        }
        return modified;
    }
    public int Size() // для получения размера вектора в элементах
    {
        return elementCount;
    }
    public T[] ToArray()
    {
        T[] result = new T[elementCount];
        Array.Copy(elementData, result, elementCount);
        return result;
    }
    public T[] ToArray(T[] a)
    {
        if (a.Length < elementCount)
        {
            return ToArray() as T[] ?? throw new InvalidOperationException();
        }

        Array.Copy(elementData, a, elementCount);
        if (a.Length > elementCount)
        {
            a[elementCount] = default!;
        }

        return a;
    }
    public void Add(int index, T e) // для добавления элемента в указанную позицию
    {
        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException("index");

        if (elementCount == 0)
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
        if (elementCount == 0)
            throw new InvalidOperationException("elementData is Empty");

        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException(nameof(index));
        return elementData[index];
    }
    public int IndexOf(object o) //
    {
        if (elementCount == 0)
            throw new InvalidOperationException("elementData is Empty");

        for (int i = 0; i < elementCount; i++)
            if (o != null && o.Equals(elementData[i]))
                return i;
        return -1;
    }
    public int LastIndexOf(object o) // для нахождения последнего вхождения указанного объекта, или -1, если его нет в векторе
    {
        if (elementCount == 0)
            throw new InvalidOperationException("elementData is Empty");

        for (int i = elementCount - 1; i >= 0; i--)
            if (o.Equals(elementData[i]))
                return i;
        return -1;
    }
    public T RemoveAt(int index) // Удаление элемента по индексу
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
    public T Set(int index, T e) // для замены элемента в указанной позиции новым элементом
    {
        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        T oldValue = elementData[index];
        elementData[index] = e;
        return oldValue;
    }
    public MyListIt<T> SubList(int fromIndex, int toIndex) // для возвращения части вектора
    {
        if (elementCount == 0)
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
        if (elementCount == 0)
            throw new InvalidOperationException("elementData is Empty");
        return elementData[0];
    }

    public T LastElement()  // для обращения к последнему элементу вектора
    {
        if (elementCount == 0)
            throw new InvalidOperationException("elementData is Empty");
        return elementData[elementCount - 1];
    }

    public void RemoveElementAt(int pos)  // для удаления элемента в заданной позиции
    {
        if (elementCount != 0)
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
        if (elementCount != 0)
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
        if (elementCount != 0)
        {
            for (int i = 0; i < elementCount; i++)
                Console.Write($"{elementData[i]} ");
            Console.WriteLine();
        }
        else Console.WriteLine("Empty vector");
    }

    public MyListIterator<T> ListIterator()
    {
        return new MyVectorIterator(this);
    }

    public MyListIterator<T> ListIterator(int index)
    {
        if (index < 0 || index > Size())
            throw new IterArgumentOutOfRangeException(nameof(index));

        MyVectorIterator iterator = new MyVectorIterator(this);
        while (iterator.HasNext() && iterator.NextIndex() < index)
        {
            iterator.Next();
        }
        return iterator;
    }

    private class MyVectorIterator : MyListIterator<T>
    {
        private readonly MyVector<T> _collection;
        private int _cursor = 0; // Указатель на текущий элемент
        private bool canRemoveOrSet = false; // Контролирует допустимость вызова Remove() или Set()

        public MyVectorIterator(MyVector<T> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public bool HasNext()
        {
            return _cursor < _collection.Size();
        }

        public T Next()
        {
            if (!HasNext())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(_cursor++);
        }

        public void Remove()
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot remove before calling Next()");

            _cursor--;
            _collection.RemoveAt(_cursor);
            canRemoveOrSet = false; // После удаления нельзя снова вызывать Remove() или Set()
        }

        public bool HasPrevious()
        {
            return _cursor > 0;
        }

        public T Previous()
        {
            if (!HasPrevious())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(--_cursor);
        }

        public int NextIndex()
        {
            return _cursor;
        }

        public int PreviousIndex()
        {
            return _cursor - 1;
        }

        public void Set(T element)
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot set before calling Next()");

            _collection.Set(_cursor - 1, element);
        }

        public void Add(T element)
        {
            _collection.Add(_cursor++, element);
            canRemoveOrSet = false; // После добавления нельзя вызывать Remove() или Set()
        }
    }
}


// Реализация MyLinkedList

public class MyLinkedList<T> : MyListIt<T>
{
    private class LinkElement
    {
        public T Value;
        public LinkElement? Next;
        public LinkElement? Prev;

        public LinkElement(T value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    private LinkElement? first;
    private LinkElement? last;
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
        var newElement = new LinkElement(e);
        if (last == null)
        {
            first = newElement;
            last = newElement;
        }
        else
        {
            last.Next = newElement;
            newElement.Prev = last;
            last = newElement;
        }
        size++;
    }

    // 4

    public void AddAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(item);
        }
    }
    public void AddAll(int index, MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(index, item);
            index++;
        }
    }
    // 5
    public void Clear()
    {
        first = null;
        last = null;
        size = 0;
    }
    // 6
    public bool Contains(object o)
    {
        if (o is T value)
        {
            var current = first;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    return true;
                }
                current = current.Next;
            }
        }
        return false;
    }
    // 7
    public bool ContainsAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            if (!Contains(item))
            {
                return false;
            }
        }
        return true;
    }
    // 8
    public bool IsEmpty() => size == 0;
    // 9
    public bool Remove(object o)
    {
        if (o is T value)
        {
            var current = first;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    RemoveElement(current);
                    return true;
                }
                current = current.Next;
            }
        }
        return false;
    }
    private void RemoveElement(LinkElement element)
    {
        if (element.Prev != null)
        {
            element.Prev.Next = element.Next;
        }
        else
        {
            first = element.Next;
        }

        if (element.Next != null)
        {
            element.Next.Prev = element.Prev;
        }
        else
        {
            last = element.Prev;
        }

        size--;
    }
    // 10
    public bool RemoveAll(MyCollection<T> c)
    {
        bool modified = false;
        foreach (var item in c.ToArray())
        {
            if (Remove(item))
            {
                modified = true;
            }
        }
        return modified;
    }
    // 11
    public bool RetainAll(MyCollection<T> c)
    {
        bool modified = false;
        var current = first;
        while (current != null)
        {
            var next = current.Next;
            if (!c.Contains(current.Value))
            {
                RemoveElement(current);
                modified = true;
            }
            current = next;
        }
        return modified;
    }
    // 12
    public int Size() => size;
    // 13
    public T[] ToArray()
    {
        var array = new T[size];
        var current = first;
        int index = 0;
        while (current != null)
        {
            array[index++] = current.Value;
            current = current.Next;
        }
        return array;
    }
    // 14
    public T[] ToArray(T[] a)
    {
        var array = ToArray();
        var result = new T[array.Length];
        Array.Copy(array, result, array.Length);
        return result;
    }
    // 15
    public void Add(int index, T e)
    {
        if (index < 0 || index > size) throw new IndexOutOfRangeException();

        if (index == size)
        {
            Add(e);
            return;
        }

        var newElement = new LinkElement(e);
        if (index == 0)
        {
            newElement.Next = first;
            first.Prev = newElement;
            first = newElement;
        }
        else
        {
            var current = GetElementAt(index);
            newElement.Next = current;
            newElement.Prev = current.Prev;
            current.Prev.Next = newElement;
            current.Prev = newElement;
        }
        size++;
    }
    // 17
    public T Get(int index)
    {
        var element = GetElementAt(index);
        return element.Value;
    }
    // 18
    public int IndexOf(object o)
    {
        if (o is T value)
        {
            var current = first;
            int index = 0;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    return index;
                }
                current = current.Next;
                index++;
            }
        }
        return -1;
    }
    // 19
    public int LastIndexOf(object o)
    {
        if (o is T value)
        {
            var current = last;
            int index = size - 1;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    return index;
                }
                current = current.Prev;
                index--;
            }
        }
        return -1;
    }
    // 20
    public T RemoveAt(int index)
    {
        var element = GetElementAt(index);
        var value = element.Value;
        RemoveElement(element);
        return value;
    }
    // 21
    public T Set(int index, T e)
    {
        var element = GetElementAt(index);
        var oldValue = element.Value;
        element.Value = e;
        return oldValue;
    }

    // 22
    public MyListIt<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
        {
            throw new ArgumentOutOfRangeException();
        }

        var subList = new MyLinkedList<T>();
        var current = GetElementAt(fromIndex);
        for (int i = fromIndex; i < toIndex; i++)
        {
            subList.Add(current.Value);
            current = current.Next;
        }
        return subList;
    }

    private LinkElement GetElementAt(int index)
    {
        if (index < 0 || index >= size) throw new IndexOutOfRangeException();

        var current = first;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }
        return current;
    }

    // 23
    public T Element() => first.Value;
    // 24
    public bool Offer(T obj)
    {
        Add(obj);
        return true;
    }
    // 25
    public T? Peek()
    {
        return first != null ? first.Value : default(T);
    }

    // 26
    public T? Poll()
    {
        if (first == null) return default;
        T value = first.Value;
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
        return first.Value;
    }
    // 30
    public T GetLast()
    {
        if (first == null)
            throw new NullReferenceException();
        return last.Value;
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
        T item = first.Value;
        Remove(item);
        return item;
    }
    // 34
    public void Push(T obj) => AddFirst(obj);
    // 35
    public T? PeekFirst() => first != null ? first.Value : default(T);

    // 36
    public T? PeekLast() => last != null ? last.Value : default(T);

    // 37
    public T? PollFirst()
    {
        if (first == null) return default(T);
        T value = first.Value;
        Remove(value);
        return value;
    }
    // 38
    public T? PollLast()
    {
        if (last == null) return default(T);
        T value = last.Value;
        Remove(value);
        return value;
    }
    // 39
    public T RemoveLast()
    {
        if (last == null)
            throw new InvalidOperationException("Список пуст.");

        T item = last.Value;

        if (size == 1)
        {
            first = null;
            last = null;
        }
        else
        {
            last = last.Prev;
            last.Next = null;
        }
        size--;
        return item;
    }
    // 40
    public T RemoveFirst()
    {
        if (first == null)
            throw new InvalidOperationException("Список пуст.");

        T item = first.Value;

        if (size == 1)
        {
            first = null;
            last = null;
        }
        else
        {
            first = first.Next;
            first.Prev = null;
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
            RemoveAt(index);
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
            RemoveAt(index);
            return true;
        }
        return false;
    }
    public void PrintLinkList()
    {
        if (first != null)
        {
            LinkElement step = new LinkElement(first.Value);
            step = first;
            while (step != null)
            {
                Console.WriteLine($"{step.Value}");
                step = step.Next;
            }
        }
        else Console.WriteLine("Empty list");
    }

    public MyListIterator<T> ListIterator()
    {
        return new MyLinkIterator(this);
    }

    public MyListIterator<T> ListIterator(int index)
    {
        if (index < 0 || index > Size())
            throw new IterArgumentOutOfRangeException(nameof(index));

        MyLinkIterator iterator = new MyLinkIterator(this);
        while (iterator.HasNext() && iterator.NextIndex() < index)
        {
            iterator.Next();
        }
        return iterator;
    }

    private class MyLinkIterator : MyListIterator<T>
    {
        private readonly MyLinkedList<T> _collection;
        private int _cursor = 0; // Указатель на текущий элемент
        private bool canRemoveOrSet = false; // Контролирует допустимость вызова Remove() или Set()

        public MyLinkIterator(MyLinkedList<T> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public bool HasNext()
        {
            return _cursor < _collection.Size();
        }

        public T Next()
        {
            if (!HasNext())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(_cursor++);
        }

        public void Remove()
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot remove before calling Next()");

            _cursor--;
            _collection.RemoveAt(_cursor);
            canRemoveOrSet = false; // После удаления нельзя снова вызывать Remove() или Set()
        }

        public bool HasPrevious()
        {
            return _cursor > 0;
        }

        public T Previous()
        {
            if (!HasPrevious())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(--_cursor);
        }

        public int NextIndex()
        {
            return _cursor;
        }

        public int PreviousIndex()
        {
            return _cursor - 1;
        }

        public void Set(T element)
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot set before calling Next()");

            _collection.Set(_cursor - 1, element);
        }

        public void Add(T element)
        {
            _collection.Add(_cursor++, element);
            canRemoveOrSet = false; // После добавления нельзя вызывать Remove() или Set()
        }
    }
}


// Интерфейс для очередей
public interface MyQueue<T> : MyCollection<T>
{
    T Element();
    bool Offer(T obj);
    T Peek();
    T Poll();
}

// Реализация MyPriorityQueue
public class MyPriorityQueue<T> : MyQueue<T>
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
            if ((rightChild < size) && (queue.Get(rightChild) != null) && (queue.Get(parent) != null) && (comparator.Compare(queue.Get(rightChild), queue.Get(parent))) > 0)
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
        queue = new MyArrayList<T>();
        size = 0;
        comparator = Comparer<T>.Default;
    }
    public MyPriorityQueue(MyCollection<T> c) //Конструктор для создания очереди с приоритетами, содержащей элементы масива а
    {
        queue = new MyArrayList<T>();
        comparator = Comparer<T>.Default;
        AddAll(c);
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
    public void Add(int index, T e) => queue.Add(index, e);
    public int LastIndexOf(object obj) => queue.LastIndexOf(obj);
    public int IndexOf(object obj) => queue.IndexOf(obj);
    public void AddAll(MyCollection<T> c) //для добавления элементов из массива
    {
        foreach (var item in c.ToArray())
        {
            Add(item);
        }
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
    }
    public void Clear() //для удаления всех элементов из очереди с приоритетами
    {
        queue.Clear();
        size = 0;
    }
    public bool Contains(object o) //для проверки, находится ли указанный объект в очереди с приоритетами
    {
        return queue.Contains(o);
    }
    public bool ContainsAll(MyCollection<T> c) //для проверки, содержатся ли указанные объекты в очереди с приоритетами
    {
        return queue.ContainsAll(c);
    }
    public bool IsEmpty() //для проверки, является ли очередь с приоритетами пустой
    {
        if (size == 0)
            return true;
        else return false;

    }
    public bool Remove(object o) // для удаления указанного объекта из очереди с приоритетами, если он есть там
    {
        int ind = queue.IndexOf(o);
        if (ind == -1) return false;
        queue.RemoveAt(ind);
        size--;
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
        return true;
    }
    public bool RemoveAll(MyCollection<T> c) //для удаления указанных объектов из очереди с приоритетами
    {
        foreach (T item in c.ToArray())
            Remove(item);
        return true;
    }
    public bool RetainAll(MyCollection<T> c) //для оставления в очереди с приоритетами только указанных объектов
    {
        if (size == 0)
            return false;
        MyArrayList<T> old = queue;
        Clear();
        foreach (T item in c.ToArray())
            if (old.Contains(item))
                Add(item);
        return true;
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
        queue.RemoveAt(size - 1);
        size--;
        for (int i = 0; i < size; i++)
            RestoringHeap(i);
        return element;
    }
    public T GetElem(int index)
    {
        return queue.Get(index);
    }
    public void Replace(int index, T element)
    {
        queue.Set(index, element);
    }
    public int GetIndex(T elem)
    {
        if (Contains(elem))
        {
            return queue.IndexOf(elem);
        }
        else return -1;
    }
    public T RemoveAt(int index)
    {
        T item = queue.RemoveAt(index);
        size--;
        return item;
    }
    public void PrintQueue()
    {
        for (int i = 0; i < size; i++)
            Console.Write(queue.Get(i) + " ");
        Console.WriteLine();
    }
    public MyIterator<T> Iterator() => new MyItr(this);

    public class MyItr : MyIterator<T>
    {
        private readonly MyPriorityQueue<T> _collection;
        private int _cursor = -1;  // Указатель на текущий элемент
        private bool _canRemove = false;

        public MyItr(MyPriorityQueue<T> collection)
        {
            _collection = collection;
        }

        // Проверка наличия следующего элемента
        public bool HasNext() => _cursor + 1 < _collection.Size();

        // Переход к следующему элементу
        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");

            _canRemove = true;  // Разрешение на удаление текущего элемента
            _cursor++;
            return _collection.queue.Get(_cursor); // Доступ к элементу через queue
        }

        // Удаление текущего элемента
        public void Remove()
        {
            if (!_canRemove)
                throw new InvalidOperationException("Нельзя удалить элемент.");

            _collection.queue.RemoveAt(_cursor); // Удаление через queue
            _cursor--;  // Смещение назад после удаления
            _collection.size--;  // Уменьшение размера коллекции
            _canRemove = false;  // Запрет повторного удаления
        }
    }
    public MyListIt<T> SubList(int fromIndex, int toIndex) => queue.SubList(fromIndex, toIndex);
}

public interface MyDeque<T> : MyCollection<T>
{
    void AddFirst(T obj);
    void AddLast(T obj);
    T GetFirst();
    T GetLast();
    bool OfferFirst(T obj);
    bool OfferLast(T obj);
    T Pор();
    void Push(T obj);
    T PeekFirst();
    T PeekLast();
    T PollFirst();
    T PollLast();
    T RemoveLast();
    T RemoveFirst();
    bool RemoveLastOccurrence(T obj);
    bool RemoveFirstOccurrence(T obj);
}

public class MyArrayDeque<T> : MyListIt<T>, MyDeque<T>
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
    public MyArrayDeque(MyCollection<T> c)
    {
        MyPriorityQueue<T> newArray = new MyPriorityQueue<T>(c);
        head = 0;
        tail = newArray.Size();
        newArray.AddAll(c);
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
    public void AddAll(MyCollection<T> c)
    {
        foreach (T item in c.ToArray())
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
    public bool Contains(object item) => elements.Contains(item);

    // 8
    public bool ContainsAll(MyCollection<T> c) => elements.ContainsAll(c);

    // 9
    public bool IsEmpty()
    {
        if (tail == 0)
            return true;
        else return false;
    }
    // 10
    public bool Remove(object item)
    {
        if (Contains(item))
        {
            elements.Remove(item);
            tail--;
            return true;
        }
        return false;
    }
    // 11
    public bool RemoveAll(MyCollection<T> c)
    {
        foreach (T item in c.ToArray())
        {
            if (Contains(item))
            {
                elements.Remove(c);
                tail--;
            }
        }
        return true;
    }
    // 12
    public bool RetainAll(MyCollection<T> c)
    {
        if (tail == 0)
            return false;
        MyPriorityQueue<T> old = elements;
        Clear();
        foreach (T item in c.ToArray())
            if (old.Contains(item))
                Add(item);
        return true;
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
    public T Pор()
    {
        T item = elements.GetElem(head);
        elements.RemoveAt(head);
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
            elements.RemoveAt(head);
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
            elements.RemoveAt(tail - 1);
            tail--;
            return element;
        }
    }
    // 32
    public T RemoveLast()
    {
        T element = elements.GetElem(tail - 1);
        elements.RemoveAt(tail - 1);
        tail--;
        return element;
    }
    // 33
    public T RemoveFirst()
    {
        T element = elements.GetElem(head);
        elements.RemoveAt(head);
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
            elements.RemoveAt(index);
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
            elements.RemoveAt(index);
            tail--;
            return true;
        }
        return false;
    }

    public T Get(int index) => elements.GetElem(index);

    // Print Deque
    public void PrintDeque()
    {
        for (int i = 0; i < Size(); i++)
        {
            Console.Write(elements.GetElem(i) + " ");
        }
        Console.WriteLine();
    }

    public T RemoveAt(int index) => elements.RemoveAt(index);

    public MyIterator<T> Iterator() => new MyItr(this);

    public class MyItr : MyIterator<T>
    {
        private readonly MyArrayDeque<T> _collection;
        private int _cursor = -1;
        private bool _canRemove = false;

        public MyItr(MyArrayDeque<T> collection)
        {
            _collection = collection;
        }

        // Проверка наличия следующего элемента
        public bool HasNext() => _cursor + 1 < _collection.Size();

        // Переход к следующему элементу
        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");

            _canRemove = true;
            _cursor++;
            return _collection.elements.GetElem(_cursor);
        }

        // Удаление текущего элемента
        public void Remove()
        {
            if (!_canRemove)
                throw new InvalidOperationException("Нельзя удалить элемент.");

            _collection.elements.RemoveAt(_cursor);
            _cursor--;
            _collection.tail--;
            _canRemove = false;
        }
    }

    public MyListIterator<T> ListIterator()
    {
        return new MyArrayDequeList(this);
    }

    public MyListIterator<T> ListIterator(int index)
    {
        if (index < 0 || index > Size())
            throw new IterArgumentOutOfRangeException(nameof(index));

        MyArrayDequeList iterator = new MyArrayDequeList(this);
        // Инициализируем курсор итератора на указанной позиции
        while (iterator.HasNext() && iterator.NextIndex() < index)
        {
            iterator.Next();
        }
        return iterator;
    }

    private class MyArrayDequeList : MyListIterator<T>
    {
        private readonly MyArrayDeque<T> _collection;
        private int _cursor = 0; // Указатель на текущий элемент
        private bool canRemoveOrSet = false; // Контролирует допустимость вызова Remove() или Set()

        public MyArrayDequeList(MyArrayDeque<T> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public bool HasNext()
        {
            return _cursor < _collection.Size();
        }

        public T Next()
        {
            if (!HasNext())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(_cursor++);
        }

        public void Remove()
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot remove before calling Next()");

            _cursor--;
            _collection.RemoveAt(_cursor);
            canRemoveOrSet = false; // После удаления нельзя снова вызывать Remove() или Set()
        }

        public bool HasPrevious()
        {
            return _cursor > 0;
        }

        public T Previous()
        {
            if (!HasPrevious())
                throw new IterNoSuchElementException();

            canRemoveOrSet = true;
            return _collection.Get(--_cursor);
        }

        public int NextIndex()
        {
            return _cursor;
        }

        public int PreviousIndex()
        {
            return _cursor - 1;
        }

        public void Set(T element)
        {
            if (!canRemoveOrSet)
                throw new IterIllegalStateException("Cannot set before calling Next()");

            _collection.Set(_cursor - 1, element);
        }

        public void Add(T element)
        {
            _collection.Add(_cursor++, element);
            canRemoveOrSet = false; // После добавления нельзя вызывать Remove() или Set()
        }
    }

    public int IndexOf(object o) => elements.IndexOf(o);
    public T Set(int index, T e)
    {
        var element = Get(index);
        var oldValue = element;
        element = e;
        return oldValue;
    }

    public MyListIt<T> SubList(int fromIndex, int toIndex) => elements.SubList(fromIndex, toIndex);
    public void Add(int index, T e) => elements.Add(index, e);

    public void AddAll(int index, MyCollection<T> c)
    {
        if (index < 0 || index > tail) throw new IndexOutOfRangeException();
        foreach (var item in c.ToArray())
            Add(index++, item);
    }
    public int LastIndexOf(object obj) => elements.LastIndexOf(obj);
}


public interface MyMap<K, V>
{
    void Clear();
    bool ContainsKey(object key);
    bool ContainsValue(object value);
    ICollection<KeyValuePair<K, V>> EntrySet();
    V Get(object key);
    bool IsEmpty();
    ICollection<K> KeySet();
    void Put(K key, V value);
    void PutAll(MyMap<K, V> m);
    void Remove(object key);
    int Size();
}

public class MyHashMap<K, V> : MyMap<K, V>
{
    private class Entry
    {
        public K Key { get; }
        public V Value { get; set; }
        public Entry? Next { get; set; }

        public Entry(K key, V value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

    private Entry?[] table;
    private int size;
    private float loadFactor;
    // конструктор MyHashMap() для создания пустого отображения с начальной ёмкостью 16 и коэффициентом загрузки 0,75;
    public MyHashMap() : this(16, 0.75f) { }
    // конструктор MyHashMap(int initialCapacity) для создания пустого отображения с указанной начальной ёмкостью и коэффициентом загрузки 0,75;
    public MyHashMap(int initialCapacity) : this(initialCapacity, 0.75f) { }
    // конструктор MyHashMap(int initialCapacity, float loadFactor) для создания пустого отображения с указанной начальной ёмкостью и коэффициентом загрузки;
    public MyHashMap(int initialCapacity, float loadFactor)
    {
        if (initialCapacity <= 0 || loadFactor <= 0)
            throw new ArgumentException("Invalid");

        table = new Entry[initialCapacity];
        this.loadFactor = loadFactor;
        size = 0;
    }
    // метод clear() для удаления всех пар «ключ-значение» из отображения
    public void Clear()
    {
        table = new Entry[table.Length];
        size = 0;
    }
    private int GetBucketIndex(object key)
    {
        int hashCode = key?.GetHashCode() ?? 0;
        return Math.Abs(hashCode) % table.Length;
    }
    //  метод containsKey(object key) для проверки, содержит ли отображение указанный ключ;
    public bool ContainsKey(object key)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        while (current != null)
        {
            if (EqualityComparer<object>.Default.Equals(current.Key, key))
                return true;
            current = current.Next;
        }
        return false;
    }
    // метод containsValue(object value) для проверки, содержит ли отображение указанное значение;
    public bool ContainsValue(object value)
    {
        foreach (var bucket in table)
        {
            Entry? current = bucket;
            while (current != null)
            {
                if (EqualityComparer<object>.Default.Equals(current.Value, value))
                    return true;
                current = current.Next;
            }
        }
        return false;
    }
    // метод entrySet() для возврата множества (Set) всех пар «ключ-значение» в отображении;
    public ICollection<KeyValuePair<K, V>> EntrySet()
    {
        var entries = new List<KeyValuePair<K, V>>();
        foreach (var bucket in table)
        {
            Entry? current = bucket;
            while (current != null)
            {
                entries.Add(new KeyValuePair<K, V>(current.Key, current.Value));
                current = current.Next;
            }
        }
        return entries;
    }
    // метод get(object key) для возврата значения, связанного с указанным ключом, или null, если ключ не найден
    public V Get(object key)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        while (current != null)
        {
            if (EqualityComparer<object>.Default.Equals(current.Key, key))
                return current.Value;
            current = current.Next;
        }
        return default;
    }
    // метод isEmpty() для проверки, является ли отображение пустым;
    public bool IsEmpty()
    {
        return size == 0;
    }
    //  метод keySet() для возврата множества (Set) всех ключей в отображении;
    public ICollection<K> KeySet()
    {
        var keys = new List<K>();
        foreach (var bucket in table)
        {
            Entry? current = bucket;
            while (current != null)
            {
                keys.Add(current.Key);
                current = current.Next;
            }
        }
        return keys;
    }
    private bool NeedsResize()
    {
        return size >= table.Length * loadFactor;
    }
    private void Resize()
    {
        int newCapacity = table.Length * 2;
        var newTable = new Entry[newCapacity];

        foreach (var bucket in table)
        {
            Entry? current = bucket;
            while (current != null)
            {
                int newIndex = Math.Abs(current.Key?.GetHashCode() ?? 0) % newCapacity;
                var next = current.Next;
                current.Next = newTable[newIndex];
                newTable[newIndex] = current;
                current = next;
            }
        }
        table = newTable;
    }
    // метод put(K key, V value) для добавления пары «ключ-значение» в отображение;
    public void Put(K key, V value)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        while (current != null)
        {
            if (EqualityComparer<K>.Default.Equals(current.Key, key))
            {
                current.Value = value;
                return;
            }
            current = current.Next;
        }

        var newEntry = new Entry(key, value) { Next = table[index] };
        table[index] = newEntry;
        size++;

        if (NeedsResize())
            Resize();
    }
    public void PutAll(MyMap<K, V> m)
    {
        foreach (var keys in m.KeySet())
        {
            Put(keys, Get(keys));
        }
    }
    // метод remove(object key) для удаления пары «ключ-значение» с указанным ключом из отображения;
    public void Remove(object key)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        Entry? previous = null;

        while (current != null)
        {
            if (EqualityComparer<object>.Default.Equals(current.Key, key))
            {
                if (previous == null)
                {
                    table[index] = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                size--;
                return;
            }
            previous = current;
            current = current.Next;
        }
    }
    //  метод size() для возврата количества пар «ключ-значение» в отображении
    public int Size()
    {
        return size;
    }
}



public interface MySet<T> : MyCollection<T>
{
    T First();
    T Last();
    MySet<T> SubSet(T fromElement, T toElement);
    MySet<T> HeadSet(T toElement);
    MySet<T> TailSet(T fromElement);
}

public class MyHashSet<T> where T : IComparable, MySet<T>
{
    private MyHashMap<T, object> map;

    // Конструктор MyHashSet() для создания пустого множества с начальной ёмкостью 16 и коэффициентом загрузки 0,75
    public MyHashSet() : this(16, 0.75f) { }
    // Конструктор MyHashSet(T[] a) для создания множества, содержащего элементы указанной коллекции
    public MyHashSet(T[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Input array cannot be null.");
        }
        map = new MyHashMap<T, object>();

        foreach (T item in array)
        {
            map.Put(item, false);
        }
    }
    // Конструктор MyHashSet(int initialCapacity, float loadFactor) для создания пустого множества с указанной начальной ёмкостью и коэффициентом загрузки.
    public MyHashSet(int initialCapacity, float loadFactor) => map = new MyHashMap<T, object>(initialCapacity, loadFactor);
    // Конструктор MyHashSet(int initialCapacity) для создания пустого множества с указанной начальной ёмкостью и коэффициентом загрузки 0,75
    public MyHashSet(int initialCapacity) : this(initialCapacity, 0.75f) { }
    // Метод add(T e) для добавления элемента в конец множества.
    public void Add(T e) => map.Put(e, false);
    // Метод addAll(T[] a) для добавления элементов из массива
    public void AddAll(T[] a)
    {
        foreach (T item in a)
            map.Put(item, false);
    }
    // Метод clear() для удаления всех элементов из множества
    public void Clear() => map.Clear();
    // Метод contains(object o) для проверки, находится ли указанный объект во множестве
    public bool Contains(T o) => map.ContainsKey(o);
    // Метод containsAll(T[] a) для проверки, содержатся ли указанные объекты во множестве
    public bool ContainsAll(T[] a)
    {
        if (a == null || a.Length == 0)
        {
            return true;
        }
        if (map == null || map.IsEmpty())
        {
            return false;
        }
        foreach (T item in a)
        {
            if (!map.ContainsKey(item))
            {
                return false;
            }
        }
        return true;
    }
    // Метод isEmpty() для проверки, является ли множество пустым.
    public bool IsEmpty() => map.IsEmpty();
    // Метод remove(object o) для удаления указанного объекта из множества, если он есть там.
    public void Remove(T o) => map.Remove(o);
    // Метод removeAll(T[] a) для удаления указанных объектов из множества
    public void RemoveAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));
        foreach (T item in a)
            map.Remove(item);
    }
    // Метод retainAll(T[] a) для оставления во множестве только указанных объектов.
    public void RetainAll(T[] a)
    {
        if (a == null || a.Length == 0)
        {
            Clear();
            return;
        }
        var keySet = map.KeySet();
        if (keySet == null || keySet.Count == 0)
        {
            return;
        }
        var retainSet = new HashSet<T>(a);
        foreach (var item in keySet.ToArray())
        {
            if (!retainSet.Contains(item))
            {
                map.Remove(item);
            }
        }
    }
    // Метод size() для получения размера множества в элементах
    public int Size() => map.Size();
    //  Метод toArray() для возвращения массива объектов, содержащего все элементы множества.
    public T[] ToArray()
    {
        return map.KeySet().ToArray();
    }
    // Метод toArray(T[] a) для возвращения массива объектов, содержащего все элементы множества.Если аргумент a равен null, то создаётся новый массив, в который копируются элементы
    public T[] ToArray(T[] a)
    {
        if (a == null)
            return ToArray();
        var combined = a.Concat(map.KeySet()).ToArray();
        return combined;
    }
    // Метод first() для возврата первого (наименьшего) элемента множества.
    public T? First()
    {
        var array = map.KeySet().ToArray();
        if (array.Length == 0)
            throw new InvalidOperationException("Set is empty");
        return array.Min();
    }
    //  Метод last() для возврата последнего (наивысшего) элемента множества
    public T? Last()
    {
        var array = map.KeySet().ToArray();
        if (array.Length == 0)
            throw new InvalidOperationException("Set is empty");
        return array.Max();
    }
    // Метод subSet(E fromElement, E toElement) для возврата подмножества элементов из диапазона[fromElement; toElement).

    public MyHashSet<T> SubSet(T fromElement, T toElement)
    {
        var subset = new MyHashSet<T>();
        foreach (var item in map.KeySet())
        {
            if (item.CompareTo(fromElement) >= 0 && item.CompareTo(toElement) < 0)
            {
                subset.Add(item);
            }
        }
        return subset;
    }
    // Метод headSet(E toElement) для возврата множества элементов, меньших чем указанный элемент.
    public MyHashSet<T> HeadSet(T toElement)
    {
        var headset = new MyHashSet<T>();
        foreach (var item in map.KeySet())
        {
            if (item.CompareTo(toElement) < 0)
            {
                headset.Add(item);
            }
        }
        return headset;
    }
    // Метод tailSet(E fromElement) для возврата части множества из элементов, больших или равных указанному элементу
    public MyHashSet<T> TailSet(T fromElement)
    {
        var tailset = new MyHashSet<T>();
        foreach (var item in map.KeySet())
        {
            if (item.CompareTo(fromElement) >= 0)
            {
                tailset.Add(item);
            }
        }
        return tailset;
    }

    public MyIterator<T> Iterator() => new MyItr(this);

    public class MyItr : MyIterator<T>
    {
        private readonly T[] _elements;
        private int _cursor = -1;
        private bool _canRemove = false;
        private readonly MyHashSet<T> _collection;

        public MyItr(MyHashSet<T> collection)
        {
            _collection = collection;
            _elements = collection.ToArray();
        }

        public bool HasNext() => _cursor + 1 < _elements.Length;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No next element.");
            _canRemove = true;
            _cursor++;
            return _elements[_cursor];
        }

        public void Remove()
        {
            if (!_canRemove)
                throw new InvalidOperationException("Cannot remove element.");
            _collection.Remove(_elements[_cursor]);
            _canRemove = false;
        }
    }
}

public interface MySortedSet<T> : MySet<T>
{
    T First();
    T Last();
}

public interface MyNavigableSet <T> : MySortedSet<T>
{
    T Lower(T key);
    T Floor(T key);
    T Higher(T key);
    T Ceiling(T key);
    T PollFirst();
    T PollLast();
}

public class MyTreeSet<T> : IEnumerable<T>, MyNavigableSet<T>
{ 
    public enum Color { Red, Black }
    private MyTreeMapRB<T, object> map;
    // Конструктор MyTreeSet() для создания пустого множества, размещающего элементы согласно естественному порядку сортировки.
    public MyTreeSet()
    {
        map = new MyTreeMapRB<T, object>();
    }
    // Конструктор MyTreeSet(MyTreeMap<E, object> m) для создания множества, использующего указанный объект MyTreeMap для хранения элементов.
    public MyTreeSet(MyTreeMapRB<T, object> m)
    {
        map = m ?? throw new ArgumentNullException(nameof(m));
    }
    // Конструктор MyTreeSet(TreeMapComparator comparator) для создания пустого множества, размещающего элементы согласно указанному компаратору.
    public MyTreeSet(Comparer<T> comparator)
    {
        map = new MyTreeMapRB<T, object>(comparator);
    }
    // Конструктор MyTreeSet(T[] a) для создания множества, содержащего элементы указанной коллекции.
    public MyTreeSet(MyCollection<T> a)
    {
        map = new MyTreeMapRB<T, object>();
        AddAll(a);
    }
    // Конструктор MyTreeSet(SortedSet<E> s) для создания множества, содержащего элементы указанного сортированного множества.
    public MyTreeSet(SortedSet<T> s)
    {
        map = new MyTreeMapRB<T, object>();
        foreach (var item in s)
        {
            map.Put(item, null);
        }
    }
    // Метод add(T e) для добавления элемента в конец множества.
    public void Add(T e)
    {
        map.Put(e, null);
    }

    // Метод addAll(T[] a) для добавления элементов из массива.
    public void AddAll(MyCollection<T> a)
    {
        foreach (var item in a.ToArray())
        {
            map.Put(item, null);
        }
    }
    // Метод clear() для удаления всех элементов из множества.
    public void Clear()
    {
        map.Clear();
    }
    // Метод contains(object o) для проверки, находится ли указанный объект во множестве.
    public bool Contains(object o)
    {
        if (o is T t)
        {
            return map.ContainsKey(t);
        }
        return false;
    }
    // Метод containsAll(T[] a) для проверки, содержатся ли указанные объекты во множестве.
    public bool ContainsAll(MyCollection<T> a)
    {
        foreach (var item in a.ToArray())
        {
            if (!map.ContainsKey(item)) return false;
        }
        return true;
    }
    // Метод isEmpty() для проверки, является ли множество пустым.
    public bool IsEmpty()
    {
        return map.IsEmpty();
    }
    // Метод remove(object o) для удаления указанного объекта из множества, если он есть там.
    public bool Remove(object o)
    {
        if (o is T t)
        {
            if (map.ContainsKey(t))
            {
                map.Remove(t);
                return true;
            }
        }
        return false;
    }
    // Метод removeAll(T[] a) для удаления указанных объектов из множества.
    public bool RemoveAll(MyCollection<T> a)
    {
        foreach (var item in a.ToArray())
        {
            map.Remove(item);
        }
        return true;
    }
    // Метод retainAll(T[] a) для оставления во множестве только указанных объектов.
    public bool RetainAll(MyCollection<T> a)
    {
        var toRemove = new List<T>();
        foreach (var item in map.KeySet())
        {
            if (Array.IndexOf(a.ToArray(), item) == -1)
            {
                toRemove.Add(item);
            }
        }
        foreach (var item in toRemove)
        {
            map.Remove(item);
        }
        return true;
    }
    // Метод size() для получения размера множества в элементах.
    public int Size()
    {
        return map.Size();
    }
    // Метод toArray() для возвращения массива объектов, содержащего все элементы множества.
    public T[] ToArray()
    {
        return ToArray(null);
    }
    // Метод toArray(T[] a) для возвращения массива объектов, содержащего все элементы множества.Если аргумент a равен null, то создаётся новый массив, в который копируются элементы.
    public T[] ToArray(T[] a)
    {
        var elements = new List<T>(map.KeySet());
        return elements.ToArray();
    }
    // Метод first() для возврата первого(наименьшего) элемента множества.
    public T First() => map.FirstKey();

    // Метод last() для возврата последнего(наивысшего) элемента множества.
    public T Last() => map.LastKey();
    // Метод subSet(E fromElement, E toElement) для возврата подмножества элементов из диапазона[fromElement; toElement).
    public MySet<T> SubSet(T fromElement, T toElement)
    {
        var result = new MyTreeSet<T>(map.SubMap(fromElement, toElement));
        return result;
    }
    // Метод headSet(E toElement) для возврата множества элементов, меньших чем указанный элемент.
    public MySet<T> HeadSet(T toElement)
    {
        var result = new MyTreeSet<T>(map.HeadMap(toElement));
        return result;
    }
    // Метод tailSet(E fromElement) для возврата части множества из элементов, больши или равных указанному элементу.
    public MySet<T> TailSet(T fromElement)
    {
        var result = new MyTreeSet<T>(map.TailMap(fromElement));
        return result;
    }
    // Метод ceiling(E obj) для поиска в наборе наименьшего элемента е, для которого истинно е >= obj.Если такой элемент найден, он возвращается.В противном случае возвращается null.
    public T Ceiling(T obj)
    {
        var entry = map.CeilingEntry(obj);
        if (entry.HasValue) return entry.Value.Key;
        return default!;
    }
    // Метод floor(E obj) для поиска в наборе наибольшего элемента е, для которого истинно е <= obj.Если такой элемент найден, он возвращается.В противном случае возвращается null.
    public T Floor(T obj)
    {
        var entry = map.FloorEntry(obj);
        if (entry.HasValue) return entry.Value.Key;
        return default!;
    }
    // Метод higher(E obj) для поиска в наборе наибольшего элемента е, для которого истинно е > obj.Если такой элемент найден, он возвращается.В противном случае возвращается null.
    public T Higher(T obj)
    {
        var entry = map.HigherEntry(obj);
        if (entry.HasValue) return entry.Value.Key;
        return default!;
    }
    // Метод lower(E obj) для поиска в наборе наименьшего элемента е, для которого истинно е<obj. Если такой элемент найден, он возвращается. В противном случае возвращается null.
    public T Lower(T obj)
    {
        var entry = map.LowerEntry(obj);
        if (entry.HasValue) return entry.Value.Key;
        return default!;
    }
    // Метод headSet(E upperBound, bool incl) для возврата множества, включающего все элементы вызывающего набора, меньшие upperBound.Результирующий набор поддерживается вызывающим набором.
    public MyTreeSet<T> HeadSet(T upperBound, bool incl)
    {
        var result = new MyTreeSet<T>(map.HeadMap(upperBound));
        return result;
    }
    // Метод subSet(E lowerBound, bool lowIncl, E upperBound, bool highIncl) для возврата NavigableSet, включающего все элементы вызывающего набора, которые больше lowerBound и меньше upperBound.Если lowIncl равно true, то элемент, равный lowerBound, включается.Если highIncl равно true, также включается элемент,равный upperBound.
    public MyTreeSet<T> SubSet(T lowerBound, bool lowIncl, T upperBound, bool highIncl)
    {
        var result = new MyTreeSet<T>(map.SubMap(lowerBound, upperBound));
        return result;
    }
    // Метод tailSet(E fromElement, bool inclusive) для возврата множества, включающего все элементы вызывающего набора, которые больше(или равны, если inclusive равно true) чем fromElement.Результирующий набор поддерживается вызывающим набором.
    public MyTreeSet<T> TailSet(T fromElement, bool inclusive)
    {
        var result = new MyTreeSet<T>(map.TailMap(fromElement));
        return result;
    }
    // Метод pollLast() для возврата последнего элемента, удаляя его в процессе. Поскольку набор сортирован, это будет элемент с наибольшим значением. Возвращает null в случае пустого набора.
    public T PollLast()
    {
        var entry = map.PollLastEntry();
        if (entry.HasValue)
        {
            return entry.Value.Key;
        }
        return default!;
    }
    // Метод pollFirst() для возврата первого элемента, удаляя его в процессе.Поскольку набор сортирован, это будет элемент с наименьшим значением.Возвращает null в случае пустого набора.
    public T PollFirst()
    {
        var entry = map.PollFirstEntry();
        if (entry.HasValue)
        {
            return entry.Value.Key;
        }
        return default!;
    }
    // Метод descendingIterator() для возврата итератора, перемещающегося от большего к меньшему, другими словами, обратного итератора.
    public IEnumerable<T> DescendingIterator()
    {
        var elements = new List<T>(map.KeySet());  // Получаем все ключи
        elements.Reverse();  // Переворачиваем список для обхода от большего к меньшему
        return elements;  // Возвращаем коллекцию, а не итератор
    }

    // Метод descendingSet() для возврата множества, представляющего собой обратную версию вызывающего набора.Результирующий набор поддерживается вызывающим набором
    public MyTreeSet<T> DescendingSet()
    {
        var result = new MyTreeSet<T>(map);
        result.AddAll(result);
        return result;
    }

    // Реализация интерфейса IEnumerable<T>
    public IEnumerator<T> GetEnumerator()
    {
        return map.KeySet().GetEnumerator();
    }

    // Реализация интерфейса IEnumerable
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public MyTreeMapRB<T, object>.Color GetColor(T obj)
    {
        var node = map.GetColor(obj);
        return node;
    }

    public MyIterator<T> Iterator() => new MyItr(this);

    public class MyItr : MyIterator<T>
    {
        private readonly T[] _elements;
        private int _cursor = -1;
        private bool _canRemove = false;
        private readonly MyTreeSet<T> _collection;

        public MyItr(MyTreeSet<T> collection)
        {
            _collection = collection;
            _elements = collection.ToArray();
        }

        public bool HasNext() => _cursor + 1 < _elements.Length;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No next element.");
            _canRemove = true;
            _cursor++;
            return _elements[_cursor];
        }

        public void Remove()
        {
            if (!_canRemove)
                throw new InvalidOperationException("Cannot remove element.");
            _collection.Remove(_elements[_cursor]);
            _canRemove = false;
        }
    }
}

public interface MySortedMap<K, V> : MyMap<K, V>
{
    K FirstKey();
    K LastKey();
    MyMap<K, V> HeadMap(K end);
    MyMap<K, V> SubMap(K start, K end);
    MyMap<K, V> TailMap(K start);

}

public class MyTreeMap<K, V> : MySortedMap<K, V>
{
    private class Node
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }

    private Node? root;
    private int size;
    private Comparer<K> comparator;

    // конструктор MyTreeMap() для создания пустого отображения, размещающего элементы согласно естественному порядку сортировки
    public MyTreeMap()
    {
        root = null;
        size = 0;
        comparator = Comparer<K>.Default;
    }

    // конструктор MyTreeMap(TreeMapComparator comp) для создания пустого отображения, размещающего элементы согласно указанному компаратору;
    public MyTreeMap(Comparer<K> comparator)
    {
        root = null;
        size = 0;
        this.comparator = comparator ?? throw new ArgumentNullException();
    }
    // метод clear() для удаления всех пар «ключ-значение» из отображения
    public void Clear()
    {
        root = null;
        size = 0;
    }
    // метод containsKey(object key) для проверки, содержит ли отображение указанный ключ;
    public bool ContainsKey(object key) => GetNode(root, key) != null;

    private Node? GetNode(Node? node, object key)
    {
        if (node == null) return null;

        // Приводим key к типу K
        K typedKey = (K)key;
        int cmp = comparator.Compare(typedKey, node.Key);

        if (cmp < 0)
            return GetNode(node.Left, key);
        if (cmp > 0)
            return GetNode(node.Right, key);

        return node;
    }
    // метод containsValue(object value) для проверки, содержит ли отображение указанное значение
    public bool ContainsValue(object value) => ContainsValue(root, value);

    private bool ContainsValue(Node? node, object value)
    {
        if (node == null) return false;
        if (EqualityComparer<object>.Default.Equals(node.Value, value)) return true; // компаратор для типа V 
        return ContainsValue(node.Left, value) || ContainsValue(node.Right, value);
    }
    //  метод entrySet() для возврата множества (Set) всех пар «ключ-значение» в отображении;
    public ICollection<KeyValuePair<K, V>> EntrySet()
    {
        var entries = new List<KeyValuePair<K, V>>();
        InOrderTraversal(root, node => entries.Add(new KeyValuePair<K, V>(node.Key, node.Value)));
        return entries;
    }

    private void InOrderTraversal(Node? node, Action<Node> action)
    {
        if (node == null) return;
        InOrderTraversal(node.Left, action);
        action(node);
        InOrderTraversal(node.Right, action);
    }
    // метод get(object key) для возврата значения, связанного с указанным ключом, или null, если ключ не найден;
    public V Get(object key)
    {
        var node = GetNode(root, key);
        return node != null ? node.Value : default!;
    }
    // метод isEmpty() для проверки, является ли отображение пустым;
    public bool IsEmpty() => size == 0;
    // метод keySet() для возврата множества (Set) всех ключей в отображении;
    public ICollection<K> KeySet()
    {
        var keys = new HashSet<K>();
        InOrderTraversal(root, node => keys.Add(node.Key));
        return keys;
    }
    // метод put(K key, V value) для добавления пары «ключ-значение» в отображение;
    public void Put(K key, V value)
    {
        root = Put(root, key, value);
    }

    private Node Put(Node? node, K key, V value)
    {
        if (node == null)
        {
            size++;
            return new Node(key, value);
        }

        int cmp = comparator.Compare(key, node.Key);
        if (cmp < 0)
            node.Left = Put(node.Left, key, value);
        else if (cmp > 0)
            node.Right = Put(node.Right, key, value);
        else
            node.Value = value;

        return node;
    }

    public void PutAll(MyMap<K, V> m)
    {

    }
    // метод remove(object key) для удаления пары «ключ-значение» с указанным ключом из отображения
    public void Remove(object key)
    {
        var node = GetNode(root, key);
        if (node == null) return; // Если узел не найден, возвращаем дефолтное значение

        // Удаляем узел и уменьшаем размер
        root = Remove(root, key);
        size--;

    }

    private Node? Remove(Node? node, object key)
    {
        if (node == null) return null;

        // Приводим ключ к типу K
        K typedKey = (K)key;
        int cmp = comparator.Compare(typedKey, node.Key);

        if (cmp < 0)
            node.Left = Remove(node.Left, key);
        else if (cmp > 0)
            node.Right = Remove(node.Right, key);
        else
        {
            // Если найдено совпадение по ключу, удаляем узел
            if (node.Left == null) return node.Right; // Если нет левого ребенка, возвращаем правого
            if (node.Right == null) return node.Left; // Если нет правого ребенка, возвращаем левого

            // Если два ребенка, находим минимальный узел в правом поддереве
            var min = GetMin(node.Right);
            node.Key = min.Key;
            node.Value = min.Value;
            node.Right = Remove(node.Right, min.Key); // Удаляем минимальный узел
        }

        return node; // Возвращаем измененный узел
    }

    private Node GetMin(Node? node) // Узел с минимальным ключом
    {
        while (node.Left != null) node = node.Left;
        return node;
    }
    // метод size() для возврата количества пар «ключ-значение» в отображении;
    public int Size() => size;
    // метод firstKey() для возврата первого ключа отображения
    public K FirstKey()
    {
        if (IsEmpty()) throw new InvalidOperationException("Map is empty.");
        return GetMin(root).Key;
    }
    // метод lastKey() для возврата последнего ключа отображения
    public K LastKey()
    {
        if (IsEmpty()) throw new InvalidOperationException("Map is empty.");
        var node = root;
        while (node.Right != null) node = node.Right;
        return node.Key;
    }
    // метод headMap(K end) для возврата сортированного отображения, содержащего элементы, ключ которых меньше end;
    public MyMap<K, V> HeadMap(K end)
    {
        var result = new MyTreeMap<K, V>(comparator);
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, end) < 0)
                result.Put(node.Key, node.Value);
        });
        return result;
    }
    //метод subMap(K start, K end) для возврата отображения, содержащего элементы, чей ключ больше или равен start и меньше end;
    public MyMap<K, V> SubMap(K start, K end)
    {
        var result = new MyTreeMap<K, V>(comparator);
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, start) >= 0 && comparator.Compare(node.Key, end) < 0)
                result.Put(node.Key, node.Value);
        });
        return result;
    }
    //метод tailMap(K start) для возврата сортированного отображения, содержащего элементы, ключ которых больше start;
    public MyMap<K, V> TailMap(K start)
    {
        var result = new MyTreeMap<K, V>(comparator);
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, start) > 0)
                result.Put(node.Key, node.Value);
        });
        return result;
    }
    // метод lowerEntry(K key) для возврата пары «ключ-значение», где ключ меньше заданного;
    public KeyValuePair<K, V>? LowerEntry(K key)
    {
        Node result = default!;
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, key) < 0)
                result = node;
        });
        return result != null ? new KeyValuePair<K, V>(result.Key, result.Value) : (KeyValuePair<K, V>?)null;
    }
    // метод floorEntry(K key) для возврата пары «ключ-значение», где ключ меньше или равен заданному;
    public KeyValuePair<K, V>? FloorEntry(K key)
    {
        Node result = default!;
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, key) <= 0)
                result = node;
        });
        return result != null ? new KeyValuePair<K, V>(result.Key, result.Value) : (KeyValuePair<K, V>?)null;
    }
    // метод higherEntry(K key) для возврата пары «ключ-значение», где ключ больше заданного;
    public KeyValuePair<K, V>? HigherEntry(K key)
    {
        Node result = default!;
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, key) > 0 && (result == null || comparator.Compare(node.Key, result.Key) < 0))
                result = node;
        });
        return result != null ? new KeyValuePair<K, V>(result.Key, result.Value) : (KeyValuePair<K, V>?)null;
    }
    // метод ceilingEntry(K key) для возврата пары «ключ-значение», где ключ больше или равен заданному;
    public KeyValuePair<K, V>? CeilingEntry(K key)
    {
        Node result = default!;
        InOrderTraversal(root, node =>
        {
            if (comparator.Compare(node.Key, key) >= 0 && (result == null || comparator.Compare(node.Key, result.Key) < 0))
                result = node;
        });
        return result != null ? new KeyValuePair<K, V>(result.Key, result.Value) : (KeyValuePair<K, V>?)null;
    }
    // метод lowerKey(K key) для возврата ключа, который меньше заданного;
    public K LowerKey(K key)
    {
        var entry = LowerEntry(key);
        if (entry.HasValue)
            return entry.Value.Key;
        throw new InvalidOperationException("Key - null");
    }
    // метод floorKey(K key) для возврата ключа, который меньше или равен заданному;
    public K FloorKey(K key)
    {
        var entry = FloorEntry(key);
        if (entry.HasValue)
            return entry.Value.Key;
        throw new InvalidOperationException("Key - null");
    }
    // метод higherKey(K key) для возврата ключа, который больше заданного;
    public K HigherKey(K key)
    {
        var entry = HigherEntry(key);
        if (entry.HasValue)
            return entry.Value.Key;
        throw new InvalidOperationException("Key - null");
    }
    // метод ceilingKey(K key) для возврата ключа, который больше или равен заданному;
    public K CeilingKey(K key)
    {
        var entry = CeilingEntry(key);
        if (entry.HasValue)
            return entry.Value.Key;
        throw new InvalidOperationException("Key - null");
    }
    // метод pollFirstEntry() для удаления и возврата первого элемента отображения;
    public KeyValuePair<K, V>? PollFirstEntry()
    {
        if (IsEmpty()) return null;
        var firstEntry = FirstEntry();
        root = Remove(root, firstEntry.Value.Key);
        size--;
        return firstEntry;
    }
    // метод pollLastEntry() для удаления и возврата последнего элемента отображения;
    public KeyValuePair<K, V>? PollLastEntry()
    {
        if (IsEmpty()) return null;
        var lastEntry = LastEntry();
        root = Remove(root, lastEntry.Value.Key);
        size--;
        return lastEntry;
    }
    // метод firstEntry() для возврата первого элемента отображения без удаления;
    public KeyValuePair<K, V>? FirstEntry()
    {
        if (IsEmpty()) return null;
        var minNode = GetMin(root);
        return new KeyValuePair<K, V>(minNode.Key, minNode.Value);
    }

    // метод lastEntry() для возврата последнего элемента отображения без удаления
    public KeyValuePair<K, V>? LastEntry()
    {
        if (IsEmpty()) return null;
        var node = root;
        while (node.Right != null) node = node.Right;
        return new KeyValuePair<K, V>(node.Key, node.Value);
    }
}