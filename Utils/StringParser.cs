using AM.AOT.Stemming;

using NLog;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace IrbisSearchEngine.Utils
{
    public class StringParser
    {
        private IStemmer stemmer;
        public const string FIELDS_TAGS = "700, 701, 702, 200, 961, 461";
        private const int SYMBOLS_IN_LINE = 63;

        public string CreateSearchTerm(string searchTerm)
        {
            if (searchTerm.Equals(null) || searchTerm.Equals(string.Empty))
            {
                return string.Empty;
            }
            else
            {
                List<string> list = new();
                searchTerm = Regex.Replace(searchTerm, @"[\p{P}\p{S}]", "");
                foreach (string splitTerm in searchTerm.Split(' '))
                {
                    string word = StemmWord(splitTerm);

                    list.Add($"\"K={word}$\"/()");
                }
                searchTerm = string.Join("*", list);
                string strongAndTerm = string.Join(" . ", list);
                string andSameSubfieldTerm = string.Join(" (F) ", list);
                string andSameFieldTerm = string.Join(" (G) ", list);
                string andEverywhereTerm = string.Join(" * ", list);
                return searchTerm;
            }
        }

        private string StemmWord(string splitTerm)
        {
            stemmer = new RussianStemmer();

            if (Regex.IsMatch(splitTerm, "[А-я]+"))
            {
                return stemmer.Stem(splitTerm).ToUpper();
            }
            else
            {
                return splitTerm.ToUpper();
            }
        }

        public string FormatAsHttp(string optimizedRecord)
        {
            optimizedRecord = optimizedRecord.Replace(System.Environment.NewLine, @"<br/>");
            return "<html><body>" + optimizedRecord + "</body></html>";
        }

        public string GetMainData(string fullDescription)
        {
            string mainData = HtmlUtilities.ConvertToPlainText(fullDescription);
            return Regex.Replace(mainData, ".{" + SYMBOLS_IN_LINE + "}(?!$)", "$0\n");
        }
    }
}