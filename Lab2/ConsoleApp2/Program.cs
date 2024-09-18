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
            int ind = 1;
            Complex on;
            on.im = 1;
            on.re = 1;
            Complex tw;
            tw.im = 1;
            tw.re = 1;
            string s = "1";
            while (s != "Q" || s != "q")
            {

                on.im = Complex.Create(on, 1);
                if (on.im == 99999.121)
                {
                    Thread.Sleep(1000);
                    break;
                }
                on.re = Complex.Create1(on, 1);
                if (on.re == 99999.121)
                {
                    Thread.Sleep(1000);
                    break;
                }
                Complex.Print(on, 1);

                Console.WriteLine($"Введите операцию (+,-,*,/,||, Arg)");
                s = Console.ReadLine();
                if (s == "Q" || s == "q")
                    break;
                switch (s)
                {
                    case "+":
                        tw.im = Complex.Create(tw, 2);
                        if (tw.im == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        tw.re = Complex.Create1(tw, 2);
                        if (tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Suma(on, tw);
                        break;

                    case "-":
                        tw.im = Complex.Create(tw, 2);
                        if (tw.im == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        tw.re = Complex.Create1(tw, 2);
                        if (tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Razn(on, tw);
                        break;

                    case "*":
                        tw.im = Complex.Create(tw, 2);
                        if (tw.im == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        tw.re = Complex.Create1(tw, 2);
                        if (tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Umnozh(on, tw);
                        break;

                    case "/":
                        tw.im = Complex.Create(tw, 2);
                        if (tw.im == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        tw.re = Complex.Create1(tw, 2);
                        if (tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Delen(on, tw);
                        break;

                    case "||":
                        Complex.Modul(on);
                        break;

                    case "Arg":
                        Complex.Argum(on);
                        break;

                    case "arg":
                        Complex.Argum(on);
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда, попробуйте снова");
                        break;

                }
                if (tw.re == 99999.121)
                {
                    Thread.Sleep(1000);
                    break;
                }
            }
            Console.WriteLine("End");
            Thread.Sleep(1000);
        }
    }
    public struct Complex
    {
        public double im;
        public double re;

        public static bool Check(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            for (int i = 0; i < str.Length; i++)
            {
                if (!(str[0] == '-') && (!(char.IsNumber(str[0]))))
                {
                    if (!(str[i] == 'Q' || str[i] == 'q') && (!(char.IsNumber(str[i]))))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static double Create(Complex n, int ind)
        {
            Console.WriteLine($"Введите мнимую часть комплексного {ind}-го числа:");
            string str = Console.ReadLine();
            if (Check(str))
            {
                if (str == "Q" || str == "q")
                {
                    return 99999.121;
                }
                else n.im = Double.Parse(str);
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
                return 99999.121;
            }
            return n.im;
        }
        public static double Create1(Complex n, int ind)
        {
            Console.WriteLine($"Введите вещественную часть комплексного {ind}-го числа:");
            string str = Console.ReadLine();
            if (Check(str))
            {
                if (str == "Q" || str == "q")
                {
                    return 99999.121;
                }
                else n.re = Double.Parse(str);
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
                return 99999.121;
            }
            return n.re;
        }
        public static void Print(Complex n, int ind)
        {
            Console.WriteLine($"(({n.im}) + ({n.re})i - {ind}-е комплексное число");
        }
        public static void Suma(Complex n, Complex r)
        {
            Complex rx;
            rx.im = n.im + r.im;
            rx.re = n.re + r.re;
            Console.WriteLine($"({n.im}) + ({n.re})i + ({r.im}) + ({r.re})i = ({rx.im})+({rx.re})i");
            return;
        }
        public static void Razn(Complex n, Complex r)
        {
            Complex rx;
            rx.im = n.im - r.im;
            rx.re = n.re - r.re;
            Console.WriteLine($"({n.im}) + ({n.re})i - ({r.im}) + ({r.re})i = ({rx.im})+({rx.re})i");
            return;
        }
        public static void Umnozh(Complex n, Complex r)
        {
            Complex rx;
            rx.im = (n.im * r.im - n.re * r.re);
            rx.re = (n.im * r.re + n.re * r.im);
            Console.WriteLine($"({n.im}) + ({n.re})i * ({r.im}) + ({r.re})i = ({rx.im})+({rx.re})i");
            return;
        }
        public static void Delen(Complex n, Complex r)
        {
            Complex rx;
            rx.im = ((n.im * r.im + n.re * r.re) / ((Math.Pow(r.im, 2)) + (Math.Pow(r.re, 2))));
            rx.re = ((n.re * r.im - n.im * r.re) / ((Math.Pow(r.im, 2)) + (Math.Pow(r.re, 2))));
            Console.WriteLine($"({n.im}) + ({n.re})i / ({r.im}) + ({r.re})i = ({rx.im})+({rx.re})i");
            return;
        }

        public static void Modul(Complex n)
        {
            double res = 0;
            res = (Math.Sqrt((Math.Pow(n.im, 2)) + Math.Pow(n.re, 2)));
            Console.WriteLine(res);
            return;
        }

        public static void Argum(Complex n)
        {
            double ar = 0;
            if (n.re > 0)
            {
                ar = Math.Atan(n.im / n.re);
            }
            if (n.re < 0 && n.im >= 0)
            {
                ar = Math.PI + Math.Atan(n.im / n.re);
            }
            if (n.re < 0 && n.im < 0)
            {
                ar = -Math.PI + Math.Atan(n.im / n.re);
            }
            if (n.re == 0 && n.im > 0)
            {
                ar = Math.PI / 2;
            }
            if (n.re == 0 && n.im < 0)
            {
                ar = -Math.PI / 2;
            }
            Console.WriteLine(ar);
            return;
        }
    }
}