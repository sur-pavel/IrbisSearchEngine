using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrbisSearchEngine.Utils.Tests
{
    [TestClass()]
    public class StringParserTests
    {
        private StringParser stringParser;

        [TestMethod()]
        public void CreateSearchTermTest()
        {
            stringParser = new StringParser();
            string expected = string.Format("\"K=КАПИТАНСК$\"/({0}) . \"K=ДОЧК$\"/({0})", StringParser.FIELDS_TAGS);
            string actual = stringParser.CreateSearchTerm("Капитанская дочка.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FormatAsHttpTest()
        {
            Assert.Fail();
        }
    }
}