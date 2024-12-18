using MyArray;
using IterLib;
using MyVector;
using MyLinkedList;
using MyQueue;
using MyDeque;
using System.Collections.Generic;
using MyHashSet;
using MyTreeSet;

public class Program
{
    static void Main()
    {
        //
        // MyArrayList
        //
        Console.WriteLine("MyArrayList\n");

        var list = new MyArrayList<int>();
        list.AddAll(new[] { 10, 20, 30, 40, 50 });

        Console.WriteLine("Исходный массив:");
        list.PrintArray();

        var iterator = list.ListIterator();

        Console.WriteLine("\nПроверяем метод HasNext и Next:");
        while (iterator.HasNext())
        {
            int current = iterator.Next();
            Console.WriteLine($"Элемент: {current}");
        }

        Console.WriteLine("\nПроверяем метод HasPrevious и Previous:");
        while (iterator.HasPrevious())
        {
            int current = iterator.Previous();
            Console.WriteLine($"Элемент: {current}");
        }

        Console.WriteLine("\nПроверяем методы NextIndex и PreviousIndex:");
        while (iterator.HasNext())
        {
            Console.WriteLine($"NextIndex: {iterator.NextIndex()}");
            iterator.Next();
        }
        while (iterator.HasPrevious())
        {
            Console.WriteLine($"PreviousIndex: {iterator.PreviousIndex()}");
            iterator.Previous();
        }

        Console.WriteLine("\nПроверяем метод Remove:");
        iterator = list.ListIterator();
        while (iterator.HasNext())
        {
            int current = iterator.Next();
            if (current % 20 == 0)
            {
                Console.WriteLine($"Удаляем элемент: {current}");
                iterator.Remove();
                list.PrintArray();
            }
        }

        Console.WriteLine("\nПроверяем метод Set:");
        iterator = list.ListIterator();
        while (iterator.HasNext())
        {
            int current = iterator.Next();
            if (current == 10) // Заменяем элемент 10 на 100
            {
                Console.WriteLine($"Замена элемента {current} на 100");
                iterator.Set(100);
                list.PrintArray();
            }
        }

        Console.WriteLine("\nПроверяем метод Add:");
        iterator = list.ListIterator();
        while (iterator.HasNext())
        {
            iterator.Next();
            if (iterator.NextIndex() == 2) // Добавляем 60 после второго элемента
            {
                Console.WriteLine("Добавляем элемент 60 на позицию 2");
                iterator.Add(60);
                list.PrintArray();
                break;
            }
        }

        Console.WriteLine("\nКонечное состояние массива:");
        list.PrintArray();

        //
        // Vector
        //
        Console.WriteLine("\n\nMyVector\n");

        MyVector<int> vector = new MyVector<int>();
        vector.Add(1);
        vector.Add(2);
        vector.Add(3);
        vector.Add(4);

        MyVector<int>.MyListItr iteratorVector = vector.ListIterator();

        // Пример работы с итератором
        Console.WriteLine("Forward traversal:");
        while (iteratorVector.HasNext())
        {
            Console.WriteLine(iteratorVector.Next());  // Выведет 1, 2, 3, 4
        }

        // Пример работы с итератором в обратном порядке
        Console.WriteLine("Backward traversal:");
        while (iteratorVector.HasPrevious())
        {
            Console.WriteLine(iteratorVector.Previous());  // Выведет 4, 3, 2, 1
        }

        // Пример использования Add() и Set()

        Console.WriteLine("\nПроверяем метод Set:");
        iteratorVector = vector.ListIterator();
        while (iteratorVector.HasNext())
        {
            int current = iteratorVector.Next();
            if (current == 2) // Заменяем элемент 10 на 100
            {
                Console.WriteLine($"Замена элемента {current} на 100");
                iteratorVector.Set(10);
                vector.Print();
            }
        }

        Console.WriteLine("\nПроверяем метод Add:");
        iteratorVector = vector.ListIterator();
        while (iteratorVector.HasNext())
        {
            iteratorVector.Next();
            if (iteratorVector.NextIndex() == 2) // Добавляем 60 после второго элемента
            {
                Console.WriteLine("Добавляем элемент 60 на позицию 3");
                iteratorVector.Add(60);
                vector.Print();
                break;
            }
        }

        //
        // MyLinkedList
        //
        Console.WriteLine("\n\nMyLinkedList\n");

        MyLinkedList<int> link = new MyLinkedList<int>(new int[] { 1, 2, 3, 4, 5 });

        // Получение итератора
        var iteratorLink = link.ListIterator();

        Console.WriteLine("Проверяем итератор с начального элемента:");
        while (iteratorLink.HasNext())
        {
            Console.WriteLine($"Next элемент: {iteratorLink.Next()}");
        }

        Console.WriteLine("\nПроверяем итератор с конца:");
        while (iteratorLink.HasPrevious())
        {
            Console.WriteLine($"Previous элемент: {iteratorLink.Previous()}");
        }

        Console.WriteLine("\nДобавляем элемент с помощью итератора:");
        iteratorLink.Add(6); // Добавляем 6 на текущую позицию
        link.PrintLinkList();

        iteratorLink.Next();
        Console.WriteLine("\nУдаляем текущий элемент с помощью итератора:");
        iteratorLink.Remove(); // Удаляем элемент 1
        link.PrintLinkList();

        Console.WriteLine("\nУстанавливаем элемент на текущую позицию:");
        if (iteratorLink.HasNext())
        {
            iteratorLink.Next();
            iteratorLink.Set(10); // Заменяем текущий элемент на 10
        }
        link.PrintLinkList();

        //
        // MyPriorityQueue
        //
        Console.WriteLine("\n\nMyQueue\n");

        // Создаем экземпляр MyPriorityQueue
        MyPriorityQueue<int> priorityQueue = new MyPriorityQueue<int>();

        // Добавляем элементы
        priorityQueue.Add(10);
        priorityQueue.Add(20);
        priorityQueue.Add(5);
        priorityQueue.Add(30);

        Console.WriteLine("Исходная очередь:");
        priorityQueue.PrintQueue();

        // Получаем итератор
        var iteratorQueue = priorityQueue.Iterator();

        Console.WriteLine("\nИтерация по элементам очереди:");
        while (iteratorQueue.HasNext())
        {
            int element = iteratorQueue.Next();
            Console.WriteLine($"Текущий элемент: {element}");

            // Удаляем элемент, если он равен 20
            if (element == 20)
            {
                Console.WriteLine("Удаление элемента 20.");
                iteratorQueue.Remove();
            }
        }

        Console.WriteLine("\nОчередь после итерации и удаления:");
        priorityQueue.PrintQueue();

        //
        // MyDeque
        //
        Console.WriteLine("\n\nMyDeque\n");

        MyArrayDeque<int> deque = new MyArrayDeque<int>();

        // Добавление элементов
        deque.AddLast(10);
        deque.AddLast(20);
        deque.AddFirst(5);
        deque.AddFirst(1);

        Console.WriteLine("Очередь после добавления элементов:");
        deque.PrintDeque();

        // Получаем итератор
        var iteratorDeque = deque.Iterator();

        Console.WriteLine("\nИтерация по элементам очереди:");
        while (iteratorDeque.HasNext())
        {
            int element = iteratorDeque.Next();
            Console.WriteLine($"Текущий элемент: {element}");

            // Удаление элемента 20
            if (element == 20)
            {
                Console.WriteLine("Удаление элемента 20.");
                iteratorDeque.Remove();
            }
        }

        Console.WriteLine("\nОчередь после итерации и удаления:");
        deque.PrintDeque();

        // Тест операций с головой и хвостом
        Console.WriteLine($"\nПервый элемент: {deque.PeekFirst()}");
        Console.WriteLine($"Последний элемент: {deque.PeekLast()}");

        // Удаление элементов
        deque.RemoveFirst();
        deque.RemoveLast();

        Console.WriteLine("\nОчередь после удаления первого и последнего элементов:");
        deque.PrintDeque();

        //
        // MyHashSet
        //
        Console.WriteLine("\n\nMyHashSet\n");

        // Создаём множество с элементами
        var set = new MyHashSet<int>(new int[] { 10, 20, 30, 40, 50 });

        Console.WriteLine("Элементы множества до итерации:");
        foreach (var item in set.ToArray())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // Проверка итератора
        Console.WriteLine("Итерация по множеству:");
        var iteratorHash = set.Iterator();
        while (iteratorHash.HasNext())
        {
            var item = iteratorHash.Next();
            Console.WriteLine($"Текущий элемент: {item}");

            // Удаляем элемент 30 во время итерации
            if (item == 30)
            {
                Console.WriteLine($"Удаляем элемент: {item}");
                iteratorHash.Remove();
            }
        }

        Console.WriteLine("Элементы множества после итерации:");
        foreach (var item in set.ToArray())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        //
        // MyTreeSet
        //
        Console.WriteLine("\n\nMyTreeSet\n");

        // Создание множества с элементами
        var treeSet = new MyTreeSet<int>(new int[] { 10, 20, 30, 40, 50 });

        Console.WriteLine("Элементы множества до итерации:");
        foreach (var item in treeSet.ToArray())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // Проверка итератора
        Console.WriteLine("Итерация по множеству:");
        var iteratorTree = treeSet.Iterator();
        while (iteratorTree.HasNext())
        {
            var item = iteratorTree.Next();
            Console.WriteLine($"Текущий элемент: {item}");

            // Удаление элемента 30 во время итерации
            if (item == 30)
            {
                Console.WriteLine($"Удаляем элемент: {item}");
                iteratorTree.Remove();
            }
        }

        Console.WriteLine("Элементы множества после итерации:");
        foreach (var item in treeSet.ToArray())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}
