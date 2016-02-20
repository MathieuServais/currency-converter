using NUnit.Framework;

namespace CurrencyConverter.Specs
{
    [TestFixture]
    public class ChangeSpec
    {
        private readonly Currency _originalCurrency = new Currency("EUR");
        private readonly Currency _targetCurrency = new Currency("CHF");

        [Test]
        public void Change_ShouldHaveOriginalCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            Assert.AreEqual(_originalCurrency, change.OriginalCurrency);
        }

        [Test]
        public void Change_ShouldHaveTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            Assert.AreEqual(_targetCurrency, change.TargetCurrency);
        }

        [Test]
        public void AddCurrencyMapping_ShouldCreateMappingWithOriginalCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            Assert.AreEqual(_originalCurrency.ChangeList[0], change);
        }

        [Test]
        public void AddCurrencyMapping_ShouldCreateMappingWithTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetNext(_originalCurrency);
            Assert.AreEqual(_targetCurrency, actual);
        }

        [Test]
        public void GetNext_ShouldReturnOriginalCurrency_WhenCurrentisTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetNext(_targetCurrency);
            Assert.AreEqual(_originalCurrency, actual);
        }

        [Test]
        public void GetRateTo_ShouldReturnRate_WithTargetCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetRateTo(_originalCurrency);
            Assert.AreEqual(0.8297, actual);
        }

        [Test]
        public void GetRateTo_ShouldReturnInvertedRate_WithOriginalCurrency()
        {
            var change = new Change(_originalCurrency, _targetCurrency, 1.2053);
            var actual = change.GetRateTo(_targetCurrency);
            Assert.AreEqual(1.2053, actual);
        }
    }
}
