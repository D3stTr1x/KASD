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
            Complex on;
            Complex tw;
            tw.im = 1;
            tw.re = 1;
            string s = "1";
            Complex rx;
            while (s != "Q" || s != "q")
            {

                on = Complex.Create(1);
                if (on.im == 99999.121 && on.re == 99999.121)
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
                        tw = Complex.Create(2);
                        if (tw.im == 99999.121 && tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        rx = Complex.Suma(on, tw);
                        Console.WriteLine($"({on.im}) + ({on.re})i + ({tw.im}) + ({tw.re})i = ({rx.im})+({rx.re})i");
                        break;

                    case "-":
                        tw = Complex.Create(2);
                        if (tw.im == 99999.121 && tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Razn(on, tw);
                        rx = Complex.Razn(on, tw);
                        Console.WriteLine($"({on.im}) + ({on.re})i - ({tw.im}) + ({tw.re})i = ({rx.im})+({rx.re})i");
                        break;

                    case "*":
                        tw = Complex.Create(2);
                        if (tw.im == 99999.121 && tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Umnozh(on, tw);
                        rx = Complex.Umnozh(on, tw);
                        Console.WriteLine($"({on.im}) + ({on.re})i * ({tw.im}) + ({tw.re})i = ({rx.im})+({rx.re})i");
                        break;

                    case "/":
                        tw = Complex.Create(2);
                        if (tw.im == 99999.121 && tw.re == 99999.121)
                        {
                            Thread.Sleep(1000);
                            break;
                        }
                        Complex.Print(tw, 2);
                        Complex.Delen(on, tw);
                        rx = Complex.Delen(on, tw);
                        Console.WriteLine($"({on.im}) + ({on.re})i / ({tw.im}) + ({tw.re})i = ({rx.im})+({rx.re})i");
                        break;

                    case "||":
                        double modl = Complex.Modul(on);
                        Console.WriteLine(modl);
                        break;

                    case "Arg":
                        double argm = Complex.Argum(on);
                        Console.WriteLine(argm);
                        break;

                    case "arg":
                        double argm1 = Complex.Argum(on);
                        Console.WriteLine(argm1);
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда, попробуйте снова");
                        break;

                }
                if (tw.im == 99999.121 && tw.re == 99999.121)
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
        public static Complex Create(int ind)
        {
            Complex n;
            Complex err;
            err.im = 99999.121;
            err.re = 99999.121;
            Console.WriteLine($"Введите мнимую часть комплексного {ind}-го числа:");
            string str = Console.ReadLine();
            if (Check(str))
            {
                if (str == "Q" || str == "q")
                {
                    return err;
                }
                else n.im = Double.Parse(str);
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
                return err;
            }

            Console.WriteLine($"Введите вещественную часть комплексного {ind}-го числа:");
            str = Console.ReadLine();
            if (Check(str))
            {
                if (str == "Q" || str == "q")
                {
                    return err;
                }
                else n.re = Double.Parse(str);
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
                return err;
            }
            return n;
        }
        public static void Print(Complex n, int ind)
        {
            Console.WriteLine($"(({n.im}) + ({n.re})i - {ind}-е комплексное число");
        }
        public static Complex Suma(Complex n, Complex r)
        {
            Complex rx;
            rx.im = n.im + r.im;
            rx.re = n.re + r.re;
            return rx;
        }
        public static Complex Razn(Complex n, Complex r)
        {
            Complex rx;
            rx.im = n.im - r.im;
            rx.re = n.re - r.re;
            return rx;
        }
        public static Complex Umnozh(Complex n, Complex r)
        {
            Complex rx;
            rx.im = (n.im * r.im - n.re * r.re);
            rx.re = (n.im * r.re + n.re * r.im);
            return rx;
        }
        public static Complex Delen(Complex n, Complex r)
        {
            Complex rx;
            rx.im = ((n.im * r.im + n.re * r.re) / ((Math.Pow(r.im, 2)) + (Math.Pow(r.re, 2))));
            rx.re = ((n.re * r.im - n.im * r.re) / ((Math.Pow(r.im, 2)) + (Math.Pow(r.re, 2))));
            return rx;
        }

        public static double Modul(Complex n)
        {
            double res = 0;
            res = (Math.Sqrt((Math.Pow(n.im, 2)) + Math.Pow(n.re, 2)));
            return res;
        }

        public static double Argum(Complex n)
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
            return ar;
        }
    }
}