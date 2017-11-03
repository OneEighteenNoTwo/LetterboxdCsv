using CommandLineManager;
using MediaParsing;
using System;
using System.IO;
using System.Linq;
namespace LetterboxedCsvGenerator
{
    class Program
    {
        class CommandLineOptions
        {
            [Option("dirs", "directories", Required = true)]
            public string directoriesToParse { get; set; }

            [Option("o", "outputfile", Required = false)]
            public string outputFilePath { get; set; }
        }


        static string[] directoriesToParse = null;
        static string CSV_OUTPUT_FILE_FULL_PATH = @"r:\LetterBoxd.csv";
        static void Main(string[] args)
        {
            var commandLineOptions = new CommandLineOptions();
            var clParser = new CLArgumentParser();
            clParser.ParseCommandLineArguments(args, commandLineOptions);

            if(commandLineOptions.directoriesToParse.Length == 0)
            {
                directoriesToParse = new string[] { @"F:\media\movies", @"K:\media\movies" };
            }
            else
            {
                directoriesToParse = commandLineOptions.directoriesToParse.Split(',');
            }

            CSV_OUTPUT_FILE_FULL_PATH = commandLineOptions.outputFilePath.Length > 0 ? commandLineOptions.outputFilePath : CSV_OUTPUT_FILE_FULL_PATH;

            try
            {
                var directoryParser = new DirectoryParser();
                var movies = directoryParser.GetMediaItemsForDirectory(directoriesToParse);
                movies = movies.OrderBy(csvString => csvString);

                using (var streamWriter = new StreamWriter(CSV_OUTPUT_FILE_FULL_PATH))
                {
                    foreach (var csvLine in movies)
                    {
                        streamWriter.WriteLine(csvLine);
                    }

                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
