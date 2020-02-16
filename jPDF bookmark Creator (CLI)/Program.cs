using System;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Text.RegularExpressions;

namespace jPDF_bookmark_Creator__CLI_
{
    class Program
    {
        static void Main(string[] args)
        => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "Path of the file to convert", ShortName ="i",LongName ="input")]
        [FileExists]
        public string inputPath { get; }

        [Option(Description = "Path of the Output File (ex. C:\\user\\<userName>\\desktop)", ShortName = "o", LongName = "output")]
        [DirectoryExists]
        public string outputPath { get; }
        private void OnExecute()
        {
            var InputPath = inputPath;
            var OutputPath = outputPath;
            int st;
            String txt = File.ReadAllText(inputPath);
            String[] firstLine = File.ReadAllLines(inputPath);
            try
            {
                st = int.Parse(firstLine[0]);
            }
            catch (FormatException)
            {

                st = 0;
            }
            Regex rx = new Regex(@"(.*?)[\W]+(\d+)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(txt);
            String finalBookmarkText = "";
            try
            {
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    finalBookmarkText += groups[1].Value + "\\" + (int.Parse(groups[2].Value) + st) + "\n";
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Make sure the text file is in correct format: " + ex.Message, "Exception");
            }
            File.WriteAllText(outputPath + "\\BookmarksConverted.txt", finalBookmarkText);
            Console.WriteLine("-----Conversion Finished---------");
        }

    }
}

