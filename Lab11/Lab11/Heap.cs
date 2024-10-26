using MyArray;

namespace MyHeap
{
    class MyHeap<T> where T : IComparable<T>
    {
        public MyArrayList<T> heap = new MyArrayList<T>();
        private int size = 0;
        public MyHeap(T[] array)// Конструктор для создания кучи из массива
        {
            size = array.Length;
            for (int i = 0; i < size; i++)
                heap.Add(array[i]);
            for (int i = size / 2 - 1; i >= 0; i--)
                RestoringHeap(i);
        }
        public T SearchMax()// Метод для нахождения максимума кучи
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty.");
            return heap.Get(0);
        }
        public T RemoveMax() // Метод для удаления максимума кучи
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty.");
            T maximum = heap.Get(0);
            heap.Set(0, heap.Get(size - 1));
            size--;
            RestoringHeap(0);
            return maximum;
        }
        public void KeyIncr(int index, T e)// Метод для увеличения ключа
        {
            if (index >= size)
                throw new IndexOutOfRangeException("Index out of range.");
            if (heap.Get(index).CompareTo(e) > 0)
                throw new InvalidOperationException("New key is smaller than the current key.");
            heap.Set(index, e);
            RestoringHeapUp(index);
        }
        public void AddElement(T element)// Метод для добавления нового элемента
        {
            heap.Add(element);
            size++;
            RestoringHeapUp(size - 1);
        }
        public void HeapMerge(MyHeap<T> newHeap)// Метод для слияния куч
        {
            while (newHeap.size > 0)
            {
                T element = newHeap.RemoveMax();
                AddElement(element);
            }
        }

        private void RestoringHeap(int i)// Восстановление свойства кучи
        {
            int left;
            int right;
            int parents = i;
            while (true)
            {
                left = 2 * i + 1;
                right = 2 * i + 2;
                if (right < size && heap.Get(right).CompareTo(heap.Get(parents)) > 0)
                    parents = right;

                if (left < size && heap.Get(left).CompareTo(heap.Get(parents)) > 0)
                    parents = left;
                if (parents == i) break;
                T temp = heap.Get(parents);
                heap.Set(parents, heap.Get(i));
                heap.Set(i, temp);

                i = parents;
            }
        }
        private void RestoringHeapUp(int i)// Восстановление свойства кучи вверх
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (heap.Get(i).CompareTo(heap.Get(parent)) <= 0) break;
                T temp = heap.Get(i);
                heap.Set(i, heap.Get(parent));
                heap.Set(parent, temp);
                i = parent;
            }
        }
        public void Print()// Метод для вывода кучи
        {
            for (int i = 0; i < size; i++)
                Console.Write(heap.Get(i) + " ");
            Console.WriteLine();
        }
    }
}
