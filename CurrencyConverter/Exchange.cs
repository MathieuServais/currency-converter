using System;

namespace CurrencyConverter
{
    /// <summary>
    /// Exchange rate from currency to another
    /// </summary>
    public class Exchange
    {
        /// <summary>
        /// Original currency for exchange
        /// </summary>
        public Currency OriginalCurrency { get; }

        /// <summary>
        /// Target currencu for exchange 
        /// </summary>
        public Currency TargetCurrency { get; }

        /// <summary>
        /// Rate for convertion (original to target)
        /// </summary>
        private readonly double _rate;

        /// <summary>
        /// Contructor exchange rate
        /// </summary>
        /// <param name="originalCurrency">Original currency</param>
        /// <param name="targetCurrency">Target currency</param>
        /// <param name="rate">echange rate</param>
        public Exchange(Currency originalCurrency, Currency targetCurrency, double rate)
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
            return Equals(OriginalCurrency, currency) ||
                   Equals(TargetCurrency, currency);
        }

        /// <summary>
        /// Return the other currency (original or target).
        /// </summary>
        /// <param name="currente">currency know</param>
        /// <returns>other currency in relation for this change</returns>
        public Currency GetNext(Currency currente)
        {
            return Equals(OriginalCurrency, currente) ? TargetCurrency : OriginalCurrency;
        }

        /// <summary>
        /// Return exchage rate to convert to this currency
        /// </summary>
        /// <param name="currency">currency to be attained</param>
        /// <returns></returns>
        public double GetRateTo(Currency currency)
        {
            return Equals(currency, TargetCurrency) ? _rate : Math.Round(1 / _rate, 4);
        }

        #region Equals

        public override bool Equals(object obj)
        {
            var change = obj as Exchange;
            if (change == null) return false;

            return change.OriginalCurrency.Equals(OriginalCurrency) &&
                   change.TargetCurrency.Equals(TargetCurrency) &&
                   Math.Abs(change._rate - _rate) < 0.0001;
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
