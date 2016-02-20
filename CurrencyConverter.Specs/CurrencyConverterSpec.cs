using NUnit.Framework;
using System.Collections.Generic;

namespace CurrencyConverter.UT
{
    [TestFixture]
    public class CurrencyConverterSpec
    {
        [Test]
        public void Convert_ShouldReturnNumber_WithOneChangeOrderedValid()
        {
            var lines = new List<string>
            {
                "EUR;1;JPY",
                "1",
                "EUR;JPY;2",
            };
            var converter = new CurrencyConverter();
            var actual = converter.Convert(lines);

            Assert.AreEqual("2", actual);
        }

        [Test]
        public void Convert_ShouldReturnNumber_WithOneChangeInvertedValid()
        {
            var lines = new List<string>
            {
                "EUR;1;JPY",
                "1",
                "JPY;EUR;0.6",
            };
            var converter = new CurrencyConverter();
            var actual = converter.Convert(lines);

            Assert.AreEqual("2", actual);
        }

        [Test]
        public void Convert_ShouldReturnNumber_WithDataOfConsigne()
        {
            var lines = new List<string>
            {
                "EUR;550;JPY",
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };
            var converter = new CurrencyConverter();
            var actual = converter.Convert(lines);

            Assert.AreEqual("59033", actual);
        }

        [Test]
        public void Convert_ShouldFindTheSmallestPath()
        {
            var lines = new List<string>
            {
                "EUR;1;JPY",
                "7",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571",
                "EUR;JPY;2"
            };
            var converter = new CurrencyConverter();
            var actual = converter.Convert(lines);

            Assert.AreEqual("2", actual);
        }

        [Test]
        public void Convert_ShouldThrowError_WhenNoWay()
        {
            var lines = new List<string>
            {
                "EUR;1;JPY",
                "1",
                "JPI;EUR;0.6",
            };
            var converter = new CurrencyConverter();
            var actual = converter.Convert(lines);

            Assert.AreEqual("ERROR", actual);
        }
    }
}
