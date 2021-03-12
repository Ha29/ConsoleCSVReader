using System;
using System.Collections.Generic;

namespace Week1_ConsoleCSVDataReader
{
    class Program
    {   

        static void Main(string[] args)
        {
            try
            {
                string[] paths = InputRetriever.PromptUser();
                string inputPath = paths[0];
                string outputPath = paths[1];
                DateTime start = DateTime.Now;
                List<Person> persons = FileHandler.ReadCSV(inputPath);
                persons.Sort();
                FileHandler.WriteJson(persons, outputPath);
                DateTime finishTime = DateTime.Now;
                var totalTime = finishTime.Subtract(start);
                Console.WriteLine($"1. Lines of data: {persons.Count}");
                Console.WriteLine($"2. Total time to process lines: {totalTime} [FORMAT = HH:MM:SS.MS]");
                Console.WriteLine($"3. Average time per line: {totalTime / persons.Count} [FORMAT = HH:MM:SS.MS]");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Message: " + e.Message);
                Environment.Exit(1);
            }
        }
    }
}
