using System;
using System.IO;
using System.Text.RegularExpressions;
using MyHashMap;

class Program
{
    static void Main()
    {
        MyHashMap<string, int> tagCounts = new MyHashMap<string, int>();
        string pattern = @"<\/?[a-zA-Z][a-zA-Z0-9]*>";
        foreach (var line in File.ReadLines("input.txt"))
        {
            string cleanedLine = line.Replace(" ", "");
            MatchCollection matches = Regex.Matches(cleanedLine, pattern); // Regex извлекает все определения, соответствующие pattern.
            foreach (Match match in matches)
            {
                string tag = match.Value.ToLower(); 
                tag = tag.Trim('<', '/'); 
                if (tagCounts.ContainsKey(tag))
                {
                    tagCounts.Put(tag, tagCounts.Get(tag) + 1);
                }
                else
                {
                    tagCounts.Put(tag, 1);
                }
            }
        }
        Console.WriteLine("Частота тегов:");
        foreach (var entry in tagCounts.EntrySet())
        {
            Console.WriteLine($"<{entry.Key}>: {entry.Value}");
        }
    }
}
