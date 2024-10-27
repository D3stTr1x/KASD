using MyQueue;
using System.Diagnostics;

public class Bid : IComparable<Bid>
{
    private int priority;
    public int Priority
    {
        get { return priority; }
        set { priority = value; }
    }
    private int number { get; set; }
    public int Number
    {
        get { return number; }
        set { number = value; }
    }
    private int numberStep { get; set; }
    public int StepAdded
    {
        get { return numberStep; }
        set { numberStep = value; }
    }

    public long ArrivalTime { get; set; } // Время добавления заявки в систему

    public Bid(int priority, int number, int numberStep)
    {
        this.priority = priority;
        this.number = number;
        this.numberStep = numberStep;
        this.ArrivalTime = 0; // Инициализация времени прибытия
    }
    public int CompareTo(Bid other)
    {
        return priority.CompareTo(other.priority);
    }
}
public class Program
{
    static void Main(string[] args)
    {
        string path = "log.txt";
        MyPriorityQueue<Bid> order = new MyPriorityQueue<Bid>();
        Console.WriteLine("Введите N:");
        int n = Convert.ToInt32(Console.ReadLine());
        int bidCounter = 1;
        Bid longestWaitBid = null;
        long maxWaitTime = 0;
        Random random = new Random();

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();
            for (int step = 1; step <= n; step++)
            {
                int numBids = random.Next(1, 11);
                for (int j = 0; j < numBids; j++)
                {
                    int priority = random.Next(1, 6);
                    Bid bid = new Bid(priority, bidCounter++, step);
                    bid.ArrivalTime = mainTimer.ElapsedMilliseconds;
                    order.Add(bid);
                    streamWriter.WriteLine($"ADD {bid.Number} {bid.Priority} {bid.StepAdded}");
                }

                // Удаление заявки с наивысшим приоритетом
                if (!order.IsEmpty())
                {
                    Bid removedBid = order.Poll();
                    long waitTime = mainTimer.ElapsedMilliseconds - removedBid.ArrivalTime;
                    if (waitTime > maxWaitTime)
                    {
                        maxWaitTime = waitTime;
                        longestWaitBid = removedBid;
                    }
                    streamWriter.WriteLine($"REMOVE {removedBid.Number} {removedBid.Priority} {removedBid.StepAdded}");
                }
            }

            // Удаление оставшихся заявок после завершения всех шагов
            while (!order.IsEmpty())
            {
                Bid removedBid = order.Poll();
                long waitTime = mainTimer.ElapsedMilliseconds - removedBid.ArrivalTime;
                if (waitTime > maxWaitTime)
                {
                    maxWaitTime = waitTime;
                    longestWaitBid = removedBid;
                }
                streamWriter.WriteLine($"REMOVE {removedBid.Number} {removedBid.Priority} {removedBid.StepAdded}");
            }

            mainTimer.Stop();
        }
        if (longestWaitBid != null)
        {
            Console.WriteLine($"Заявка с максимальным временем ожидания:\nНомер: {longestWaitBid.Number}");
            Console.WriteLine($"Приоритет: {longestWaitBid.Priority}");
            Console.WriteLine($"Шаг добавления: {longestWaitBid.StepAdded}");
            Console.WriteLine($"Время ожидания: {maxWaitTime} миллисекунд");
        }
    }
}
