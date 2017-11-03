using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace MediaParsing
{
    public class DirectoryParser
    {
        public MatchCollection GetRegExMatches(string pattern, string line)
        {
            return Regex.Matches(line, pattern, RegexOptions.IgnoreCase);
        }

        public IEnumerable<string> GetMediaItemsForDirectory(string[] directories)
        {
            var results = new List<string>();
           
           IEnumerable <string> filesInDir = new List<string>();
            foreach (string directory in directories)
            {
                foreach(var innerDirectory in Directory.EnumerateDirectories(directory))
                {
                    results.AddRange(GetCsvEntriesForDirectory(innerDirectory));
                }
                results.AddRange(GetCsvEntriesForDirectory(directory));
            }

            return results;
        }

        private IEnumerable<string> GetCsvEntriesForDirectory(string innerDirectory)
        {
            var results = new List<string>();
            string csvString = "";
            IEnumerable<string> filesInDir = Directory.EnumerateFiles(innerDirectory);
            foreach (var line in filesInDir)
            {
                var newLine = line.Substring(innerDirectory.Length + 1);
                var matches = GetRegExMatches(@"(.+)[\s.]((\d{4})|\((\d+)\))[.\s]", newLine);

                if (matches.Count == 0)
                    continue;

                var matchData = matches[0].Groups;
                var title = matchData[1].Value.Replace('.', ' ');
                var year = matchData[3] == null ? matchData[2].Value : matchData[3].Value;
                csvString = $"{title}, {year}";

                bool skipEntry = results.Any(result => result.ToLower() == csvString.ToLower()) || year.Length != 4;

                if (skipEntry)
                    continue;

                results.Add(csvString);
            }

            return results;
        }
    }
}
