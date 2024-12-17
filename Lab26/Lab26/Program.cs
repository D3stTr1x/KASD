using System;
using System.IO;
using System.Text.RegularExpressions;
using MyHashSet;

class Program
{
    static void Main(string[] args)
    {
        Regex wordRegex = new Regex(@"\b[a-zA-Z]+\b");
        MyHashSet<string> uniqueWords = new MyHashSet<string>();
        try
        {
            foreach (var line in File.ReadLines("input.txt"))
            {
                var matches = wordRegex.Matches(line);
                foreach (Match match in matches)
                {                    
                    uniqueWords.Add(match.Value.ToLower());
                }
            }            
            Console.WriteLine("Уникальные слова в файле:");
            foreach (var word in uniqueWords.ToArray())
            {
                Console.WriteLine(word);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
