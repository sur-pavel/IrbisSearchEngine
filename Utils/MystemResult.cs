using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrbisSearchEngine
{
    /// <summary>
    /// Результат анализа для одного слова.
    /// </summary>
    public class MystemResult
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        public MystemAnalysis[] Analysis { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(Text);
            foreach (MystemAnalysis analysis in Analysis)
            {
                result.Append("; ");
                result.Append(analysis);
            }
            return result.ToString();
        }
    }
}