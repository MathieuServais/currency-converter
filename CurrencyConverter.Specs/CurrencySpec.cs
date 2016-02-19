using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyConverter.UT
{
    [TestClass]
    public class CurrencySpec
    {
        [TestMethod]
        public void Currency_ShouldHaveCodeIso()
        {
            var currency = new Currency("EUR");
            Assert.AreEqual("EUR", currency.IsoCode);
        }

        [TestMethod]
        public void GetCurrencyListToChange_ShouldReturnEligibleCurrency()
        {
            var currency = new Currency("EUR");
            var currencyTarget = new Currency("CHF");
            var change = new Change(currency, currencyTarget, 1);

            var actual = currency.GetCurrencyListToChange();

            Assert.AreEqual(currencyTarget, actual[0]);
        }

        [TestMethod]
        public void GetRateWithPrevious_ShouldReturnChangeRate_WithPreviousCurrency()
        {
            var currency = new Currency("EUR");
            var currencyTarget = new Currency("CHF");
            new Change(currency, currencyTarget, 1.2345);
            var othercurrency = new Currency("OTH");
            new Change(othercurrency, currencyTarget, 0.0);

            // After Dijkstra
            currencyTarget.Previous = currency;

            var actual = currencyTarget.GetRateWithPrevious();

            Assert.AreEqual(1.2345, actual);
        }
    }
}
