namespace MyArray
{
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
            // Проверяем, есть ли место в массиве, если нет — увеличиваем его
            if (size == elementData.Length)
            {
                T[] newData = new T[elementData.Length * 2];
                for (int i = 0; i < elementData.Length; i++)
                {
                    newData[i] = elementData[i];
                }
                elementData = newData;
            }

            // Добавляем новый элемент и увеличиваем размер
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
                    if (item != null)
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

        public T[] ToArray() // для возвращения массива объектов, содержащего все элементы динамического массива
        {
            T[] a = new T[size]; // Создаём массив типа T[] с размером size
            for (int i = 0; i < size; i++)
            {
                a[i] = elementData[i]; // Копируем элементы
            }
            return a;
        }

        public T[] ToArray(T[] a) //для возвращения массива объектов, содержащего все элементы динамического массива.Если аргумент a равен null, то создаётся новыймассив, в который копируются элементы
        {
            if (a.Length < size)
            {
                a = new T[size];
            }
            for (int i = 0; i < size; i++)
            {
                a[i] = elementData[i];
            }
            return a;

        }
        

        public void Add(int index, T e) //для добавления элемента в указанную позицию
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");

            if (elementData == null)
                elementData = new T[1];

            if (size == elementData.Length)
            {
                T[] newArray = new T[size + size / 2 + 1];
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
            if (e != null)
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

        public void PrintArray()
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
}
