using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Diagnostics;

namespace Lab17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            int cb = comboBox1.SelectedIndex;
            switch (cb)
            {
                case 0:
                    PointPairList pointList = new PointPairList();
                    PointPairList pointList1 = new PointPairList();
                    Stopwatch sw = new Stopwatch();
                    long[] time = new long[4];
                    MyArrayList<int> dynamic5 = new MyArrayList<int>();
                    MyArrayList<int> dynamic6 = new MyArrayList<int>();
                    MyArrayList<int> dynamic7 = new MyArrayList<int>();
                    MyArrayList<int> dynamic8 = new MyArrayList<int>();
                    MyLinkedList<int> link5 = new MyLinkedList<int>();
                    MyLinkedList<int> link6 = new MyLinkedList<int>();
                    MyLinkedList<int> link7 = new MyLinkedList<int>();
                    MyLinkedList<int> link8 = new MyLinkedList<int>();
                    Random rand = new Random();
                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 10; i++)
                        {
                            dynamic5.Add(rand.Next(1, 10));
                        }
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;

                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 100; i++)
                        {
                            dynamic6.Add(rand.Next(1, 10));
                        }
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                    }
                    time[1] /= 20;
                    
                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 1000; i++)
                        {
                            dynamic7.Add(rand.Next(1, 10));
                        }
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    
                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 10000; i++)
                        {
                            dynamic8.Add(rand.Next(1, 10));
                        }
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
                    LineItem myCurve = pane.AddCurve("Dynamic", pointList, Color.Black, SymbolType.None);
                    for (int i=0; i<time.Length; i++)
                    {
                        time[i] = 0;
                    }
                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 1000; i++)
                        {
                            link5.Add(rand.Next(1, 10));
                        }
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;

                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 10000; i++)
                        {
                            link6.Add(rand.Next(1, 10));
                        }
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                    }
                    time[1] /= 20;

                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 100000; i++)
                        {
                            link7.Add(rand.Next(1, 10));
                        }
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;

                    for (int k = 0; k < 20; k++)
                    {
                        sw.Start();
                        for (int i = 0; i < 1000000; i++)
                        {
                            link8.Add(rand.Next(1, 10));
                        }
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
                    sw.Reset();                    
                    myCurve = pane.AddCurve("Link", pointList1, Color.Red, SymbolType.None);

                    break;
                case 1:
                    pointList = new PointPairList();
                    pointList1 = new PointPairList();
                    sw = new Stopwatch();
                    time = new long[4];
                    int[] l5 = new int[100000];
                    int[] l6 = new int[1000000];
                    int[] l7 = new int[10000000];
                    int[] l8 = new int[100000000];
                    dynamic5 = new MyArrayList<int>(l5);
                    dynamic6 = new MyArrayList<int>(l6);
                    dynamic7 = new MyArrayList<int>(l7);
                    dynamic8 = new MyArrayList<int>(l8);                    
                    link5 = new MyLinkedList<int>(l5);
                    link6 = new MyLinkedList<int>(l6);
                    link7 = new MyLinkedList<int>(l7);
                    link8 = new MyLinkedList<int>(l8);
                    rand = new Random();
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        link5.Set(randIndex, 1);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        link6.Set(randIndex, 1);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        link7.Set(randIndex, 1);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        link8.Set(randIndex, 1);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 20;
                    pw = 10;
                    for(int i=0; i<time.Length; i++)
                    {
                        pointList1.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new long[4];
                    myCurve = pane.AddCurve("Link", pointList1, Color.Red, SymbolType.None);


                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size()); 
                        sw.Start();
                        dynamic5.Set(randIndex, 1);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        dynamic6.Set(randIndex, 1);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        dynamic7.Set(randIndex, 1);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        dynamic8.Set(randIndex, 1);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 20;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new long[4];
                    myCurve = pane.AddCurve("Dynamic", pointList, Color.Black, SymbolType.None);
                    break;
                case 2:
                    pointList = new PointPairList();
                    pointList1 = new PointPairList();
                    sw = new Stopwatch();
                    time = new long[4];
                    dynamic5 = new MyArrayList<int>(100000);
                    dynamic6 = new MyArrayList<int>(1000000);
                    dynamic7 = new MyArrayList<int>(10000000);
                    dynamic8 = new MyArrayList<int>(100000000);
                    l5 = new int[100000];
                    l6 = new int[1000000];
                    l7 = new int[10000000];
                    l8 = new int[100000000];
                    link5 = new MyLinkedList<int>(l5);
                    link6 = new MyLinkedList<int>(l6);
                    link7 = new MyLinkedList<int>(l7);
                    link8 = new MyLinkedList<int>(l8);
                    rand = new Random();
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        link5.Get(randIndex);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        link6.Get(randIndex);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        link7.Get(randIndex);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        link8.Get(randIndex);
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
                    time = new long[4];
                    myCurve = pane.AddCurve("Link", pointList1, Color.Red, SymbolType.None);


                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        dynamic5.Get(randIndex);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        dynamic6.Get(randIndex);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        dynamic7.Get(randIndex);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        dynamic8.Get(randIndex);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 20;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new long[4];
                    myCurve = pane.AddCurve("Dynamic", pointList, Color.Black, SymbolType.None);
                    break;
                case 3:
                    pointList = new PointPairList();
                    pointList1 = new PointPairList();
                    sw = new Stopwatch();
                    time = new long[4];
                    dynamic5 = new MyArrayList<int>(10000);
                    dynamic6 = new MyArrayList<int>(100000);
                    dynamic7 = new MyArrayList<int>(1000000);
                    dynamic8 = new MyArrayList<int>(10000000);
                    l5 = new int[10000];
                    l6 = new int[100000];
                    l7 = new int[1000000];
                    l8 = new int[10000000];
                    link5 = new MyLinkedList<int>(l5);
                    link6 = new MyLinkedList<int>(l6);
                    link7 = new MyLinkedList<int>(l7);
                    link8 = new MyLinkedList<int>(l8);
                    rand = new Random();
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        link5.Add(randIndex, 1);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        link6.Add(randIndex, 1);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        link7.Add(randIndex, 1);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        link8.Add(randIndex, 1);
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
                    time = new long[4];
                    myCurve = pane.AddCurve("Link", pointList1, Color.Red, SymbolType.None);


                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        dynamic5.Add(randIndex, 1);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        dynamic6.Add(randIndex, 1);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        dynamic7.Add(randIndex, 1);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        dynamic8.Add(randIndex, 1);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 20;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new long[4];
                    myCurve = pane.AddCurve("Dynamic", pointList, Color.Black, SymbolType.None);
                    break;
                case 4:
                    pointList = new PointPairList();
                    pointList1 = new PointPairList();
                    sw = new Stopwatch();
                    time = new long[4];
                    dynamic5 = new MyArrayList<int>(100000);
                    dynamic6 = new MyArrayList<int>(1000000);
                    dynamic7 = new MyArrayList<int>(10000000);
                    dynamic8 = new MyArrayList<int>(100000000);
                    l5 = new int[100000];
                    l6 = new int[1000000];
                    l7 = new int[10000000];
                    l8 = new int[100000000];
                    link5 = new MyLinkedList<int>(l5);
                    link6 = new MyLinkedList<int>(l6);
                    link7 = new MyLinkedList<int>(l7);
                    link8 = new MyLinkedList<int>(l8);
                    rand = new Random();
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        link5.RemoveInd(randIndex);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        link6.RemoveInd(randIndex);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        link7.RemoveInd(randIndex);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        link8.RemoveInd(randIndex);
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
                    time = new long[4];
                    myCurve = pane.AddCurve("Link", pointList1, Color.Red, SymbolType.None);


                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link5.Size());
                        sw.Start();
                        dynamic5.RemoveInd(randIndex);
                        sw.Stop();
                        time[0] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[0] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link6.Size());
                        sw.Start();
                        dynamic6.RemoveInd(randIndex);
                        sw.Stop();
                        time[1] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[1] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link7.Size());
                        sw.Start();
                        dynamic7.RemoveInd(randIndex);
                        sw.Stop();
                        time[2] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[2] /= 20;
                    for (int k = 0; k < 20; k++)
                    {
                        int randIndex = rand.Next(0, link8.Size());
                        sw.Start();
                        dynamic8.RemoveInd(randIndex);
                        sw.Stop();
                        time[3] += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    time[3] /= 20;
                    pw = 10;
                    for (int i = 0; i < time.Length; i++)
                    {
                        pointList.Add(pw, time[i]);
                        pw *= 10;
                    }
                    time = new long[4];
                    myCurve = pane.AddCurve("Dynamic", pointList, Color.Black, SymbolType.None);
                    break;

            }
            pane.XAxis.Scale.Max = 10000;
            pane.XAxis.Scale.Min = 10;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}

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
        // Проверяем, есть ли место в массиве, если нет — увеличиваем его
        if (size == elementData.Length)
        {
            T[] newData = new T[elementData.Length + 2];
            for (int i = 0; i < elementData.Length; i++)
            {
                newData[i] = elementData[i];
            }
            elementData = newData;
        }

        // Добавляем новый элемент и увеличиваем размер
        elementData[size] = e;
        size++;
    }

    public bool Contains(object o) //  для проверки, находится ли указанный объект в динамическом массиве.
    {
        if (elementData != null && size != 0)
        {
            for (int i = 0; i < size; i++)
            {
                if (o.Equals(elementData[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Remove(object o) // для удаления указанного объекта из динамического массива, если он есть там.
    {
        if (o == null || elementData == null)
        {
            return;
        }

        if (Contains(o))
        {
            int index = IndexOf(o);

            if (index >= 0)
            {
                for (int i = index; i < size - 1; i++)
                {
                    elementData[i] = elementData[i + 1];
                }
                elementData[size - 1] = default; // Для значимых типов присваиваем значение по умолчанию
                size--;
            }
        }
    }

    public int Size() //  для получения размера динамического массива в элементах.
    {
        return size;
    }

    public void Add(int index, T e) //для добавления элемента в указанную позицию
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException("index");

        if (elementData == null)
            elementData = new T[1];

        if (size == elementData.Length)
        {
            T[] newArray = new T[size + size / 2 + 1];
            for (int i = 0; i < size; i++)
                newArray[i] = elementData[i];
            elementData = newArray;
        }

        for (int i = size; i > index; i--)
            elementData[i] = elementData[i - 1];

        elementData[index] = e;
        size++;
    }

    public T Get(int index) //для возвращения элемента в указанной позиции.
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
        return elementData[index];
    }

    public int IndexOf(object o) //для возвращения индекса указанного объекта, или -1, если его нет в динамическом массиве
    {
        for (int i = 0; i < size; i++)
            if (o.Equals(elementData[i]))
                return i;
        return -1;
    }
    public T RemoveInd(int index) //для удаления и возвращения элемента в указанной позиции
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
        T e = elementData[index];
        if (e != null)
            Remove(e);
        return e;
    }

    public void Set(int index, T e)//для замены элемента в указанной позиции новым элементом
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (e == null)
            throw new ArgumentNullException();
        elementData[index] = e;
    }
}

public class MyLinkedList<T>
{
    private class LinkElement<E>
    {
        public T value;
        public LinkElement<E> next;
        public LinkElement<E> pred;

        public LinkElement(T element)
        {
            next = null;
            pred = null;
            value = element;
        }
    }
    private LinkElement<T> first;
    private LinkElement<T> last;
    private int size;

    // 1
    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }

    // 2
    public MyLinkedList(T[] a)
    {
        foreach (T item in a)
            Add(item);
    }
    // 3
    public void Add(T e)
    {
        LinkElement<T> element = new LinkElement<T>(e);
        if (size == 0)
        {
            first = element;
            last = element;
        }
        else
        {
            last.next = element;
            element.pred = last;
            last = element;
        }
        size++;
    }
    // 6
    public bool Contains(T o)
    {
        LinkElement<T> step = first;
        while (step != null)
        {
            if (step.value.Equals(o))
                return true;
            step = step.next;
        }
        return false;
    }
    // 9
    public void Remove(T o)
    {
        LinkElement<T> current = first;
        while (current != null)
        {
            if (current.value.Equals(o))
            {
                if (current.pred != null)
                    current.pred.next = current.next;
                else
                    first = current.next;

                if (current.next != null)
                    current.next.pred = current.pred;
                else
                    last = current.pred;

                size--;
                return;
            }
            current = current.next;
        }
    }
    // 12
    public int Size() => size;
    // 15
    public void Add(int index, T e)
    {
        if (index < 0 || index > size)
            throw new IndexOutOfRangeException("Index out of range");

        LinkElement<T> newElement = new LinkElement<T>(e);

        if (index == 0)
        {
            newElement.next = first;
            if (first != null) first.pred = newElement;
            first = newElement;
            if (last == null) last = newElement;
        }
        else if (index == size)
        {
            newElement.pred = last;
            if (last != null) last.next = newElement;
            last = newElement;
        }
        else
        {
            LinkElement<T> current = first;
            for (int i = 0; i < index; i++)
                current = current.next;

            newElement.next = current;
            newElement.pred = current.pred;
            current.pred.next = newElement;
            current.pred = newElement;
        }
        size++;
    }
    // 17
    public T Get(int index)
    {
        if (index < 0 || index >= size)
            throw new IndexOutOfRangeException();

        LinkElement<T> step = first;
        for (int i = 0; i < index; i++)
            step = step.next;

        return step.value;
    }
    // 18
    public int IndexOf(T o)
    {
        int i = 0;
        LinkElement<T> step = first;
        while (step != null)
        {
            if (step.value.Equals(o))
                return i;
            i++;
            step = step.next;
        }
        return -1;
    }
    // 20
    public T RemoveInd(int index)
    {
        T item = Get(index);
        Remove(item);
        return item;
    }
    // 21
    public void Set(int index, T e)
    {
        LinkElement<T> step = first;
        for (int i = 0; i < index; i++)
            step = step.next;

        step.value = e;
    }
}