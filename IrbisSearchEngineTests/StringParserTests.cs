using Microsoft.VisualStudio.TestTools.UnitTesting;
using IrbisSearchEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrbisSearchEngine.Tests
{
    [TestClass()]
    public class StringParserTests
    {
        [TestMethod()]
        public void CreateSearchTermTest()
        {
            StringParser stringParser = new();
            string str = "Капитанская дочка? :";
            string actual = stringParser.SearchTerm(str);
            string expected = "\"K=КАПИТАНСК$ . \"K=ДОЧК$";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FormatAsHttpTest()
        {
            Assert.Fail();
        }
    }
}