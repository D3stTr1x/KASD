using MyDeque;
public class Sorts
{
    public int CountNumber(string line)
    {
        int count = 0;
        foreach (char c in line)
        {
            if (Char.IsDigit(c))
                count++;
        }
        return count;
    }

    public int CountSpaces(string line)
    {
        int count = 0;
        foreach (char c in line)
        {
            if (c == ' ')
                count++;
        }
        return count;
    }

    public void AddLine(int n)
    {
        MyArrayDeque<string> deque = new MyArrayDeque<string>();

        using (StreamReader input = new StreamReader("input.txt"))
        using (StreamWriter output = new StreamWriter("sorted.txt"))
        {
            string? part = input.ReadLine();
            if (part != null)
            {
                deque.Add(part);
                part = input.ReadLine();
            }
            while (part != null)
            {
                if (CountNumber(part) > CountNumber(deque.GetFirst()))
                    deque.AddLast(part);
                else
                    deque.AddFirst(part);

                part = input.ReadLine();
            }
            for (int i = 0; i < deque.Size(); i++)
                output.WriteLine(deque.GetElement(i));
        }
        for (int i = 0; i < deque.Size();i++)
        {
            string path = deque.GetElement(i);
            if (CountSpaces(path) > n)
            {
                deque.Remove(path);
                i -= 2;
            }
        }
        deque.PrintDeque();
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Sorts sorting = new Sorts();

        Console.Write("Введите значение n: ");
        int n = Convert.ToInt32(Console.ReadLine());
        sorting.AddLine(n);
    }
}
