using System;
using System.IO;

namespace Week1_ConsoleCSVDataReader
{
    public static class InputRetriever
    {
        /// <summary>
        /// Gets a yes (Y) or no (N) response from the user through the console. 
        /// </summary>
        /// <param name="message">The message displayed to the console user right before they answer yes or no. </param>
        /// <returns>The response (Y) or (N)</returns>
        public static string GetValidResponse(string message)
        {
            Console.WriteLine($"{message} (Y/N)");
            string response = Console.ReadLine();
            while (!response.Equals("Y") && !response.Equals("N"))
            {
                Console.WriteLine($"{message} (Y/N)");
                response = Console.ReadLine();
            }
            return response;
        }
        /// <summary>
        /// Prompts user to provide the output folder and rejects the folder if it is invalid. 
        /// </summary>
        /// <param name="question">Question displayed to user before the user answers</param>
        /// <returns>The path of the folder for output</returns>
        public static string GetOutputPath(string question)
        {
            Console.WriteLine(question);
            string outputPath = Console.ReadLine();
            if (!Directory.Exists(outputPath))
            {
                throw new ArgumentException($"Directory {outputPath} does not exist");
            }
            if (!outputPath[outputPath.Length - 1].Equals("/"))
            {
                outputPath += "/";
            }
            return outputPath;
        }
        /// <summary>
        /// Gets the valid file name from the user through the console.
        /// </summary>
        /// <param name="outputPath">The folder path of the output already provided by the user. </param>
        /// <returns>The full path of the filename to write to as output. </returns>
        public static string GetValidFileName(string outputPath)
        {
            Console.WriteLine("Enter new filename without the extension: ");
            string fileName = Console.ReadLine();
            string tentativeFileName = outputPath + fileName + ".json";
            while (!FileHandler.IsFileNameValid(fileName) || File.Exists(tentativeFileName))
            {
                Console.WriteLine("Please enter a different filename. Either your filename was invalid or it already exists");
                fileName = Console.ReadLine();
                tentativeFileName = outputPath + fileName + ".json";
            }
            return tentativeFileName;
        }
        /// <summary>
        /// Prompts the user to answer a series of questions for input and output locations. 
        /// </summary>
        /// <returns></returns>
        public static string[] PromptUser()
        {
            Console.WriteLine("Where is the full path of the import csv? ");
            string inputPath = Console.ReadLine();
            if (!File.Exists(inputPath))
            {
                throw new ArgumentException($"File {inputPath} does not exist");
            }
            string outputPath = GetOutputPath("Where should the export file be saved to? ");
            if (File.Exists(outputPath + "converted.json"))
            {
                string message = $"The file {outputPath}converted.json exists, would you like to overwrite it (Y) or (N) to create a new file?";
                string response = GetValidResponse(message);
                if (response.Equals("Y"))
                {
                    return new string[] { inputPath, outputPath + "converted.json"};
                }
                else
                {
                    message = "Would you like to enter a different filename instead of a different location?";
                    response = GetValidResponse(message);
                    if (response.Equals("Y"))
                    {
                        string outputFile = GetValidFileName(outputPath);
                        return new string[] { inputPath, outputFile };
                    }
                    else
                    {
                        string newOutputPath = GetOutputPath("Please enter a different folder to save the export: ");
                        while (newOutputPath == outputPath)
                        {
                            newOutputPath = GetOutputPath("Please enter a different folder to save the export: ");
                        }
                        if (File.Exists(newOutputPath + "converted.json"))
                        {
                            string outputFile = GetValidFileName(newOutputPath);
                            return new string[] { inputPath, outputFile };
                        } 
                        else
                        {
                            return new string[] { inputPath, newOutputPath + "converted.json" };
                        }
                    }
                }
            }
            else
            {
                return new string[] { inputPath, outputPath + "converted.json" };
            }
        }
    }
}
