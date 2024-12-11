using MyTreeMap;
public class MyTreeSet<T> : IEnumerable<T>
{
    public enum Color { Red, Black }
    private MyTreeMap<T, object> map;
    // Конструктор MyTreeSet() для создания пустого множества, размещающего элементы согласно естественному порядку сортировки.
    public MyTreeSet()
    {
        map = new MyTreeMap<T, object>();
    }
    // Конструктор MyTreeSet(MyTreeMap<E, object> m) для создания множества, использующего указанный объект MyTreeMap для хранения элементов.
    public MyTreeSet(MyTreeMap<T, object> m)
    {
        map = m ?? throw new ArgumentNullException(nameof(m));
    }
    // Конструктор MyTreeSet(TreeMapComparator comparator) для создания пустого множества, размещающего элементы согласно указанному компаратору.
    public MyTreeSet(Comparer<T> comparator)
    {
        map = new MyTreeMap<T, object>(comparator);
    }
    // Конструктор MyTreeSet(T[] a) для создания множества, содержащего элементы указанной коллекции.
    public MyTreeSet(T[] a)
    {
        map = new MyTreeMap<T, object>();
        AddAll(a);
    }
    // Конструктор MyTreeSet(SortedSet<E> s) для создания множества, содержащего элементы указанного сортированного множества.
    public MyTreeSet(SortedSet<T> s)
    {
        map = new MyTreeMap<T, object>();
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
    public void AddAll(T[] a)
    {
        foreach (var item in a)
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
    public bool ContainsAll(T[] a)
    {
        foreach (var item in a)
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
    public void Remove(object o)
    {
        if (o is T t)
        {
            if (map.ContainsKey(t))
            {
                map.Remove(t);
            }
        }
    }
    // Метод removeAll(T[] a) для удаления указанных объектов из множества.
    public void RemoveAll(T[] a)
    {
        foreach (var item in a)
        {
            map.Remove(item);
        }
    }
    // Метод retainAll(T[] a) для оставления во множестве только указанных объектов.
    public void RetainAll(T[] a)
    {
        var toRemove = new List<T>();
        foreach (var item in map.KeySet())
        {
            if (Array.IndexOf(a, item) == -1)
            {
                toRemove.Add(item);
            }
        }
        foreach (var item in toRemove)
        {
            map.Remove(item);
        }
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
    public MyTreeSet<T> SubSet(T fromElement, T toElement)
    {
        var result = new MyTreeSet<T>(map.SubMap(fromElement, toElement));
        return result;
    }
    // Метод headSet(E toElement) для возврата множества элементов, меньших чем указанный элемент.
    public MyTreeSet<T> HeadSet(T toElement)
    {
        var result = new MyTreeSet<T>(map.HeadMap(toElement));
        return result;
    }
    // Метод tailSet(E fromElement) для возврата части множества из элементов, больши или равных указанному элементу.
    public MyTreeSet<T> TailSet(T fromElement)
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
        result.AddAll(ToArray());
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
    public MyTreeMap<T, object>.Color GetColor(T obj)
    {
        var node = map.GetColor(obj);
        return node;
    }
}

public class Program
{
    public static void Main()
    {
        // Создаём экземпляр множества MyTreeSet с типом int
        MyTreeSet<int> treeSet = new MyTreeSet<int>();

        // Проверка метода Add
        Console.WriteLine("Добавляем элементы в множество:");
        Console.WriteLine("Добавить 5: ");
        treeSet.Add(5); 
        Console.WriteLine("Добавить 10: ");
        treeSet.Add(10); 
        Console.WriteLine("Добавить 5 снова: ");
        treeSet.Add(5); // false (так как элемент уже существует)

        // Проверка метода Size
        Console.WriteLine("Размер множества: " + treeSet.Size()); // 2

        // Проверка метода Contains
        Console.WriteLine("Содержит ли 5? " + treeSet.Contains(5)); // true
        Console.WriteLine("Содержит ли 15? " + treeSet.Contains(15)); // false

        // Проверка метода ToArray
        Console.WriteLine("Элементы множества: ");
        foreach (var item in treeSet.ToArray())
        {
            Console.WriteLine(item); // 5 10
        }

        // Проверка метода Remove
        Console.WriteLine("Удаляем 5: ");
        treeSet.Remove(5);
        Console.WriteLine("Удаляем 15: ");
        treeSet.Remove(15); // false
        Console.WriteLine("Размер множества после удаления: " + treeSet.Size()); // 1

        // Проверка метода First и Last
        Console.WriteLine("Первый элемент: " + treeSet.First()); // 10
        Console.WriteLine("Последний элемент: " + treeSet.Last()); // 10

        // Проверка метода PollFirst и PollLast
        Console.WriteLine("PollFirst: " + treeSet.PollFirst()); // 10
        treeSet.Add(20);
        treeSet.Add(30);
        Console.WriteLine("PollLast: " + treeSet.PollLast()); // 30

        // Проверка метода DescendingIterator
        Console.WriteLine("Обратный итератор:");
        treeSet.Add(15); // Добавим элемент для проверки обратного итератора
        foreach (var item in treeSet.DescendingIterator())
        {
            Console.Write(item); // 20 15            
        }
        Console.WriteLine($"Color of 15: {treeSet.GetColor(15)}");
        // Проверка метода SubSet
        Console.WriteLine("Подмножество от 10 до 30:");
        var subset = treeSet.SubSet(10, 30);
        foreach (var item in subset)
        {
            Console.WriteLine(item); // 15 20
        }

        // Проверка метода HeadSet
        Console.WriteLine("HeadSet до 20:");
        var headSet = treeSet.HeadSet(20);
        foreach (var item in headSet)
        {
            Console.WriteLine(item); // 15
        }

        // Проверка метода TailSet
        Console.WriteLine("TailSet от 15:");
        var tailSet = treeSet.TailSet(15);
        foreach (var item in tailSet)
        {
            Console.WriteLine(item); // 20
        }

        // Проверка метода RetainAll
        Console.WriteLine("Оставляем только элементы [15, 20]:");
        treeSet.RetainAll(new int[] { 15, 20 });
        foreach (var item in treeSet)
        {
            Console.WriteLine(item); // 15 20
        }

        // Проверка метода Clear
        treeSet.Clear();
        Console.WriteLine("После Clear, размер множества: " + treeSet.Size()); // 0

        // Проверка метода IsEmpty
        Console.WriteLine("Множество пусто? " + treeSet.IsEmpty()); // true
    }
} 