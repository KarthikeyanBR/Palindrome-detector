using System;
using System.Globalization;
using System.IO;

namespace Palindrome_detector
{
    public class Palindrome
    {
        public static void Main()
        {
            //Hard coded values can be moved to config files or create a constant class.
            var inputFile = File.ReadAllLines(@"C:\Test\WordsFile.txt");//Please place the input file the mentioned path
            var outputFilePath = @"C:\Test\ResultFile.txt";
            PalindromeDetector(inputFile, outputFilePath);

            Console.WriteLine("Press Any Key to continue");
            Console.ReadLine();
        }

        public static void PalindromeDetector(string[] inputFile, string outputFilePath)
        {
            try
            {
                //Input file does not contain any data 
                if (inputFile is null || inputFile.Length == 0)
                {
                    throw new ArgumentNullException("Input File Exception");
                }

                //Create a new file if not already existing or Clear the values insides the output file if exists for every new run
                if (!File.Exists(outputFilePath) || (File.Exists(outputFilePath) && outputFilePath.Length > 0))
                {
                    File.Create(outputFilePath).Dispose();
                }
                foreach (var line in inputFile)
                {
                    var reversedLine = "";

                    //Handling if the input file data contains only empty or while spaces
                    if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                    {
                        char[] charArray = line.ToCharArray();
                        Array.Reverse(charArray);
                        reversedLine = new string(charArray);
                        if (line.Equals(reversedLine, StringComparison.OrdinalIgnoreCase) && File.Exists(outputFilePath))
                        {
                            using (TextWriter tw = new StreamWriter(outputFilePath, true))
                            {
                                tw.WriteLine(line);//Write the Palindrome strings to the output file
                            }
                        }
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                //Logging of the errors is important for finding the root cause during production issues
                Console.WriteLine("Input File Cant be Empty", ex.Message);
            }
        }
    }
}
