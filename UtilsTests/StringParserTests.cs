using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
namespace IrbisSearchEngine.Utils.Tests
{
    [TestClass()]
    public class StringParserTests
    {
        private StringParser stringParser;
private Logger logger;
        [TestMethod()]
        public void CreateSearchTermTest()
        {
             LogRunner logRunner = new();
             this.logger = logRunner.Logger;
            stringParser = new StringParser(this.logger);
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