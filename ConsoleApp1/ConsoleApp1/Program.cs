using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader sr = new StreamReader("Vector.txt");
                uint dim = uint.Parse(sr.ReadLine());
                double[][] matr = new double[dim][];
                for (int i = 0; i < dim; i++)
                {
                    matr[i] = sr.ReadLine().Split(' ').Select(x => double.Parse(x)).ToArray();
                }

                if (Symetry(dim, matr))
                {
                    double[][] vector = new double[1][];
                    vector[0] = sr.ReadLine().Split(' ').Select(x => double.Parse(x)).ToArray();
                    LenVector(dim, vector, matr);
                }
                else
                {
                    Console.WriteLine("Несимметричная матрица");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}\n");
            }
            Thread.Sleep(2000);
        }
        public static void LenVector(uint dim, double[][] vector, double[][] matr)
        {
            double[] l = new double[dim];
            double len = 0;
            for (int i = 0; i < dim; i++)
            {
                l[i] = vector[0][i] * matr[i][i];
            }
            double[][] trns = new double[dim][];
            for (int i = 0; i < dim; i++)
                trns[i] = new double[1];
            for (int i = 0; i < dim; i++)
            {
                trns[i][0] = vector[0][i];
            }
            for (int i = 0; i < dim; i++)
            {
                len += l[i] * trns[i][0];
            }
            len = Math.Sqrt(len);
            Console.WriteLine($"Длинна вектора: {len}");
        }
        public static bool Symetry(uint dim, double[][] matr)
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if ((i != j) && (matr[i][j] !=  0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}

