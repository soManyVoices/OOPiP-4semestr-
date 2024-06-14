using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string path = "C:\\Users\\Alexander\\Desktop\\С#\\Lab9Dem\\Lab9Dem\\bin\\Debug\\example.txt";
        Regex.Replace(File.ReadAllText(path), @"\W+", " ")
                         .ToLower()
                         .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                         .Where(word => word.Length >= 4)
                         .ToList()
                         .ForEach(Console.WriteLine);
        Console.WriteLine("Нажмите для выхода");
        Console.ReadKey();
    }
}