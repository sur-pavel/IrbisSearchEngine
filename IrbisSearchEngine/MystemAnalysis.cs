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
        /// <summary>
        /// Грамматический разбор слова
        /// </summary>
        [JsonProperty("gr")]
        public string Grammeme { get; set; }

        /// <summary>
        /// Обсценное?
        /// </summary>
        public bool IsObscene
        {
            get { return Split.Contains("обсц"); }
        }

        /// <summary>
        /// Основная форма слова
        /// </summary>
        [JsonProperty("lex")]
        public string Lexeme { get; set; }

        /// <summary>
        /// Часть речи.
        /// </summary>
        public string PartOfSpeech
        {
            get { return Split[0].Trim('='); }
        }

        /// <summary>
        /// Относительный вес
        /// </summary>
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

        /// <inheritdoc />
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