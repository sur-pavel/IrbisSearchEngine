using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace IrbisSearchEngine
{
    internal class MystemAnalysis
    {
        [JsonProperty("gr")]
        public string Grammeme { get; set; }

        public bool IsObscene
        {
            get { return Split.Contains("обсц"); }
        }

        [JsonProperty("lex")]
        public string Lexeme { get; set; }

        public string PartOfSpeech
        {
            get { return Split[0].Trim('='); }
        }

        [JsonProperty("wt")]
        public double Weight { get; set; }

        private string[] Split
        {
            get
            {
                if (string.IsNullOrEmpty(Grammeme))
                    return new[] { String.Empty };
                return Grammeme.Split(',');
            }
        }

        public override string ToString()
        {
            return string.Format
                (
                    "{0}, {1}",
                    Lexeme,
                    PartOfSpeech
                );
        }
    }
}