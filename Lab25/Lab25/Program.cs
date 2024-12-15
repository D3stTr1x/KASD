using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyHashSet;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var set = new SortedSet<string>(lines, new CustomStringComparer());

        foreach (var line in set)
        {
            Console.WriteLine(line);
        }
    }
}

public class CustomStringComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (x == null || y == null) throw new ArgumentNullException();

        // Разделение на слова и сортировка по длине
        var wordsX = x.Split(' ').Where(w => !string.IsNullOrEmpty(w)).OrderBy(w => w.Length).ToList();
        var wordsY = y.Split(' ').Where(w => !string.IsNullOrEmpty(w)).OrderBy(w => w.Length).ToList();

        for (int i = 0; i < Math.Min(wordsX.Count, wordsY.Count); i++)
        {
            int compare = wordsX[i].Length.CompareTo(wordsY[i].Length);
            if (compare != 0) return compare;
        }
        return wordsX.Count.CompareTo(wordsY.Count);
    }
}
