using MyStack;
using System;
using System.IO;
class PoalndNotation
{
    public int Priority(string s)
    {
        switch (s)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
            case "//":
                return 2;
            case "^":
                return 3;
            case "sqrt":
            case "abs":
            case "sin":
            case "cos":
            case "tg":
            case "ln":
            case "log":
            case "min":
            case "max":
            case "mod":
            case "exp":
                return 4;
            case "%":
                return 5;
            default:
                return 0;
        }
    }
    private string GetLabel(ref int pos, string s)
    {
        string st = "";
        for (; pos < s.Length; pos++)
        {
            if (Char.IsLetter(s[pos]))
            {
                st += s[pos];
            }
            else break;
        }
        pos--;
        return st;
    }
    private string GetNumb(ref int pos, string s)
    {
        string st = "";
        for (; pos < s.Length; pos++)
        {
            if (Char.IsNumber(s[pos]) || s[pos] == '.')
            {
                st += s[pos];
            }
            else
            {
                break;
            }
        }
        pos--;
        return st;
    }

    public string Create_Poland(string s)
    {
        string after = "";
        MyStack<string> oper = new MyStack<string>();

        for (int i = 0; i < s.Length; i++)
        {
            if (Char.IsDigit(s[i]) || s[i] == 'x' || s[i] == 'y' || s[i] == 'z' ||
                (s[i] == 'a' && i + 2 < s.Length && s[i + 1] != 'b' && s[i + 2] != 's') ||
                s[i] == 'b' || s[i] == 'c' || s[i] == 'd')
            {
                string numOrVar = GetNumb(ref i, s); 
                after += numOrVar + " "; 
            }
            else if (s[i] == '-' && (i == 0 || s[i - 1] == '(' || !Char.IsDigit(s[i - 1])))// Обрабатываем унарный минус
            {
                string num = "-" + GetNumb(ref i, s);
                after += num + " ";
            }
            else if (s[i] == '(')
            {
                oper.Push("("); 
            }
            else if (s[i] == ')')
            {
                while (!oper.Empty() && oper.Peek() != "(")
                {
                    after += oper.Peek() + " ";
                    oper.Pop();
                }
                if (!oper.Empty()) oper.Pop();
            }
            else if (Char.IsLetter(s[i]))
            {
                string path = GetLabel(ref i, s);
                if (Priority(path) != 0)
                {
                    while (!oper.Empty() && Priority(oper.Peek()) >= Priority(path))
                    {
                        after += oper.Peek() + " ";
                        oper.Pop();
                    }
                    oper.Push(path);
                }
            }
            else if (s[i] == '+' || s[i] == '-' || s[i] == '%' || s[i] == '^' || s[i] == '*' || s[i] == '/' || (s[i] == '/' && i + 1 < s.Length && s[i + 1] == '/'))
            {
                string path = s[i].ToString();
                if (s[i] == '/' && s[i + 1] == '/')
                {
                    path += s[i + 1];
                    i++;
                }
                if (oper.Empty())
                {
                    oper.Push(path);
                }
                else
                {
                    while (!oper.Empty() && Priority(oper.Peek()) >= Priority(path))
                    {
                        after += oper.Peek() + " ";
                        oper.Pop();
                    }
                    oper.Push(path);
                }
            }
        }
        while (!oper.Empty())
        {
            after += oper.Peek() + " ";
            oper.Pop();
        }
        return after.Trim();
    }
    private double DescriptionOperation(string path, params double[] array)
    {
        switch (path)
        {
            case "+":
                return array[0] + array[1];
            case "-":
                return array[0] - array[1];
            case "*": return array[0] * array[1];
            case "/": return array[0] / array[1];
            case "^": return Math.Pow(array[0], array[1]);
            case "sqrt": return Math.Sqrt(array[0]);
            case "abs": return Math.Abs(array[0]);
            case "//": return Math.Floor(array[0] / array[1]);
            case "exp": return Math.Exp(array[0]);
            case "sin": return Math.Sin(array[0]);
            case "cos": return Math.Cos(array[0]);
            case "tg": return Math.Tan(array[0]);
            case "ln": return Math.Log(array[0]);
            case "log": return Math.Log10(array[0]);
            case "min": return array[0] < array[1] ? array[0] : array[1];
            case "max": return array[0] > array[1] ? array[0] : array[1];
            case "mod":
                return (int)array[0] % (int)array[1];
            default: return 0;
        }
    }
    public double Calculate(string s)
    {
        MyStack<double> node = new MyStack<double>(); 
        string[] tokens = s.Split(' '); 

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                node.Push(number);
            }
            else
            {
                if (token == "sqrt" || token == "abs" || token == "sin" || token == "cos" || token == "tg" || token == "ln" || token == "log" || token == "exp")
                {
                    double a = node.Peek();
                    node.Pop();
                    double result = DescriptionOperation(token, a);
                    node.Push(result);
                }
                else
                {
                    double b = node.Peek();
                    node.Pop();
                    double a = node.Peek();
                    node.Pop();
                    double result = DescriptionOperation(token, a, b);
                    node.Push(result);
                }
            }
        }
        // Результат вычисления находится на вершине стека
        return node.Peek();
    }
}
public class Program
{
    static void Main(string[] args)
    {
        string s = Console.ReadLine();
        PoalndNotation rpn = new PoalndNotation();
        string polNat = rpn.Create_Poland(s);
        Console.WriteLine($"Обратная Польская нотация: {polNat}");
        double result = rpn.Calculate(polNat);
        Console.WriteLine($"Результат: {result}");
    }
}

