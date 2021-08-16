using ManagedIrbis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrbisSearchEngine
{
    internal class FoundBook
    {
        public string BriefDescription { get; set; } = string.Empty;
        public string FullDescritption { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string DatabaseDescription { get; set; } = string.Empty;
        public string MainData { get; set; } = string.Empty;
        public MarcRecord Record { get; set; } = new MarcRecord();
    }
}