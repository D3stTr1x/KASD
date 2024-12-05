using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Lab22
{
    public partial class Form1 : Form
    {
        MyHashMap<int, int> hash5 = new MyHashMap<int, int>();
        MyHashMap<int, int> hash6 = new MyHashMap<int, int>();
        MyHashMap<int, int> hash7 = new MyHashMap<int, int>();
        MyHashMap<int, int> hash8 = new MyHashMap<int, int>();
        MyTreeMap<int, int> tree5 = new MyTreeMap<int, int>();
        MyTreeMap<int, int> tree6 = new MyTreeMap<int, int>();
        MyTreeMap<int, int> tree7 = new MyTreeMap<int, int>();
        MyTreeMap<int, int> tree8 = new MyTreeMap<int, int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.XAxis.Title.Text = "Массив 10^x, элементов";
            pane.YAxis.Title.Text = "Время выполнения, мс";
            pane.Title.Text = "График зависимости времени от количества этеметов";
            pane.XAxis.Scale.MinAuto = false;
            pane.XAxis.Scale.MaxAuto = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            int cb = comboBox1.SelectedIndex;
            Random rand = new Random();
            switch (cb)
            {
                case 0:
                    PointPairList pointList = new PointPairList();
                    PointPairList pointList1 = new PointPairList();
                    Stopwatch sw = new Stopwatch();
                    double[] time = new double[4];                  

                    for (int k = 0; k < 20; k++)
                    {
                        hash5.Clear();
                        sw.Start();
                        for (int i = 0; i < 10; i++)
                            hash5.Put(i, rand.Next(1, 100));                        
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }                   
                    time[0] /= 20;
                    foreach (var entry in hash5.EntrySet())
                    {
                        Debug.WriteLine($"{entry.Key} => {entry.Value}");
                    }

                    for (int k = 0; k < 20; k++)
                    {
                        hash6.Clear();
                        sw.Start();
                        for (int i = 0; i < 100; i++)
                            hash6.Put(i, rand.Next(1, 100));
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();                       
                        
                    }                    
                    time[1] /= 20;
                    
                    for (int k = 0; k < 20; k++)
                    {
                        hash7.Clear();
                        sw.Start();
                        for (int i = 0; i < 1000; i++)
                            hash7.Put(i, rand.Next(1, 100));
                        sw.Stop();                        
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    
                    time[2] /= 20;

                    for (int k = 0; k < 20; k++)
                    {
                        hash8.Clear();
                        sw.Start();
                        for (int i = 0; i < 10000; i++)
                            hash8.Put(i, rand.Next(1, 100));
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    
                    time[3] /= 20;

                    int pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    sw.Reset();
                    LineItem myCurve = pane.AddCurve("Hash", pointList, Color.Red, SymbolType.None);
                    for (int i = 0; i < time.Length; i++)
                    {
                        time[i] = 0;
                    }
                    for (int k = 0; k < 5; k++)
                    {
                        tree5.Clear();
                        sw.Start();
                        for (int i = 0; i < 10; i++)
                            tree5.PutBig(i, rand.Next(1, 100));
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 5;
                    foreach (var entry in tree5.EntrySet())
                    {
                        Debug.WriteLine($"{entry.Key} => {entry.Value}");
                    }

                    for (int k = 0; k < 20; k++)
                    {
                        tree6.Clear();
                        sw.Start();
                        for (int i = 0; i < 100; i++)
                            tree6.PutBig(i, rand.Next(1, 100));
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;

                    for (int k = 0; k < 20; k++)
                    {
                        tree7.Clear();
                        sw.Start();
                        for (int i = 0; i < 1000; i++)
                            tree7.PutBig(i, rand.Next(1, 100));
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;

                    for (int k = 0; k < 5; k++)
                    {
                        tree8.Clear();
                        sw.Start();
                        for (int i = 0; i < 10000; i++)
                            tree8.PutBig(i, rand.Next(1, 100));
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 5;

                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList1.Add(pw, time[i]);
                        pw *= 10;
                    }
                    sw.Reset();
                    myCurve = pane.AddCurve("Tree", pointList1, Color.Black, SymbolType.None);

                    break;
                case 1:                   
                    pointList = new PointPairList();
                    pointList1 = new PointPairList();
                    sw = new Stopwatch();
                    time = new double[4];
                    rand = new Random();
                    

                    Debug.WriteLine("Hash5 size before Get: " + hash5.Size());
                    Debug.WriteLine("Tree5 size before Get: " + tree5.Size());
                    Debug.WriteLine("Hash6 size before Get: " + hash6.Size());
                    Debug.WriteLine("Tree6 size before Get: " + tree6.Size());
                    Debug.WriteLine("Hash7 size before Get: " + hash7.Size());
                    Debug.WriteLine("Tree7 size before Get: " + tree7.Size());
                    Debug.WriteLine("Hash8 size before Get: " + hash8.Size());
                    Debug.WriteLine("Tree8 size before Get: " + tree8.Size());

                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, hash5.Size());
                        sw.Start();
                        var t = hash5.Get(randKey);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                        Debug.Write(t + " ");
                    }
                    Debug.WriteLine("");
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, hash6.Size());
                        sw.Start();
                        var t = hash6.Get(randKey);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                        Debug.Write(t + " ");
                    }
                    Debug.WriteLine("");
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, hash7.Size());
                        sw.Start();
                        var t = hash7.Get(randKey);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, hash8.Size());
                        sw.Start();
                        var t = hash8.Get(randKey);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 20;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList1.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new double[4];
                    myCurve = pane.AddCurve("Hash", pointList1, Color.Red, SymbolType.None);


                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, tree5.Size());
                        sw.Start();
                        var t = tree5.GetBig(randKey);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                        Debug.Write(t + " ");
                    }
                    Debug.WriteLine("");
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, tree6.Size());
                        sw.Start();
                        var t = tree6.GetBig(randKey);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                        Debug.Write(t + " ");
                    }
                    Debug.WriteLine("");
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, tree7.Size());
                        sw.Start();
                        var t = tree7.GetBig(randKey);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 2; k++)
                    {
                        int randKey = rand.Next(0, tree8.Size());
                        sw.Start();
                        var t = tree8.GetBig(randKey);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 2;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    myCurve = pane.AddCurve("Tree", pointList, Color.Black, SymbolType.None);
                    break;
                case 2:
                    pointList = new PointPairList();
                    pointList1 = new PointPairList();
                    sw = new Stopwatch();
                    time = new double[4];
                    rand = new Random();
                    Debug.WriteLine("Hash5 size before Remove: " + hash5.Size());
                    Debug.WriteLine("Tree5 size before Remove: " + tree5.Size());
                    Debug.WriteLine("Hash6 size before Remove: " + hash6.Size());
                    Debug.WriteLine("Tree6 size before Remove: " + tree6.Size());
                    Debug.WriteLine("Hash7 size before Remove: " + hash7.Size());
                    Debug.WriteLine("Tree7 size before Remove: " + tree7.Size());
                    Debug.WriteLine("Hash8 size before Remove: " + hash8.Size());
                    Debug.WriteLine("Tree8 size before Remove: " + tree8.Size());
                    for (int k = 0; k < 5; k++)
                    {
                        int randKey = rand.Next(0, hash5.Size());
                        while(!hash5.ContainsKey(randKey))
                            randKey = rand.Next(0, hash5.Size());
                        sw.Start();
                        hash5.Remove(randKey);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    foreach (var entry in hash5.EntrySet())
                    {
                        Debug.WriteLine($"{entry.Key} => {entry.Value}");
                    }
                    time[0] /= 5;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, hash6.Size());
                        while (!hash6.ContainsKey(randKey))
                            randKey = rand.Next(0, hash6.Size());
                        sw.Start();
                        hash6.Remove(randKey);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, hash7.Size());
                        while (!hash7.ContainsKey(randKey))
                            randKey = rand.Next(0, hash7.Size());
                        sw.Start();
                        hash7.Remove(randKey);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 5; k++)
                    {
                        int randKey = rand.Next(0, hash8.Size());
                        while (!hash8.ContainsKey(randKey))
                            randKey = rand.Next(0, hash8.Size());
                        sw.Start();
                        hash8.Remove(randKey);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 5;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList1.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new double[4];
                    myCurve = pane.AddCurve("Hash", pointList1, Color.Red, SymbolType.None);


                    for (int k = 0; k < 5; k++)
                    {
                        int randKey = rand.Next(0, tree5.Size());
                        while (!tree5.ContainsKeyBig(randKey))
                            randKey = rand.Next(0, tree5.Size());
                        sw.Start();
                        tree5.RemoveBig(randKey);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    foreach (var entry in tree5.EntrySet())
                    {
                        Debug.WriteLine($"{entry.Key} => {entry.Value}");
                    }
                    time[0] /= 5;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, tree6.Size());
                        while (!tree6.ContainsKeyBig(randKey))
                            randKey = rand.Next(0, tree6.Size());
                        sw.Start();
                        tree6.RemoveBig(randKey);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randKey = rand.Next(0, tree7.Size());
                        while (!tree7.ContainsKeyBig(randKey))
                            randKey = rand.Next(0, tree7.Size());
                        sw.Start();
                        tree7.RemoveBig(randKey);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 5; k++)
                    {
                        int randKey = rand.Next(0, tree8.Size());
                        while (!tree8.ContainsKeyBig(randKey))
                            randKey = rand.Next(0, tree8.Size());
                        sw.Start();
                        tree8.RemoveBig(randKey);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 5;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    myCurve = pane.AddCurve("Tree", pointList, Color.Black, SymbolType.None);
                    break;
            }
            pane.XAxis.Scale.Max = 10000;
            pane.XAxis.Scale.Min = 10;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}

