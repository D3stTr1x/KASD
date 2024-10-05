
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

    public int Size() //  для получения размера динамического массива в элементах.
    {
        return size;
    }
    public void Print()
    {
        if (size != 0)
        {
            for (int i = 0; i < size; i++)
                Console.WriteLine(elementData[i]);
        }
    }

    public bool Contains(string o) //  для проверки, находится ли указанный объект в динамическом массиве.
    {
        if (elementData != null && size != 0)
        {
            for (int i = 0; i < size; i++)
            {
                string? elem = elementData[i]?.ToString();
                if (elem == null)
                    elem = string.Empty;
                if (TransformTag(o).Equals(TransformTag(elem)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private string TransformTag(string a)
    {
        return a.ToLower().Replace("/", "");
    }
}
class Programm
{ 
    static void Main(string[] args)
    {
        StreamReader f = new StreamReader("input.txt");
        string str = f.ReadToEnd();
        string? prRes = null;
        MyArrayList<string> res = new MyArrayList<string>();
        char beg = '<';
        char end = '>';
        int flag;
        for (int i = 0; i < str.Length; i++)
        {
            flag = 0;
            prRes = null;
            if (str[i] == beg)
            {
                if (Char.IsLetter(str[i + 1]) || str[i + 1] == '/')
                {
                    prRes += "<";
                    prRes += str[i+1];
                    int j = i + 2;
                    while (str[j] != end)
                    {
                        if ((!Char.IsLetter(str[j])) && (!Char.IsNumber(str[j])))
                        {
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            prRes = null;
                            break;
                        }
                        prRes += str[j];
                        j++;
                    }
                    if (flag==0)
                    {
                        prRes += '>';
                    }
                }
            }
            if (flag == 0 && prRes!=null && !res.Contains(prRes))
            {
                res.Add(prRes);
            }
        }
        Console.WriteLine($"Количество слов: {res.Size()}\nСлова:");
        res.Print();

    }
}


