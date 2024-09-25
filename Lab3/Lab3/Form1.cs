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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.ComTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Reflection;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.XAxis.Title.Text = "Massive 10^x";
            pane.YAxis.Title.Text = "Time in mc";
            pane.Title.Text = "TimeCheck";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private int[] array1 = new int[10];
        private int[] array2 = new int[100];
        private int[] array3 = new int[1000];
        private int[] array4 = new int[10000];
        private int[] array5 = new int[100000];
        private int[] array6 = new int[1000000];
        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int modulus = 1000;
            if (comboBox1.SelectedIndex == 0) //пузырьком, вставками, выбором, шейкерная, гномья
            {
                if (comboBox2.SelectedIndex == 0) //Рандом по модую 1000
                {

                    First(rand, array1, array2, array3, array4, modulus);
                }

                if (comboBox2.SelectedIndex == 1) //Массивы, разбитые на несколько отсортированных подмассивов разного размера
                {
                    int[] dimention = new int[6];
                    Second(rand, array1, array2, array3, array4, dimention, modulus);
                }

                if (comboBox2.SelectedIndex == 2) //Изначально отсортированные с некоторым числом перестановаок          ????????
                {
                    Third(rand, array1, array2, array3, array4, modulus);
                }

                if (comboBox2.SelectedIndex == 3) // Полностью отсортированные массивы (в прямом и обратном порядке), массивы с несколькими заменёнными элементами, массивы с большим количеством повторений одного элемента(10 %, 25 %, 50 %, 75 % и 90 %)
                {
                    int[] forvardOrRevers = new int [6];
                    Fourth(rand, array1, array2, array3, array4, modulus, forvardOrRevers);
                }
            }
            if (comboBox1.SelectedIndex == 1) // битонная, Шелла, деревом;
            {
                if (comboBox2.SelectedIndex == 0) //Рандом по модую 1000
                {
                    First(rand, array1, array2, array3, array4, modulus);
                    array5 = Rand1000(rand, array5, modulus);
                }

                if (comboBox2.SelectedIndex == 1) //Массивы, разбитые на несколько отсортированных подмассивов разного размера
                {
                    int[] dimention = new int[6];
                    Second(rand, array1, array2, array3, array4, dimention, modulus);

                    array5 = Rand1000(rand, array5, modulus);
                    dimention[4] = rand.Next(0, array5.Length);
                    sortfor2(array5, dimention[4]);
                }

                if (comboBox2.SelectedIndex == 2) //Изначально отсортированные с некоторым числом перестановаок          ????????
                {
                    Third(rand, array1, array2, array3, array4, modulus);
                    array5 = Rand1000(rand, array5, modulus);
                    Array.Sort(array5);
                    int k = rand.Next(10);
                    for (int i = 0; i < k; i++)
                    {
                        int pois1 = rand.Next(0, array5.Length);
                        int pois2 = rand.Next(0, array5.Length);
                        if (pois1 != pois2)
                        {
                            array5[rand.Next(pois1)] = array5[rand.Next(pois2)];
                        }
                        else i--;
                    }
                }

                if (comboBox2.SelectedIndex == 3) // Полностью отсортированные массивы (в прямом и обратном порядке), массивы с несколькими заменёнными элементами, массивы с большим количеством повторений одного элемента(10 %, 25 %, 50 %, 75 % и 90 %)
                {
                    int[] forvardOrRevers = new int[6];
                    Fourth(rand, array1, array2, array3, array4, modulus, forvardOrRevers);
                    int repVal = rand.Next(0, modulus);
                  
                    ArrayWithRepeat(array5, repVal, modulus, forvardOrRevers[4]);
                }
            }
            if (comboBox1.SelectedIndex == 2) // расчёской, пирамидальная, быстрая, слиянием, подсчётом, поразрядная
            {
                if (comboBox2.SelectedIndex == 0) //Рандом по модую 1000
                {
                    First(rand, array1, array2, array3, array4, modulus);
                    array5 = Rand1000(rand, array5, modulus);
                    array6 = Rand1000(rand, array6, modulus);
                }

                if (comboBox2.SelectedIndex == 1) //Массивы, разбитые на несколько отсортированных подмассивов разного размера
                {
                    int[] dimention = new int[6];

                    Second(rand, array1, array2, array3, array4, dimention, modulus);

                    array5 = Rand1000(rand, array5, modulus);
                    dimention[4] = rand.Next(0, array5.Length);
                    sortfor2(array5, dimention[4]);

                    array6 = Rand1000(rand, array6, modulus);
                    dimention[5] = rand.Next(0, array6.Length);
                    sortfor2(array5, dimention[5]);
                }

                if (comboBox2.SelectedIndex == 2) //Изначально отсортированные с некоторым числом перестановаок          ????????
                {
                    Third(rand, array1, array2, array3, array4, modulus);

                    array5 = Rand1000(rand, array5, modulus);
                    Array.Sort(array5);
                    array6 = Rand1000(rand, array6, modulus);
                    Array.Sort(array6);

                    int k = rand.Next(10);
                    for (int i = 0; i < k; i++)
                    {
                        int pois1 = rand.Next(0, array5.Length);
                        int pois2 = rand.Next(0, array5.Length);
                        if (pois1 != pois2)
                        {
                            array5[rand.Next(pois1)] = array5[rand.Next(pois2)];
                        }
                        else i--;
                    }
                    k = rand.Next(10);
                    for (int i = 0; i < k; i++)
                    {
                        int pois1 = rand.Next(0, array6.Length);
                        int pois2 = rand.Next(0, array6.Length);
                        if (pois1 != pois2)
                        {
                            array6[pois1] = array6[rand.Next(pois2)];
                        }
                        else i--;
                    }
                }

                if (comboBox2.SelectedIndex == 3) // Полностью отсортированные массивы (в прямом и обратном порядке), массивы с несколькими заменёнными элементами, массивы с большим количеством повторений одного элемента(10 %, 25 %, 50 %, 75 % и 90 %)
                {
                    int[] forvardOrRevers = new int[6];
                    Fourth(rand, array1, array2, array3, array4, modulus, forvardOrRevers);
                    int repVal = rand.Next(0, modulus);
                    ArrayWithRepeat(array5, repVal, modulus, forvardOrRevers[4]);
                    repVal = rand.Next(0, modulus);
                    ArrayWithRepeat(array6, repVal, modulus, forvardOrRevers[5]);
                }
            }
        }

        private void First(Random rand, int[]array1, int[] array2, int[] array3, int[] array4, int modulus)
        {
            array1 = Rand1000(rand, array1, modulus);
            array2 = Rand1000(rand, array2, modulus);
            array3 = Rand1000(rand, array3, modulus);
            array4 = Rand1000(rand, array4, modulus);
        }

        private void Second(Random rand, int[]array1, int[] array2, int[] array3, int[] array4, int[] dimention, int modulus)
        {
            array1 = Rand1000(rand, array1, modulus);
            dimention[0] = rand.Next(1, array1.Length);
            sortfor2(array1, dimention[0]);

            array2 = Rand1000(rand, array2, modulus);
            dimention[1] = rand.Next(1, array2.Length);
            sortfor2(array2, dimention[1]);

            array3 = Rand1000(rand, array3, modulus);
            dimention[2] = rand.Next(1, array3.Length);
            sortfor2(array3, dimention[2]);

            array4 = Rand1000(rand, array4, modulus);
            dimention[3] = rand.Next(1, array4.Length);
            sortfor2(array4, dimention[3]);
        }

        private void Third(Random rand, int[] array1, int[] array2, int[] array3, int[] array4, int modulus)
        {
            array1 = Rand1000(rand, array1, modulus);
            Array.Sort(array1);
            int k = rand.Next(10);
            for (int i = 0; i < k; i++)
            {
                int pois1 = rand.Next(0, array1.Length);
                int pois2 = rand.Next(0, array1.Length);
                if (pois1 != pois2)
                {
                    array1[rand.Next(pois1)] = array1[rand.Next(pois2)];
                }
                else i--;
            }
            array2 = Rand1000(rand, array2, modulus);
            Array.Sort(array2);
            k = rand.Next(10);
            for (int i = 0; i < k; i++)
            {
                int pois1 = rand.Next(0, array2.Length);
                int pois2 = rand.Next(0, array2.Length);
                if (pois1 != pois2)
                {
                    array2[rand.Next(pois1)] = array2[rand.Next(pois2)];
                }
                else i--;
            }
            array3 = Rand1000(rand, array3, modulus);
            Array.Sort(array3);
            k = rand.Next(10);
            for (int i = 0; i < k; i++)
            {
                int pois1 = rand.Next(0, array3.Length);
                int pois2 = rand.Next(0, array3.Length);
                if (pois1 != pois2)
                {
                    array3[rand.Next(pois1)] = array3[rand.Next(pois2)];
                }
                else i--;
            }
            array4 = Rand1000(rand, array4, modulus);
            Array.Sort(array4);
            k = rand.Next(10);
            for (int i = 0; i < k; i++)
            {
                int pois1 = rand.Next(0, array4.Length);
                int pois2 = rand.Next(0, array4.Length);
                if (pois1 != pois2)
                {
                    array4[rand.Next(pois1)] = array4[rand.Next(pois2)];
                }
                else i--;
            }
        }

        private void Fourth(Random rand, int[] array1, int[] array2, int[] array3, int[] array4, int modulus, int[] forvardOrRevers)
        {
            for (int i = 0; i < forvardOrRevers.Length; i++)
                forvardOrRevers[i] = rand.Next(0, 2);
            int repVal = rand.Next(0, modulus);
            ArrayWithRepeat(array1, repVal, modulus, forvardOrRevers[0]);
            repVal = rand.Next(0, modulus);
            ArrayWithRepeat(array2, repVal, modulus, forvardOrRevers[1]);
            repVal = rand.Next(0, modulus);
            ArrayWithRepeat(array3, repVal, modulus, forvardOrRevers[2]);
            repVal = rand.Next(0, modulus);
            ArrayWithRepeat(array4, repVal, modulus, forvardOrRevers[3]);
        }

        private void sortfor2(int[]array, int dimention)
        {
            int k = array.Length / dimention;
            int j = 0;
            while (k > 0)
            {
                // Проверяем, чтобы не выйти за границы массива
                int remainingElements = array.Length - j;
                int lengthToSort = Math.Min(dimention, remainingElements); // Чтобы избежать переполнения индекса
                Array.Sort(array, j, lengthToSort);
                j += dimention; k--;
            }
        }

        private int[] Rand1000(Random rand, int[]array, int modulus)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(0, modulus);
            return array;
        }
        private int[] ArrayWithRepeat(int[]array, int repeatingValue, int moduls, int poryadok)
        {
            Random rand = new Random();
            double[] percentages = { 0.10, 0.25, 0.50, 0.75, 0.90 };
            double chosenPercentage = percentages[rand.Next(percentages.Length)];
            int numberOfRepetitions = (int)(array.Length * chosenPercentage);
            for (int i = 0; i < numberOfRepetitions; i++)
                array[i] = repeatingValue;
            for (int i = numberOfRepetitions; i < array.Length; i++)
                array[i] = rand.Next(0, moduls);
            if (poryadok == 1)
                Array.Sort(array);
            else if (poryadok == 0) 
                Array.Reverse(array);

            return array;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            if (comboBox1.SelectedIndex == 0) //пузырьком, вставками, выбором, шейкерная, гномья
            {
                PointPairList bub = new PointPairList();
                PointPairList ins = new PointPairList();
                PointPairList sel = new PointPairList();
                PointPairList shake = new PointPairList();
                PointPairList gnom = new PointPairList();
                pane.CurveList.Clear();
                Stopwatch stopwatch = new Stopwatch();
                long[] s = new long[4];
                long msec;
                var sortr = new SortingAlgorithms();
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckBubbleTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckBubbleTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckBubbleTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckBubbleTime(stopwatch, array4);
                    s[3] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    bub.Add(i + 1, s[i]);
                }
                LineItem myCurve = pane.AddCurve("Bubble", bub, Color.Red, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckInsertionTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckInsertionTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckInsertionTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckInsertionTime(stopwatch, array4);
                    s[3] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    ins.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Insertion", ins, Color.Blue, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckSelectionTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckSelectionTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckSelectionTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckSelectionTime(stopwatch, array4);
                    s[3] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    sel.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Selection", sel, Color.Green, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckShakerTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckShakerTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckShakerTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckShakerTime(stopwatch, array4);
                    s[3] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    shake.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Shaker", shake, Color.Black, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckGnomTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckGnomTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckGnomTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckGnomTime(stopwatch, array4);
                    s[3] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    gnom.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Gnome", gnom, Color.Pink, SymbolType.None);

                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();

            }
            if (comboBox1.SelectedIndex == 1) // битонная, Шелла, деревом;
            {
                PointPairList bit = new PointPairList();
                PointPairList shell = new PointPairList();
                PointPairList tree = new PointPairList();
                pane.CurveList.Clear();
                Stopwatch stopwatch = new Stopwatch();
                long[] s = new long[5];
                long msec;
                var sortr = new SortingAlgorithms();
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckBitonicTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckBitonicTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckBitonicTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckBitonicTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckBitonicTime(stopwatch, array5);
                    s[4] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    bit.Add(i + 1, s[i]);
                }
                LineItem myCurve = pane.AddCurve("Bitonic", bit, Color.Green, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckShellTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckShellTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckShellTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckShellTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckShellTime(stopwatch, array5);
                    s[4] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    shell.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Shell", shell, Color.Red, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckTreeTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckTreeTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckTreeTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckTreeTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckTreeTime(stopwatch, array5);
                    s[4] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    tree.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Tree", tree, Color.Brown, SymbolType.None);

                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }

            if (comboBox1.SelectedIndex == 2) // расчёской, пирамидальная, быстрая, слиянием, подсчётом, поразрядная
            {
                PointPairList comb = new PointPairList();
                PointPairList heap = new PointPairList();
                PointPairList quick = new PointPairList();
                PointPairList merge = new PointPairList();
                PointPairList count = new PointPairList();
                PointPairList radix = new PointPairList();
                pane.CurveList.Clear();
                Stopwatch stopwatch = new Stopwatch();
                long[] s = new long[6];
                long msec;
                var sortr = new SortingAlgorithms();
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckCombTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckCombTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckCombTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckCombTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckCombTime(stopwatch, array5);
                    s[4] += msec;
                    msec = CheckCombTime(stopwatch, array6);
                    s[5] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    comb.Add(i + 1, s[i]);
                }
                LineItem myCurve = pane.AddCurve("Comb", comb, Color.Green, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckHeapTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckHeapTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckHeapTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckHeapTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckHeapTime(stopwatch, array5);
                    s[4] += msec;
                    msec = CheckHeapTime(stopwatch, array6);
                    s[5] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    heap.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Heap", heap, Color.Red, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckQuickTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckQuickTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckQuickTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckQuickTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckQuickTime(stopwatch, array5);
                    s[4] += msec;
                }
                //ОДИН массив 10^6 на методе быстрой сортировки в 3ей группе тестовых данных делается (158000-200000мс)2.63-3.33 минуты, а в 4ой группе - (около 111000мс)1.85минут поэтому он не повторяется 20 раз
                msec = CheckQuickTime(stopwatch, array6);
                s[5] += msec;
                for (int i = 0; i < s.Length; i++)
                {
                    if (i < s.Length - 1)
                    {
                        s[i] /= 20;
                    }
                    quick.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Quick", quick, Color.Purple, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckMergeTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckMergeTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckMergeTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckMergeTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckMergeTime(stopwatch, array5);
                    s[4] += msec;
                    msec = CheckMergeTime(stopwatch, array6);
                    s[5] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    merge.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Merge", merge, Color.Black, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckCountingTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckCountingTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckCountingTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckCountingTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckCountingTime(stopwatch, array5);
                    s[4] += msec;
                    msec = CheckCountingTime(stopwatch, array6);
                    s[5] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    count.Add(i + 1, s[i]);
                }
                myCurve = pane.AddCurve("Counting", count, Color.Pink, SymbolType.None);

                for (int i = 0; i < s.Length; i++)
                    s[i] = 0;
                for (int i = 1; i <= 20; i++)
                {
                    msec = CheckRadixTime(stopwatch, array1);
                    s[0] += msec;
                    msec = CheckRadixTime(stopwatch, array2);
                    s[1] += msec;
                    msec = CheckRadixTime(stopwatch, array3);
                    s[2] += msec;
                    msec = CheckRadixTime(stopwatch, array4);
                    s[3] += msec;
                    msec = CheckRadixTime(stopwatch, array5);
                    s[4] += msec;
                    msec = CheckRadixTime(stopwatch, array6);
                    s[5] += msec;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] /= 20;
                    radix.Add(i+1, s[i]);
                }
                myCurve = pane.AddCurve("Radix", radix, Color.Blue, SymbolType.None);

                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }

        private long CheckGnomTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.GnomeSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckShakerTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.ShakerSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckSelectionTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.SelectionSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckInsertionTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.InsertionSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckBubbleTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.BubbleSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckBitonicTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.BitonicSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckShellTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.ShellSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckTreeTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.TreeSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        private long CheckCountingTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.CountingSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckRadixTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.RadixSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckHeapTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.HeapSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        private long CheckCombTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.CombSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long CheckQuickTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.QuickSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        private long CheckMergeTime(Stopwatch stopwatch, int[] array)
        {
            var sortr = new SortingAlgorithms();
            stopwatch.Reset();
            stopwatch.Start();
            int[] sortmas1 = sortr.MergeSort((int[])array.Clone());
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        private async void button3_Click(object sender, EventArgs e)
        {
            StreamWriter f = new StreamWriter("Save.txt", false);
            await f.WriteLineAsync("Массив длинной 10:");
            await f.WriteLineAsync();
            for (int i = 0; i < array1.Length; i++)
            {
                await f.WriteAsync($"{array1[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync();
            int[] clon = (int[])array1.Clone();
            Array.Sort(clon);
            for (int i = 0; i < clon.Length; i++)
            {
                await f.WriteAsync($"{clon[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync("Массив длинной 100:");
            await f.WriteLineAsync();
            for (int i = 0; i < array2.Length; i++)
            {
                await f.WriteAsync($"{array2[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync();
            int[] clon1 = (int[])array2.Clone();
            Array.Sort(clon1);
            for (int i = 0; i < clon1.Length; i++)
            {
                await f.WriteAsync($"{clon1[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync("Массив длинной 1000:");
            await f.WriteLineAsync();
            for (int i = 0; i < array3.Length; i++)
            {
                await f.WriteAsync($"{array3[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync();
            int[] clon2 = (int[])array3.Clone();
            Array.Sort(clon2);
            for (int i = 0; i < clon2.Length; i++)
            {
                await f.WriteAsync($"{clon2[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync("Массив длинной 10000:");
            await f.WriteLineAsync();
            for (int i = 0; i < array4.Length; i++)
            {
                await f.WriteAsync($"{array4[i]} ");
            }
            await f.WriteLineAsync();
            await f.WriteLineAsync();
            int[] clon3 = (int[])array4.Clone();
            Array.Sort(clon3);
            for (int i = 0; i < clon3.Length; i++)
            {
                await f.WriteAsync($"{clon3[i]} ");
            }
            
            if (!Empt(array5))
            {
                await f.WriteLineAsync();
                await f.WriteLineAsync("Массив длинной 100000:");
                await f.WriteLineAsync();
                for (int i = 0; i < array5.Length; i++)
                {
                    await f.WriteAsync($"{array5[i]} ");
                }

                await f.WriteLineAsync();
                await f.WriteLineAsync();
                int[] clon4 = (int[])array5.Clone();
                Array.Sort(clon4);
                for (int i = 0; i < clon4.Length; i++)
                {
                    await f.WriteAsync($"{clon4[i]} ");
                }
                
            }
            if (!Empt(array6))
            {
                await f.WriteLineAsync();
                await f.WriteLineAsync("Массив длинной 1000000:");
                await f.WriteLineAsync();
                for (int i = 0; i < array6.Length; i++)
                {
                    await f.WriteAsync($"{array6[i]} ");
                }
                await f.WriteLineAsync();
                await f.WriteLineAsync();
                int[] clon5 = (int[])array6.Clone();
                Array.Sort(clon5);
                for (int i = 0; i < clon5.Length; i++)
                {
                    await f.WriteAsync($"{clon5[i]} ");
                }
                await f.WriteLineAsync();
                await f.WriteLineAsync();
            }
            f.Close();
        }

        private bool Empt(int[] mas)
        {
            for(int i=0; i<mas.Length; i++)
            {
                if (mas[i] != 0)
                    return false;
            }
            return true;
        }
    }
    public class SortingAlgorithms
    {
        // 1. Bubble Sort
        public int[] BubbleSort(int[] arr)
        {
            int temp;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            return arr;
        }

        // 2. Shaker Sort
        public int[] ShakerSort(int[] arr)
        {
            int temp;
            for (int i = 0; i < arr.Length / 2; i++)
            {
                bool swapped = false;
                for (int j = i; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                for (int j = arr.Length - i - 2; j > i; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
            return arr;
        }

        // 3. Comb Sort
        public int[] CombSort(int[] arr)
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
                    if (arr[i] > arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                        swapped = true;
                    }
                }
            }
            return arr;
        }

        // 4. Insertion Sort
        public int[] InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
            return arr;
        }

        // 5. Shell Sort
        public int[] ShellSort(int[] arr)
        {
            int gap = arr.Length / 2;
            while (gap > 0)
            {
                for (int i = gap; i < arr.Length; i++)
                {
                    int temp = arr[i];
                    int j = i;
                    while (j >= gap && arr[j - gap] > temp)
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
        public int[] GnomeSort(int[] arr)
        {
            int i = 1;
            while (i < arr.Length)
            {
                if (i == 0 || arr[i] >= arr[i - 1])
                {
                    i++;
                }
                else
                {
                    int temp = arr[i];
                    arr[i] = arr[i - 1];
                    arr[i - 1] = temp;
                    i--;
                }
            }
            return arr;
        }

        // 7. Selection Sort
        public int[] SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
            return arr;
        }

        // 8. Quick Sort
        private int Partition(int[] array, int minIndex, int maxIndex)
        {
            int t;
            int pivotIndex = MedianOfThree(array, minIndex, maxIndex); // Выбираем медиану как опорный элемент
            Zam(array, pivotIndex, maxIndex); // Перемещаем опорный элемент в конец

            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    t = array[pivot];
                    array[pivot] = array[i];
                    array[i] = t;
                }
            }

            pivot++;
            t = array[pivot];
            array[pivot] = array[maxIndex];
            array[maxIndex] = t;
            return pivot;
        }

        private int[] QuickSort1(int[] array, int minIndex, int maxIndex)
        {
            while (minIndex < maxIndex)
            {
                // Если подмассив маленький, используем сортировку вставками
                if (maxIndex - minIndex < 10)
                {
                    InsertionSort(array, minIndex, maxIndex);
                    break;
                }

                // Определяем индекс опорного элемента
                var pivotIndex = Partition(array, minIndex, maxIndex);

                // Выбираем меньший подмассив для рекурсии, а другой обрабатываем в цикле
                if (pivotIndex - minIndex < maxIndex - pivotIndex)
                {
                    QuickSort1(array, minIndex, pivotIndex - 1);
                    minIndex = pivotIndex + 1; // продолжаем сортировать правую часть
                }
                else
                {
                    QuickSort1(array, pivotIndex + 1, maxIndex);
                    maxIndex = pivotIndex - 1; // продолжаем сортировать левую часть
                }
            }

            return array;
        }

        public int[] QuickSort(int[] array)
        {
            return QuickSort1(array, 0, array.Length - 1);
        }

        // Медиана трёх: находим среднее значение из первого, последнего и среднего элементов
        private int MedianOfThree(int[] array, int minIndex, int maxIndex)
        {
            int midIndex = (minIndex + maxIndex) / 2;

            if (array[minIndex] > array[midIndex])
                Zam(array, minIndex, midIndex);
            if (array[minIndex] > array[maxIndex])
                Zam(array, minIndex, maxIndex);
            if (array[midIndex] > array[maxIndex])
                Zam(array, midIndex, maxIndex);

            return midIndex;
        }

        // Сортировка вставками для небольших подмассивов
        private void InsertionSort(int[] array, int minIndex, int maxIndex)
        {
            for (int i = minIndex + 1; i <= maxIndex; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= minIndex && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        // Метод для обмена элементов
        private void Zam(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        // 9. Merge Sort
        public int[] MergeSort(int[] arr)
        {
            return MergeSort(arr, 0, arr.Length - 1);
        }

        private int[] MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
            return arr;
        }

        private void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;
            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];
            Array.Copy(arr, left, leftArr, 0, n1);
            Array.Copy(arr, mid + 1, rightArr, 0, n2);
            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    arr[k++] = leftArr[i++];
                }
                else
                {
                    arr[k++] = rightArr[j++];
                }
            }
            while (i < n1) arr[k++] = leftArr[i++];
            while (j < n2) arr[k++] = rightArr[j++];
        }


        // 10. Counting Sort
        public int[] CountingSort(int[] arr)
        {
            int min = (int)arr[0], max = (int)arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min) min = (int)arr[i];
                if (arr[i] > max) max = (int)arr[i];
            }
            int[] count = new int[max - min + 1];
            for (int i = 0; i < arr.Length; i++) count[(int)arr[i] - min]++;
            int k = 0;
            for (int i = 0; i < count.Length; i++)
            {
                while (count[i]-- > 0)
                {
                    arr[k++] = i + min;
                }
            }
            return arr;
        }

        // 11. Radix Sort
        public int[] RadixSort(int[] arr)
        {
            int max = (int)arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max) max = (int)arr[i];
            }
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSortByDigit(arr, exp);
            }
            return arr;
        }

        private void CountingSortByDigit(int[] arr, int exp)
        {
            int[] output = new int[arr.Length];
            int[] count = new int[10];
            for (int i = 0; i < arr.Length; i++) count[((int)arr[i] / exp) % 10]++;
            for (int i = 1; i < 10; i++) count[i] += count[i - 1];
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                output[count[((int)arr[i] / exp) % 10] - 1] = arr[i];
                count[((int)arr[i] / exp) % 10]--;
            }
            Array.Copy(output, arr, arr.Length);
        }

        // 12. Heap Sort
        public int[] HeapSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                Heapify(arr, i, 0);
            }
            return arr;
        }

        private void Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && arr[left] > arr[largest]) largest = left;
            if (right < n && arr[right] > arr[largest]) largest = right;
            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                Heapify(arr, n, largest);
            }
        }
        // 13. Tree Sort
        public int[] TreeSort(int[] arr)
        {
            TreeNode root = null;
            // Строим сбалансированное дерево поиска
            foreach (var value in arr)
            {
                root = InsertNode(root, value);
            }
            // Собираем элементы дерева в отсортированный массив
            int index = 0;
            InOrderTraversal(root, arr, ref index);

            return arr;
        }

        private class TreeNode
        {
            public int Value;
            public TreeNode Left, Right;
            public int Height;
            public TreeNode(int value)
            {
                Value = value;
                Height = 1; // Начальная высота узла - 1
                Left = Right = null;
            }
        }

        // получение высоты узла
        private int GetHeight(TreeNode node)
        {
            return node?.Height ?? 0;
        }

        // получение баланса узла
        private int GetBalance(TreeNode node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }
        private TreeNode RotateRight(TreeNode y)
        {
            TreeNode x = y.Left;
            TreeNode T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        // левый поворот
        private TreeNode RotateLeft(TreeNode x)
        {
            TreeNode y = x.Right;
            TreeNode T2 = y.Left;

            // Выполняем поворот
            y.Left = x;
            x.Right = T2;

            // Обновляем высоты
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        // вставка узла с балансировкой
        private TreeNode InsertNode(TreeNode node, int value)
        {
            // выполняем обычную вставку узла
            if (node == null)
                return new TreeNode(value);

            if (value < node.Value)
                node.Left = InsertNode(node.Left, value);
            else if (value > node.Value)
                node.Right = InsertNode(node.Right, value);
            else
                return node; // Дубликаты не вставляем

            // обновляем высоту предка узла
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            // получаем баланс предка узла и выполняем повороты
            int balance = GetBalance(node);
            if (balance > 1 && value < node.Left.Value)
                return RotateRight(node);
            if (balance < -1 && value > node.Right.Value)
                return RotateLeft(node);
            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // обход дерева для сборки элементов в отсортированном порядке
        private void InOrderTraversal(TreeNode root, int[] arr, ref int index)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left, arr, ref index);
                arr[index++] = root.Value;
                InOrderTraversal(root.Right, arr, ref index);
            }
        }


        // 14. Bitonic Sort
        public int[] BitonicSort(int[] arr)
        {
            return BitonicSort(arr, 0, arr.Length, true);
        }

        private int[] BitonicSort(int[] arr, int low, int length, bool ascending)
        {
            if (length > 1)
            {
                int mid = length / 2;
                BitonicSort(arr, low, mid, true);       // Сортируем первую половину по возрастанию
                BitonicSort(arr, low + mid, mid, false); // Сортируем вторую половину по убыванию
                BitonicMerge(arr, low, length, ascending); // Объединяем результат
            }
            return arr;
        }

        private void BitonicMerge(int[] arr, int low, int length, bool ascending)
        {
            if (length > 1)
            {
                int mid = length / 2;
                for (int i = low; i < low + mid; i++)
                {
                    if ((ascending && arr[i] > arr[i + mid]) || (!ascending && arr[i] < arr[i + mid]))
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + mid];
                        arr[i + mid] = temp;
                    }
                }
                BitonicMerge(arr, low, mid, ascending);
                BitonicMerge(arr, low + mid, mid, ascending);
            }
        }

    }
}

