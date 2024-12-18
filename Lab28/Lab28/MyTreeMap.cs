namespace MyTreeMapRB
{
    public class MyTreeMapRB<K, V>
    {
        public enum Color { Red, Black }

        private class Node
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            public Color NodeColor { get; set; }

            public Node(K key, V value, Color color)
            {
                Key = key;
                Value = value;
                NodeColor = color;
            }
        }

        private Node? root;
        private int size;
        private readonly Comparer<K> comparator;

        private Node RotateLeft(Node node)
        {
            var x = node.Right!;
            node.Right = x.Left;
            x.Left = node;
            x.NodeColor = node.NodeColor;
            node.NodeColor = Color.Red;
            return x;
        }

        private Node RotateRight(Node node)
        {
            var x = node.Left!;
            node.Left = x.Right;
            x.Right = node;
            x.NodeColor = node.NodeColor;
            node.NodeColor = Color.Red;
            return x;
        }

        private void FlipColors(Node node)
        {
            node.NodeColor = Color.Red;
            node.Left!.NodeColor = Color.Black;
            node.Right!.NodeColor = Color.Black;
        }

        // Балансировка дерева, поддержка свойств красно-чёрного дерева
        private Node Balance(Node node)
        {
            if (IsRed(node.Right) && !IsRed(node.Left)) node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left)) node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right)) FlipColors(node);
            return node;
        }

        private bool IsRed(Node? node) => node != null && node.NodeColor == Color.Red;

        // конструктор MyTreeMap() для создания пустого отображения, размещающего элементы согласно естественному порядку сортировки
        public MyTreeMapRB()
        {
            root = null;
            size = 0;
            comparator = Comparer<K>.Default;
        }
        // конструктор MyTreeMap(TreeMapComparator comp) для создания пустого отображения, размещающего элементы согласно указанному компаратору;
        public MyTreeMapRB(Comparer<K> comparator)
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
            if (EqualityComparer<V>.Default.Equals(node.Value, value)) return true;
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
            root.NodeColor = Color.Black; 
        }

        private Node Put(Node? node, K key, V value)
        {
            if (node == null)
            {
                size++;
                return new Node(key, value, Color.Red); 
            }

            int cmp = comparator.Compare(key, node.Key);
            if (cmp < 0)
                node.Left = Put(node.Left, key, value);
            else if (cmp > 0)
                node.Right = Put(node.Right, key, value);
            else
                node.Value = value; 

            return Balance(node); // После вставки, сбалансировать дерево
        }
        // метод remove(object key) для удаления пары «ключ-значение» с указанным ключом из отображения
        public V Remove(K key)
        {
            if (!ContainsKey(key)) throw new InvalidOperationException("Key not found.");

            var node = GetNode(root, key);
            root = Remove(root, key);
            size--;
            return node!.Value;
        }

        private Node? Remove(Node? node, K key)
        {
            if (node == null) return null;

            int cmp = comparator.Compare(key, node.Key);

            // Обработка удаления для красно-чёрного дерева
            if (cmp < 0)
            {
                if (!IsRed(node.Left) && !IsRed(node.Left?.Left))
                    node = MoveRedLeft(node);
                node.Left = Remove(node.Left, key);
            }
            else
            {
                if (IsRed(node.Left))
                    node = RotateRight(node);
                if (cmp == 0 && node.Right == null) return null;
                if (!IsRed(node.Right) && !IsRed(node.Right?.Left))
                    node = MoveRedRight(node);
                if (cmp == 0)
                {
                    var minNode = GetMin(node.Right);
                    node.Key = minNode.Key;
                    node.Value = minNode.Value;
                    node.Right = Remove(node.Right, minNode.Key);
                }
                else
                    node.Right = Remove(node.Right, key);
            }
            return Balance(node);
        }
        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);
            if (IsRed(node.Right?.Left))
            {
                node.Right = RotateRight(node.Right!);
                node = RotateLeft(node);
                FlipColors(node);
            }
            return node;
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);
            if (IsRed(node.Left?.Left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }
            return node;
        }
        private Node GetMin(Node? node)
        {
            while (node?.Left != null) node = node.Left;
            return node!;
        }
        // метод size() для возврата количества пар «ключ-значение» в отображении;
        public int Size() => size;
        // метод firstKey() для возврата первого ключа отображения
        public K FirstKey()
        {
            if (IsEmpty()) throw new InvalidOperationException("Tree is empty.");
            return GetMin(root).Key;
        }
        // метод lastKey() для возврата последнего ключа отображения
        public K LastKey()
        {
            if (IsEmpty()) throw new InvalidOperationException("Tree is empty.");
            var node = root;
            while (node?.Right != null) node = node.Right;
            return node!.Key;
        }
        // метод headMap(K end) для возврата сортированного отображения, содержащего элементы, ключ которых меньше end;
        public MyTreeMapRB<K, V> HeadMap(K end)
        {
            var result = new MyTreeMapRB<K, V>(comparator);
            InOrderTraversal(root, node =>
            {
                if (comparator.Compare(node.Key, end) < 0)
                    result.Put(node.Key, node.Value);
            });
            return result;
        }
        //метод subMap(K start, K end) для возврата отображения, содержащего элементы, чей ключ больше или равен start и меньше end;
        public MyTreeMapRB<K, V> SubMap(K start, K end)
        {
            var result = new MyTreeMapRB<K, V>(comparator);
            InOrderTraversal(root, node =>
            {
                if (comparator.Compare(node.Key, start) >= 0 && comparator.Compare(node.Key, end) < 0)
                    result.Put(node.Key, node.Value);
            });
            return result;
        }
        //метод tailMap(K start) для возврата сортированного отображения, содержащего элементы, ключ которых больше start;
        public MyTreeMapRB<K, V> TailMap(K start)
        {
            var result = new MyTreeMapRB<K, V>(comparator);
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

        public Color GetColor(K key)
        {
            var node = GetNode(root, key);
            return node != null ? node.NodeColor : default!;
        }
    }
}
