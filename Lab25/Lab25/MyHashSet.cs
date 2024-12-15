using MyHashMap;

namespace MyHashSet
{
    public class MyHashSet<T> where T : IComparable
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
    }
}
