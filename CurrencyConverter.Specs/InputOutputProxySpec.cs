using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CurrencyConverter.UT
{
    [TestClass]
    public class InputOutputProxySpec
    {
        [TestMethod]
        public void GetInputCurrency_ShouldReturnCurrency_Line1Col1()
        {
            // Given
            var input = new List<string>
            {
                "EUR;550;JPY"
            };
            var parser = new InputOutputProxy(input);

            // When
            var actual = parser.GetInputCurrency();

            // Then
            Assert.AreEqual(new Currency("EUR"), actual);
        }

        [TestMethod]
        public void GetInputAmount_ShouldReturnAmount_Line1Col2()
        {
            var input = new List<string>
            {
                "EUR;550;JPY"
            };
            var parser = new InputOutputProxy(input);

            var actual = parser.GetInputAmount();

            Assert.AreEqual(550, actual);
        }

        [TestMethod]
        public void GetInputAmount_ShouldReturnAmount_Line1Col3()
        {
            var input = new List<string>
            {
                "EUR;550;JPY"
            };
            var parser = new InputOutputProxy(input);

            var actual = parser.GetOutputCurrency();

            Assert.AreEqual(new Currency("JPY"), actual);
        }

        [TestMethod]
        public void GetNumberChange_ShouldReturnNumberOfChange_Line2()
        {
            var input = new List<string>
            {
                "EUR;550;JPY",
                "1"
            };
            var parser = new InputOutputProxy(input);

            var actual = parser.GetNumberChange();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetChangeList_ShouldReturnOneChange_Line3()
        {
            var input = new List<string>
            {
                "EUR;550;JPY",
                "1",
                "EUR;JPY;0.9661"
            };
            var parser = new InputOutputProxy(input);
            var actual = parser.GetChangeList();

            var expected = new Change(new Currency("EUR"), new Currency("JPY"), 0.9661);
            Assert.AreEqual(expected, actual[0]);
        }

        [TestMethod]
        public void FormatResult_ShouldReturnInterString()
        {
            var proxy = new InputOutputProxy(null);
            var actual = proxy.FormatResult(1.9999);

            Assert.AreEqual("2", actual);
        }
    }
}
