using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyConverter.UT
{
    [TestClass]
    public class ChangeSpec
    {
        private Currency _originalCurrency = new Currency("EUR");
        private Currency _targetCurrency = new Currency("CHF");

        [TestMethod]
        public void Change_ShouldHaveOriginalCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            Assert.AreEqual(_originalCurrency, change.OriginalCurrency);
        }

        [TestMethod]
        public void Change_ShouldHaveTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            Assert.AreEqual(_targetCurrency, change.TargetCurrency);
        }

        [TestMethod]
        public void AddCurrencyMapping_ShouldCreateMappingWithOriginalCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            Assert.AreEqual(_originalCurrency.ChangeList[0], change);
        }

        [TestMethod]
        public void AddCurrencyMapping_ShouldCreateMappingWithTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetNext(_originalCurrency);
            Assert.AreEqual(_targetCurrency, actual);
        }

        [TestMethod]
        public void GetNext_ShouldReturnOriginalCurrency_WhenCurrentisTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetNext(_targetCurrency);
            Assert.AreEqual(_originalCurrency, actual);
        }

        [TestMethod]
        public void GetRateTo_ShouldReturnRate_WithTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetRateTo(_originalCurrency);
            Assert.AreEqual(0.8297, actual);
        }

        [TestMethod]
        public void GetRateTo_ShouldReturnInvertedRate_WithOriginalCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetRateTo(_targetCurrency);
            Assert.AreEqual(1.2053, actual);
        }
    }
}
