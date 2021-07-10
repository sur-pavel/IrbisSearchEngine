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
        public string BriefDescription { get; set; }
        public string FullDescritption { get; set; }
        public string DatabaseName { get; set; }
        public MarcRecord Record { get; set; }
    }
}