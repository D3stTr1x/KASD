using System;
using System.Collections;
using System.Collections.Generic;

public class MyHashMap<K, V>
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
    private int GetBucketIndex(K key)
    {
        int hashCode = key?.GetHashCode() ?? 0;
        return Math.Abs(hashCode) % table.Length;
    }
    //  метод containsKey(object key) для проверки, содержит ли отображение указанный ключ;
    public bool ContainsKey(K key)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        while (current != null)
        {
            if (EqualityComparer<K>.Default.Equals(current.Key, key))
                return true;
            current = current.Next;
        }
        return false;
    }
    // метод containsValue(object value) для проверки, содержит ли отображение указанное значение;
    public bool ContainsValue(V value)
    {
        foreach (var bucket in table)
        {
            Entry? current = bucket;
            while (current != null)
            {
                if (EqualityComparer<V>.Default.Equals(current.Value, value))
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
    public V? Get(K key)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        while (current != null)
        {
            if (EqualityComparer<K>.Default.Equals(current.Key, key))
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
    // метод remove(object key) для удаления пары «ключ-значение» с указанным ключом из отображения;
    public void Remove(K key)
    {
        int index = GetBucketIndex(key);
        Entry? current = table[index];
        Entry? previous = null;

        while (current != null)
        {
            if (EqualityComparer<K>.Default.Equals(current.Key, key))
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

class Program
{
    static void Main(string[] args)
    {
        // Создаем экземпляр MyHashMap
        var myHashMap = new MyHashMap<string, int>();

        // Проверяем isEmpty() на пустом HashMap
        Console.WriteLine($"IsEmpty: {myHashMap.IsEmpty()}"); // Ожидается true

        // Добавляем элементы
        myHashMap.Put("Apple", 1);
        myHashMap.Put("Banana", 2);
        myHashMap.Put("Cherry", 3);

        // Проверяем isEmpty() после добавления элементов
        Console.WriteLine($"IsEmpty: {myHashMap.IsEmpty()}"); // Ожидается false

        // Проверяем Size()
        Console.WriteLine($"Size: {myHashMap.Size()}"); // Ожидается 3

        // Проверяем Get() на существующих ключах
        Console.WriteLine($"Get(\"Apple\"): {myHashMap.Get("Apple")}"); // Ожидается 1
        Console.WriteLine($"Get(\"Banana\"): {myHashMap.Get("Banana")}"); // Ожидается 2

        // Проверяем Get() на несуществующем ключе
        Console.WriteLine($"Get(\"Orange\"): {myHashMap.Get("Orange")}"); // Ожидается null (default)

        // Проверяем ContainsKey()
        Console.WriteLine($"ContainsKey(\"Cherry\"): {myHashMap.ContainsKey("Cherry")}"); // Ожидается true
        Console.WriteLine($"ContainsKey(\"Orange\"): {myHashMap.ContainsKey("Orange")}"); // Ожидается false

        // Проверяем ContainsValue()
        Console.WriteLine($"ContainsValue(2): {myHashMap.ContainsValue(2)}"); // Ожидается true
        Console.WriteLine($"ContainsValue(5): {myHashMap.ContainsValue(5)}"); // Ожидается false

        // Удаляем элемент и проверяем Remove()
        myHashMap.Remove("Banana");
        Console.WriteLine($"ContainsKey(\"Banana\"): {myHashMap.ContainsKey("Banana")}"); // Ожидается false
        Console.WriteLine($"Size after remove: {myHashMap.Size()}"); // Ожидается 2

        // Проверяем KeySet()
        Console.WriteLine("KeySet:");
        foreach (var key in myHashMap.KeySet())
        {
            Console.WriteLine(key); // Ожидается "Apple" и "Cherry"
        }

        // Проверяем EntrySet()
        Console.WriteLine("EntrySet:");
        foreach (var entry in myHashMap.EntrySet())
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        // Очищаем HashMap
        myHashMap.Clear();
        Console.WriteLine($"Size after clear: {myHashMap.Size()}"); // Ожидается 0
        Console.WriteLine($"IsEmpty after clear: {myHashMap.IsEmpty()}"); // Ожидается true
    }
}