public class MyTreeMap<K, V>
{
    private class Node
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }

    private Node root;
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

    private Node GetNode(Node node, K key)
    {
        if (node == null) return null;
        int cmp = comparator.Compare(key, node.Key);
        if (cmp < 0) return GetNode(node.Left, key);
        if (cmp > 0) return GetNode(node.Right, key);
        return node;
    }

    private Node GetNodeBig(Node node, K key)
    {
        while (node != null)
        {
            int cmp = comparator.Compare(key, node.Key);
            if (cmp == 0)
                return node; // Нашли нужный узел
            else if (cmp < 0)
                node = node.Left; // Идем в левое поддерево
            else
                node = node.Right; // Идем в правое поддерево
        }

        return null; // Узел не найден
    }
    private void InOrderTraversal(Node node, Action<Node> action)
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
        return node != null ? node.Value : default;
    }

    public V GetBig(K key)
    {
        var node = GetNodeBig(root, key);
        return node != null ? node.Value : default;
    }
    // метод put(K key, V value) для добавления пары «ключ-значение» в отображение;
    public void Put(K key, V value)
    {
        root = Put(root, key, value);
    }

    private Node Put(Node node, K key, V value)
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

    public void PutBig(K key, V value)
    {
        Node newNode = new Node(key, value);
        if (root == null)
        {
            root = newNode;
            size++;
            return;
        }

        Node current = root;
        Node parent = null;

        while (current != null)
        {
            parent = current;
            int cmp = comparator.Compare(key, current.Key);
            if (cmp < 0)
            {
                current = current.Left;
            }
            else if (cmp > 0)
            {
                current = current.Right;
            }
            else
            {
                // Обновляем значение, если ключ уже существует
                current.Value = value;
                return;
            }
        }

        // Вставляем новый узел
        int comparison = comparator.Compare(key, parent.Key);
        if (comparison < 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }
        size++;
    }
    // метод remove(object key) для удаления пары «ключ-значение» с указанным ключом из отображения
    public V Remove(K key)
    {
        var node = GetNode(root, key);
        if (node == null) return default;

        root = Remove(root, key);
        size--;
        return node.Value;
    }

    private Node Remove(Node node, K key)
    {
        if (node == null) return default;

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

    private Node GetMin(Node node) // Узел с минимальным ключом
    {
        while (node.Left != null) node = node.Left;
        return node;
    }

    public V RemoveBig(K key)
    {
        if (root == null) return default;

        Node parent = null;
        Node current = root;

        // Найти узел, который нужно удалить, и его родителя
        while (current != null)
        {
            int cmp = comparator.Compare(key, current.Key);
            if (cmp == 0) break;
            parent = current;
            current = (cmp < 0) ? current.Left : current.Right;
        }

        // Если узел не найден
        if (current == null) return default;

        V removedValue = current.Value;

        // Если у узла два дочерних узла
        if (current.Left != null && current.Right != null)
        {
            Node successorParent = current;
            Node successor = current.Right;

            // Найти минимальный узел в правом поддереве
            while (successor.Left != null)
            {
                successorParent = successor;
                successor = successor.Left;
            }

            // Заменить значения текущего узла
            current.Key = successor.Key;
            current.Value = successor.Value;

            // Удалить узел-наследник
            current = successor;
            parent = successorParent;
        }

        // Удалить узел (текущий узел имеет 0 или 1 потомка)
        Node replacement = (current.Left != null) ? current.Left : current.Right;

        if (parent == null) // Если удаляем корень
            root = replacement;
        else if (parent.Left == current)
            parent.Left = replacement;
        else
            parent.Right = replacement;

        size--;
        return removedValue;
    }

    // метод size() для возврата количества пар «ключ-значение» в отображении;
    public int Size() => size;
    public void Clear()
    {
        root = null;
        size = 0;
    }
    public bool ContainsKey(K key) => GetNode(root, key) != null;
    public bool ContainsKeyBig(K key) => GetNodeBig(root, key) != null;
    public K FirstKey()
    {
        if (IsEmpty()) throw new InvalidOperationException("Map is empty.");
        return GetMin(root).Key;
    }
    public bool IsEmpty() => size == 0;
    public IEnumerable<KeyValuePair<K, V>> EntrySet()
    {
        var entries = new List<KeyValuePair<K, V>>();
        InOrderTraversal(root, node => entries.Add(new KeyValuePair<K, V>(node.Key, node.Value)));
        return entries;
    }
}

