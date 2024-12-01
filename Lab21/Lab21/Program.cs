using System;
using System.Collections.Generic;

public class MyTreeMap<K, V>
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
    public bool ContainsKey(K key) => GetNode(root, key) != null;

    private Node? GetNode(Node? node, K key)
    {
        if (node == null) return null;
        int cmp = comparator.Compare(key, node.Key);
        if (cmp < 0) return GetNode(node.Left, key);
        if (cmp > 0) return GetNode(node.Right, key);
        return node;
    }
    // метод containsValue(object value) для проверки, содержит ли отображение указанное значение
    public bool ContainsValue(V value) => ContainsValue(root, value);

    private bool ContainsValue(Node? node, V value)
    {
        if (node == null) return false;
        if (EqualityComparer<V>.Default.Equals(node.Value, value)) return true; // компаратор для типа V 
        return ContainsValue(node.Left, value) || ContainsValue(node.Right, value);
    }
    //  метод entrySet() для возврата множества (Set) всех пар «ключ-значение» в отображении;
    public IEnumerable<KeyValuePair<K, V>> EntrySet()
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
    public V Get(K key)
    {
        var node = GetNode(root, key);
        return node != null ? node.Value : default!;
    }
    // метод isEmpty() для проверки, является ли отображение пустым;
    public bool IsEmpty() => size == 0;
    // метод keySet() для возврата множества (Set) всех ключей в отображении;
    public IEnumerable<K> KeySet()
    {
        var keys = new List<K>();
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
    // метод remove(object key) для удаления пары «ключ-значение» с указанным ключом из отображения
    public V Remove(K key)
    {
        var node = GetNode(root, key);
        if (node == null) return default!;

        root = Remove(root, key);
        size--;
        return node.Value;
    }

    private Node Remove(Node? node, K key)
    {
        if (node == null) return default!;

        int cmp = comparator.Compare(key, node.Key);
        if (cmp < 0)
            node.Left = Remove(node.Left, key);
        else if (cmp > 0)
            node.Right = Remove(node.Right, key);
        else
        {
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            var min = GetMin(node.Right);
            node.Key = min.Key;
            node.Value = min.Value;
            node.Right = Remove(node.Right, min.Key); // В случае c двамя потомкми используется минимальный узел из правого поддерева
        }

        return node;
    }

    private Node GetMin(Node? node) // Узел с минимальным ключом
    {
        while (node.Left != null) node = node.Left;
        return node;
    }
    // метод size() для возврата количества пар «ключ-значение» в отображении;
    public int Size => size;
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
    public MyTreeMap<K, V> HeadMap(K end)
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
    public MyTreeMap<K, V> SubMap(K start, K end)
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
    public MyTreeMap<K, V> TailMap(K start)
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