using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Range = Microsoft.Office.Interop.Word.Range;

namespace IrbisSearchEngine
{
    internal class WordHandler
    {
        private Document objDoc;
        private Application wordApp;
        private Patterns patterns;
        private string appPath;

        internal WordHandler(Patterns patterns)
        {
            this.patterns = patterns;
            wordApp = new Application();
            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        internal void WriteToWord(string text, string fileName)
        {
            objDoc = wordApp.Documents.Open(appPath + @"\" + fileName, ReadOnly: false);
            Range rng = wordApp.ActiveDocument.Range(0, 0);
            rng.Text += text;
        }

        internal void Quit()
        {
            objDoc.Close();
            wordApp.Quit();
        }
    }
}