using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace Lab13
{
    public partial class Form1 : Form
    {
        string pathArray = "array.txt";
        string pathTime = "time.txt";
        public void TimeOfSorting<T>(Func<int, T[]> Generate, int size, params Func<T[], T[]>[] SortingAlg)
        {
            T[] sortedArray = new T[size];
            double[] sumSpeedSort = new double[SortingAlg.Length];
            foreach (var method in SortingAlg)
            {
                T[] warmupArray = Generate(size);
                method((T[])warmupArray.Clone());
            }

            for (int i = 0; i < 20; i++)
            {
                T[] array = Generate(size);

                try
                {
                    using (StreamWriter sw = File.AppendText(pathArray))
                    {
                        sw.WriteLine("Unsorted array: " + (i + 1).ToString());
                        foreach (T item in array)
                            sw.Write(item.ToString() + " ");
                        sw.WriteLine();
                    }

                    int index = 0;

                    foreach (Func<T[], T[]> Method in SortingAlg)
                    {
                        T[] arrayCopy = (T[])array.Clone();
                        Stopwatch timer = new Stopwatch();

                        timer.Start();
                        sortedArray = Method(arrayCopy);
                        timer.Stop();
                        sumSpeedSort[index] += (double)timer.ElapsedTicks / Stopwatch.Frequency * 1000;
                        index++;
                    }

                    using (StreamWriter sw = File.AppendText(pathArray))
                    {
                        sw.WriteLine("Sorted array: " + (i + 1).ToString());
                        foreach (T item in sortedArray) sw.Write(item.ToString() + " ");
                        sw.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                using (StreamWriter sw = File.AppendText(pathTime))
                {
                    for (int i = 0; i < sumSpeedSort.Length; i++)
                    {
                        if (i == 0)
                        {
                            sw.Write((sumSpeedSort[i] / 20).ToString("F2"));
                        }
                        else
                        {
                            sw.Write(" " + (sumSpeedSort[i] / 20).ToString("F2"));
                        }
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.XAxis.Title.Text = "Массив 10^x, элементов";
            pane.YAxis.Title.Text = "Время выполнения, мс";
            pane.Title.Text = "График зависимости времени выполнения сортировки от количества этеметов";
            pane.XAxis.Scale.MinAuto = false;
            pane.XAxis.Scale.MaxAuto = false;
        }   

        int check = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            pathArray = "array.txt";
            pathTime = "time.txt";

            File.WriteAllText(pathTime, String.Empty);
            File.WriteAllText(pathArray, String.Empty);
            var sortingAlgorithms = new SortingAlgorithms<int>();
            var sortingAlgorithmsChar = new SortingAlgorithms<char>();
            var sortingAlgorithmsString = new SortingAlgorithms<string>();
            var sortingAlgorithmsDouble = new SortingAlgorithms<double>();
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0:
                                    for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateArray, size,
                                                           sortingAlgorithms.BubbleSort,
                                                           sortingAlgorithms.InsertionSort,
                                                           sortingAlgorithms.SelectionSort,
                                                           sortingAlgorithms.ShakerSort,
                                                           sortingAlgorithms.GnomeSort);
                                    check = 1;
                                    break;
                                case 1:
                                    for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateArray, size,
                                                           sortingAlgorithms.BitonicSort,
                                                           sortingAlgorithms.ShellSort,
                                                           sortingAlgorithms.TreeSort);
                                    check = 2;
                                    break;
                                case 2:
                                    for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateArray, size,
                                                           sortingAlgorithms.CombSort,
                                                           sortingAlgorithms.HeapSort,
                                                           sortingAlgorithms.MergeSort,
                                                           sortingAlgorithms.QuickSort);
                                    check = 3;
                                    break;
                            }
                            break;
                        case 1:
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0:
                                    for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateSubArrays, size,
                                                           sortingAlgorithms.BubbleSort,
                                                           sortingAlgorithms.InsertionSort,
                                                           sortingAlgorithms.SelectionSort,
                                                           sortingAlgorithms.ShakerSort,
                                                           sortingAlgorithms.GnomeSort);
                                    check = 1;
                                    break;
                                case 1:
                                    for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateSubArrays, size,
                                                           sortingAlgorithms.BitonicSort,
                                                           sortingAlgorithms.ShellSort,
                                                           sortingAlgorithms.TreeSort);
                                    check = 2;
                                    break;
                                case 2:
                                    for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateSubArrays, size,
                                                           sortingAlgorithms.CombSort,
                                                           sortingAlgorithms.HeapSort,
                                                           sortingAlgorithms.QuickSort,
                                                           sortingAlgorithms.MergeSort);
                                    check = 3;
                                    break;
                            }
                            break;
                        case 2:
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0:
                                    for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateBySwap, size,
                                                           sortingAlgorithms.BubbleSort,
                                                           sortingAlgorithms.InsertionSort,
                                                           sortingAlgorithms.SelectionSort,
                                                           sortingAlgorithms.ShakerSort,
                                                           sortingAlgorithms.GnomeSort);
                                    check = 1;
                                    break;
                                case 1:
                                    for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateBySwap, size,
                                                           sortingAlgorithms.BitonicSort,
                                                           sortingAlgorithms.ShellSort,
                                                           sortingAlgorithms.TreeSort);
                                    check = 2;
                                    break;
                                case 2:
                                    for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateBySwap, size,
                                                           sortingAlgorithms.CombSort,
                                                           sortingAlgorithms.HeapSort,
                                                           sortingAlgorithms.QuickSort,
                                                           sortingAlgorithms.MergeSort);
                                    check = 3;
                                    break;
                            }
                            break;
                        case 3:
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0:
                                    for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateSwapAndRepeat, size,
                                                           sortingAlgorithms.BubbleSort,
                                                           sortingAlgorithms.InsertionSort,
                                                           sortingAlgorithms.SelectionSort,
                                                           sortingAlgorithms.ShakerSort,
                                                           sortingAlgorithms.GnomeSort);
                                    check = 1;
                                    break;
                                case 1:
                                    for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateSwapAndRepeat, size,
                                                           sortingAlgorithms.BitonicSort,
                                                           sortingAlgorithms.ShellSort,
                                                           sortingAlgorithms.TreeSort);
                                    check = 2;
                                    break;
                                case 2:
                                    for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                        this.TimeOfSorting(Generate.GenerateSwapAndRepeat, size,
                                                           sortingAlgorithms.CombSort,
                                                           sortingAlgorithms.HeapSort,
                                                           sortingAlgorithms.QuickSort,
                                                           sortingAlgorithms.MergeSort);
                                    check = 3;
                                    break;
                            }
                            break;

                    }
                    break;
                case 1:
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                this.TimeOfSorting(Generate.GenerateCharArray, size,
                                                   sortingAlgorithmsChar.BubbleSort,
                                                   sortingAlgorithmsChar.InsertionSort,
                                                   sortingAlgorithmsChar.SelectionSort,
                                                   sortingAlgorithmsChar.ShakerSort,
                                                   sortingAlgorithmsChar.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                this.TimeOfSorting(Generate.GenerateCharArray, size,
                                                   sortingAlgorithmsChar.BitonicSort,
                                                   sortingAlgorithmsChar.ShellSort,
                                                   sortingAlgorithmsChar.TreeSort);
                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                this.TimeOfSorting(Generate.GenerateCharArray, size,
                                                   sortingAlgorithmsChar.CombSort,
                                                   sortingAlgorithmsChar.HeapSort,
                                                   sortingAlgorithmsChar.MergeSort,
                                                   sortingAlgorithmsChar.QuickSort);
                            check = 3;
                            break;
                    }
                    break;
                case 2:
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                this.TimeOfSorting(Generate.GenerateStringArray, size,
                                                   sortingAlgorithmsString.BubbleSort,
                                                   sortingAlgorithmsString.InsertionSort,
                                                   sortingAlgorithmsString.SelectionSort,
                                                   sortingAlgorithmsString.ShakerSort,
                                                   sortingAlgorithmsString.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                this.TimeOfSorting(Generate.GenerateStringArray, size,
                                                   sortingAlgorithmsString.BitonicSort,
                                                   sortingAlgorithmsString.ShellSort,
                                                   sortingAlgorithmsString.TreeSort);
                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                this.TimeOfSorting(Generate.GenerateStringArray, size,
                                                   sortingAlgorithmsString.CombSort,
                                                   sortingAlgorithmsString.HeapSort,
                                                   sortingAlgorithmsString.MergeSort,
                                                   sortingAlgorithmsString.QuickSort);
                            check = 3;
                            break;
                    }
                    break;
                case 3:
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                this.TimeOfSorting(Generate.GenerateDoubleArray, size,
                                                   sortingAlgorithmsDouble.BubbleSort,
                                                   sortingAlgorithmsDouble.InsertionSort,
                                                   sortingAlgorithmsDouble.SelectionSort,
                                                   sortingAlgorithmsDouble.ShakerSort,
                                                   sortingAlgorithmsDouble.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                this.TimeOfSorting(Generate.GenerateDoubleArray, size,
                                                   sortingAlgorithmsDouble.BitonicSort,
                                                   sortingAlgorithmsDouble.ShellSort,
                                                   sortingAlgorithmsDouble.TreeSort);
                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                this.TimeOfSorting(Generate.GenerateDoubleArray, size,
                                                   sortingAlgorithmsDouble.CombSort,
                                                   sortingAlgorithmsDouble.HeapSort,
                                                   sortingAlgorithmsDouble.MergeSort,
                                                   sortingAlgorithmsDouble.QuickSort);
                            check = 3;
                            break;
                    }
                    break;
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            List<List<double>> list = new List<List<double>>();
            pathArray = "array.txt";
            pathTime = "time.txt";
            try
            {
                StreamReader sr = new StreamReader(pathTime);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] lineArray = line.Split(' ');
                    List<double> time = new List<double>();
                    foreach (string s in lineArray)
                        time.Add(Convert.ToDouble(s));
                    list.Add(time);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            for (int i = 0; i < list[0].Count(); i++) // Error out of index
            {
                PointPairList pointList = new PointPairList();
                int x = 10;
                for (int j = 0; j < list.Count(); j++)
                {
                    pointList.Add(x, list[j][i]);
                    x *= 10;
                }
                switch (check)
                {
                    case 1:
                        pane.XAxis.Scale.Max = 10000;
                        pane.XAxis.Scale.Min = 10;
                        switch (i)
                        {
                            case 0:
                                pane.AddCurve("BubbleSort", pointList, Color.Black, SymbolType.Default);
                                break;
                            case 1:
                                pane.AddCurve("InsertionSort", pointList, Color.Blue, SymbolType.Default);
                                break;
                            case 2:
                                pane.AddCurve("SelectionSort", pointList, Color.Green, SymbolType.Default);
                                break;
                            case 3:
                                pane.AddCurve("ShakerSort", pointList, Color.Yellow, SymbolType.Default);
                                break;
                            case 4:
                                pane.AddCurve("GnomeSort", pointList, Color.Red, SymbolType.Default);
                                break;
                        }
                        break;
                    case 2:
                        pane.XAxis.Scale.Max = 100000;
                        pane.XAxis.Scale.Min = 10;
                        switch (i)
                        {
                            case 0:
                                pane.AddCurve("BitonicSort", pointList, Color.Red, SymbolType.Default);
                                break;
                            case 1:
                                pane.AddCurve("ShellSort", pointList, Color.Yellow, SymbolType.Default);
                                break;
                            case 2:
                                pane.AddCurve("TreeSort", pointList, Color.Orange, SymbolType.Default);
                                break;
                        }
                        break;
                    case 3:
                        pane.XAxis.Scale.Max = 1000000;
                        pane.XAxis.Scale.Min = 10;
                        switch (i)
                        {
                            case 0:
                                pane.AddCurve("CombSort", pointList, Color.Red, SymbolType.Default);
                                break;
                            case 1:
                                pane.AddCurve("HeapSort", pointList, Color.Blue, SymbolType.Default);
                                break;
                            case 2:
                                pane.AddCurve("MergeSort", pointList, Color.Yellow, SymbolType.Default);
                                break;
                            case 3:
                                pane.AddCurve("QuickSort", pointList, Color.Green, SymbolType.Default);
                                break;                           
                            case 4:
                                pane.AddCurve("RadixSort", pointList, Color.Purple, SymbolType.Default);
                                break;
                        }
                        break;
                }
            }
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
    public static class Generate
    {
        // Генерация массива случайных чисел указанного размера
        public static int[] GenerateArray(int size)
        {
            Random rand = new Random();
            return Enumerable.Range(0, size).Select(_ => rand.Next(1, 100)).ToArray();
        }

        // Генерация массива с подмассивами, содержащими случайные числа
        public static int[] GenerateSubArrays(int size)
        {
            Random rand = new Random();
            int[] array = new int[size];

            int subArraySize = Math.Max(1, size / 10); // Определяем размер подмассива
            for (int i = 0; i < size; i += subArraySize)
            {
                for (int j = 0; j < subArraySize && i + j < size; j++)
                {
                    array[i + j] = rand.Next(1, 100);
                }
            }

            return array;
        }

        // Генерация массива, в котором элементы случайным образом переставляются
        public static int[] GenerateBySwap(int size)
        {
            int[] array = Enumerable.Range(0, size).ToArray();
            Random rand = new Random();

            // Перестановка элементов в массиве
            for (int i = 0; i < size; i++)
            {
                int j = rand.Next(i, size);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return array;
        }

        // Генерация массива, который содержит как перестановки, так и повторяющиеся элементы
        public static int[] GenerateSwapAndRepeat(int size)
        {
            int[] array = new int[size];
            Random rand = new Random();

            // Половина массива содержит случайные числа
            for (int i = 0; i < size / 2; i++)
            {
                array[i] = rand.Next(1, 100);
            }

            // Другая половина повторяет первую часть, но с некоторыми перестановками
            for (int i = size / 2; i < size; i++)
            {
                int index = rand.Next(0, size / 2);
                array[i] = array[index];
            }

            return array;
        }
        public static char[] GenerateCharArray(int size)
        {
            char[] array = new char[size];
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = (char)rand.Next('A', 'z' + 1);
            }
            return array;
        }
        public static string[] GenerateStringArray(int size)
        {
            string[] array = new string[size];
            Random random = new Random(); 
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 10; 
            for (int i=0; i<size; i++)
                array[i] = new string(Enumerable.Repeat(chars, length) .Select(s => s[random.Next(s.Length)]).ToArray());
            return array;
        }
        public static double[] GenerateDoubleArray(int size)
        {
            double[] array = new double[size];
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = 0.1+(100-0.1)*rand.NextDouble();
            }
            return array;
        }
    }
    public class SortingAlgorithms<T>
    {
        private Comparer<T> comparer;

        public SortingAlgorithms(Comparer<T> comparer = null)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        // 1. Bubble Sort
        public T[] BubbleSort(T[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (comparer.Compare(arr[j], arr[j + 1]) > 0)
                    {
                        Swap(arr, j, j + 1);
                    }
                }
            }
            return arr;
        }

        // 2. Shaker Sort
        public T[] ShakerSort(T[] arr)
        {
            for (int i = 0; i < arr.Length / 2; i++)
            {
                bool swapped = false;
                for (int j = i; j < arr.Length - i - 1; j++)
                {
                    if (comparer.Compare(arr[j], arr[j + 1]) > 0)
                    {
                        Swap(arr, j, j + 1);
                        swapped = true;
                    }
                }
                for (int j = arr.Length - i - 2; j > i; j--)
                {
                    if (comparer.Compare(arr[j], arr[j - 1]) < 0)
                    {
                        Swap(arr, j, j - 1);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
            return arr;
        }

        // 3. Comb Sort
        public T[] CombSort(T[] arr)
        {
            double gap = arr.Length;
            bool swapped = true;
            while (gap > 1 || swapped)
            {
                gap = Math.Max(1, gap / 1.3);
                swapped = false;
                for (int i = 0; i + (int)gap < arr.Length; i++)
                {
                    int j = i + (int)gap;
                    if (comparer.Compare(arr[i], arr[j]) > 0)
                    {
                        Swap(arr, i, j);
                        swapped = true;
                    }
                }
            }
            return arr;
        }

        // 4. Insertion Sort
        public T[] InsertionSort(T[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                T key = arr[i];
                int j = i - 1;
                while (j >= 0 && comparer.Compare(arr[j], key) > 0)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
            return arr;
        }

        // 5. Shell Sort
        public T[] ShellSort(T[] arr)
        {
            int gap = arr.Length / 2;
            while (gap > 0)
            {
                for (int i = gap; i < arr.Length; i++)
                {
                    T temp = arr[i];
                    int j = i;
                    while (j >= gap && comparer.Compare(arr[j - gap], temp) > 0)
                    {
                        arr[j] = arr[j - gap];
                        j -= gap;
                    }
                    arr[j] = temp;
                }
                gap /= 2;
            }
            return arr;
        }

        // 6. Gnome Sort
        public T[] GnomeSort(T[] arr)
        {
            int i = 1;
            while (i < arr.Length)
            {
                if (i == 0 || comparer.Compare(arr[i], arr[i - 1]) >= 0)
                {
                    i++;
                }
                else
                {
                    Swap(arr, i, i - 1);
                    i--;
                }
            }
            return arr;
        }

        // 7. Selection Sort
        public T[] SelectionSort(T[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (comparer.Compare(arr[j], arr[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                Swap(arr, minIndex, i);
            }
            return arr;
        }

        // 8. Quick Sort
        public T[] QuickSort(T[] array)
        {
            bool swap = false; 
            QuickSortInternal(array, 0, array.Length, swap);
            return array;
        }

        private void QuickSortInternal(T[] array, int left, int right, bool swap)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, swap);
                QuickSortInternal(array, left, pivotIndex, swap);
                QuickSortInternal(array, pivotIndex + 1, right, swap);
            }
        }

        private int Partition(T[] array, int start, int stop, bool swap)
        {
            int left = start;
            T pivot = array[left];
            int pivotIndex = left;

            for (int i = start + 1; i < stop; i++)
            {
                // Проверка условия для сортировки (swap определяет порядок)
                bool shouldSwap = (!swap && comparer.Compare(array[i], pivot) < 0) ||
                                  (swap && comparer.Compare(array[i], pivot) > 0);

                if (shouldSwap)
                {
                    left++;
                    Swap(array, i, left);
                }

                if (comparer.Compare(array[i], pivot) == 0)
                {
                    pivotIndex = i;
                }
                else if (comparer.Compare(array[left], pivot) == 0)
                {
                    pivotIndex = left;
                }
            }

            // Перемещаем опорный элемент в финальное положение
            Swap(array, pivotIndex, left);
            return left;
        }

        // 9. Merge Sort
        public T[] MergeSort(T[] arr)
        {
            if (arr.Length <= 1) return (T[])arr.Clone(); 
            int mid = arr.Length / 2;
            T[] left = new T[mid];
            T[] right = new T[arr.Length - mid];
            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, right, 0, arr.Length - mid);
            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }

        private T[] Merge(T[] left, T[] right)
        {
            T[] result = new T[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                result[k++] = comparer.Compare(left[i], right[j]) <= 0 ? left[i++] : right[j++];
            }
            while (i < left.Length) result[k++] = left[i++];
            while (j < right.Length) result[k++] = right[j++];

            return result;
        }


        // 10. Counting Sort
        public T[] CountingSort(T[] array, Func<T, int> keySelector)
        {
            if (array.Length == 0) return array;

            int maxValue = keySelector(array[0]);
            int minValue = keySelector(array[0]);

            foreach (var item in array)
            {
                int key = keySelector(item);
                if (key > maxValue) maxValue = key;
                if (key < minValue) minValue = key;
            }

            int range = maxValue - minValue + 1;
            int[] counts = new int[range];
            T[] output = new T[array.Length];

            foreach (var item in array)
            {
                counts[keySelector(item) - minValue]++;
            }

            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] += counts[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                int key = keySelector(array[i]);
                output[--counts[key - minValue]] = array[i];
            }

            return output;
        }

        // 11. Radix Sort (ограничение: предполагается, что T - это int или может быть разложен на разряды)
        public T[] RadixSort(T[] array, Func<T, int> keySelector)
        {
            if (array.Length == 0) return array;

            int maxValue = keySelector(array[0]);
            foreach (var item in array)
            {
                int key = keySelector(item);
                if (key > maxValue) maxValue = key;
            }

            int exponent = 1;
            while (maxValue / exponent > 0)
            {
                array = CountingSortByDigit(array, keySelector, exponent);
                exponent *= 10;
            }

            return array;
        }

        private T[] CountingSortByDigit(T[] array, Func<T, int> keySelector, int exponent)
        {
            int[] counts = new int[10];
            T[] output = new T[array.Length];

            foreach (var item in array)
            {
                int digit = (keySelector(item) / exponent) % 10;
                counts[digit]++;
            }

            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] += counts[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                int digit = (keySelector(array[i]) / exponent) % 10;
                output[--counts[digit]] = array[i];
            }

            return output;
        }

        // 12. Heap Sort
        public T[] HeapSort(T[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                Swap(arr, 0, i);
                Heapify(arr, i, 0);
            }
            return arr;
        }

        private void Heapify(T[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && comparer.Compare(arr[left], arr[largest]) > 0) largest = left;
            if (right < n && comparer.Compare(arr[right], arr[largest]) > 0) largest = right;
            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, n, largest);
            }
        }
        // 13. Tree Sort
        public T[] TreeSort(T[] array)
        {
            if (array.Length == 0) return array;

            var tree = new SortedSet<T>(comparer);
            foreach (var item in array)
            {
                tree.Add(item);
            }

            T[] sortedArray = new T[array.Length];
            tree.CopyTo(sortedArray);
            return sortedArray;
        }

        // 14. Bitonic Sort
        public T[] BitonicSort(T[] array)
        {
            BitonicSortRecursive(array, 0, array.Length, true);
            return array;
        }

        private void BitonicSortRecursive(T[] array, int low, int count, bool ascending)
        {
            if (count > 1)
            {
                int k = count / 2;

                BitonicSortRecursive(array, low, k, true);
                BitonicSortRecursive(array, low + k, count - k, false);
                BitonicMerge(array, low, count, ascending);
            }
        }

        private void BitonicMerge(T[] array, int low, int count, bool ascending)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                {
                    if ((comparer.Compare(array[i], array[i + k]) > 0) == ascending)
                    {
                        Swap(array, i, i + k);
                    }
                }
                BitonicMerge(array, low, k, ascending);
                BitonicMerge(array, low + k, count - k, ascending);
            }
        }

        private void Swap(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
