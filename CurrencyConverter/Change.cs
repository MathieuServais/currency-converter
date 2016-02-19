using System;

namespace CurrencyConverter
{
    /// <summary>
    /// Exchange rate from currency to another
    /// </summary>
    public class Change
    {
        /// <summary>
        /// Original currency for exchange
        /// </summary>
        public Currency OriginalCurrency { get; private set; }

        /// <summary>
        /// Target currencu for exchange 
        /// </summary>
        public Currency TargetCurrency { get; private set; }

        /// <summary>
        /// Rate for convertion (original to target)
        /// </summary>
        private double _rate;

        /// <summary>
        /// Contructor exchange rate
        /// </summary>
        /// <param name="originalCurrency">Original currency</param>
        /// <param name="targetCurrency">Target currency</param>
        /// <param name="rate">echange rate</param>
        public Change(Currency originalCurrency, Currency targetCurrency, double rate)
        {
            OriginalCurrency = originalCurrency;
            TargetCurrency = targetCurrency;
            _rate = rate;

            MapWithCurrency();
        }

        /// <summary>
        /// Mapping between Currency and change
        /// </summary>
        private void MapWithCurrency()
        {
            OriginalCurrency.ChangeList.Add(this);
            TargetCurrency.ChangeList.Add(this);
        }

        /// <summary>
        /// Indicate if change use this currency
        /// </summary>
        /// <param name="currency">Currency tested</param>
        /// <returns>True if currency is used, false otherwise</returns>
        public bool HasCurrency(Currency currency)
        {
            return OriginalCurrency == currency ||
                   TargetCurrency == currency;
        }

        /// <summary>
        /// Return the other currency (original or target).
        /// </summary>
        /// <param name="currente">currency know</param>
        /// <returns>other currency in relation for this change</returns>
        public Currency GetNext(Currency currente)
        {
            return OriginalCurrency == currente ? TargetCurrency : OriginalCurrency;
        }

        /// <summary>
        /// Return exchage rate to convert to this currency
        /// </summary>
        /// <param name="currency">currency to be attained</param>
        /// <returns></returns>
        public double GetRateTo(Currency currency)
        {
            if (currency == TargetCurrency)
                return _rate;
            else
                return Math.Round(1 / _rate, 4);
        }

        #region Equals

        public override bool Equals(object obj)
        {
            var change = obj as Change;
            if (change == null) return false;

            return change.OriginalCurrency.Equals(OriginalCurrency) &&
                   change.TargetCurrency.Equals(TargetCurrency) &&
                   change._rate == _rate;
        }

        public override int GetHashCode()
        {
            return OriginalCurrency.GetHashCode() ^
                   TargetCurrency.GetHashCode() ^
                   _rate.GetHashCode();
        }

        #endregion
    }
}
