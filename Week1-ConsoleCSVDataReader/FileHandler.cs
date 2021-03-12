using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace Week1_ConsoleCSVDataReader
{
    public static class FileHandler
    {
        /// <summary>
        /// Reads the CSV file given the input file's full path. 
        /// </summary>
        /// <param name="inputPath">The full path of the input file. </param>
        /// <returns>A List of records from the CSV</returns>
        public static List<Person> ReadCSV(string inputPath)
        {
            using (var reader = new StreamReader(inputPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                IEnumerable<Person> records = csv.GetRecords<Person>();
                return records.ToList();
            }
        }
        /// <summary>
        /// Writes to the JSON file with the filepath specified with the content as the list of persons.
        /// </summary>
        /// <param name="persons">List of persons from the CSV file</param>
        /// <param name="filepath">The full filepath to write the json file to. </param>
        public static void WriteJson(List<Person> persons, string filepath)
        {
            string data = JsonSerializer.Serialize<List<Person>>(persons);
            using StreamWriter writer = new StreamWriter(filepath);
            writer.WriteLine(data);
            Console.WriteLine("Sucessfully written to " + filepath);
        }
        /// <summary>
        /// Returns whether the filename is a valid filename. 
        /// </summary>
        /// <param name="fileName">A name passed in to be checked for validity. </param>
        /// <returns>Whether a filename is a valid.</returns>
        public static Boolean IsFileNameValid(string fileName)
        {
            return !fileName.Any(f => Path.GetInvalidFileNameChars().Contains(f));
        }
    }
}