public class MyHashMap<K, V>
{
    private class Entry
    {
        public K Key { get; }
        public V Value { get; set; }
        public Entry Next { get; set; }

        public Entry(K key, V value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

    private Entry[] table;
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
    private int GetBucketIndex(K key)
    {
        int hashCode = key?.GetHashCode() ?? 0;
        return Math.Abs(hashCode) % table.Length;
    }
    // метод get(object key) для возврата значения, связанного с указанным ключом, или null, если ключ не найден
    public V Get(K key)
    {
        int index = GetBucketIndex(key);
        Entry current = table[index];
        while (current != null)
        {
            if (EqualityComparer<K>.Default.Equals(current.Key, key))
                return current.Value;
            current = current.Next;
        }
        return default;
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
            Entry current = bucket;
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
        Entry current = table[index];
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
        Entry current = table[index];
        Entry previous = null;

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
    public void Clear()
    {
        table = new Entry[table.Length];
        size = 0;
    }
    public bool ContainsKey(K key)
    {
        int index = GetBucketIndex(key);
        Entry current = table[index];
        while (current != null)
        {
            if (EqualityComparer<K>.Default.Equals(current.Key, key))
                return true;
            current = current.Next;
        }
        return false;
    }

    // метод entrySet() для возврата множества (Set) всех пар «ключ-значение» в отображении;
    public ICollection<KeyValuePair<K, V>> EntrySet()
    {
        var entries = new List<KeyValuePair<K, V>>();
        foreach (var bucket in table)
        {
            Entry current = bucket;
            while (current != null)
            {
                entries.Add(new KeyValuePair<K, V>(current.Key, current.Value));
                current = current.Next;
            }
        }
        return entries;
    }
}