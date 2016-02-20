using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter
{
    /// <summary>
    /// Currency is node for Dijkstra
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// ISO currency names (3 caractères)
        /// </summary>
        public string IsoCode { get; }

        /// <summary>
        /// Previous currency for best path on Dijkstra resolution
        /// </summary>
        public Currency Previous { get; set; }

        /// <summary>
        /// List of change with this currency
        /// </summary>
        public List<Change> ChangeList { get; set; }

        /// <summary>
        /// Constructor with iso normes
        /// </summary>
        /// <param name="isoCode">ISO Code</param>
        public Currency(string isoCode)
        {
            IsoCode = isoCode;
            ChangeList = new List<Change>();
        }

        /// <summary>
        /// Return distinct connected currency linked with exchange rate.
        /// </summary>
        /// <returns></returns>
        public List<Currency> GetCurrencyListToChange()
        {
            var currencyList = new List<Currency>();
            foreach(var change in ChangeList)
            {
                change.GetNext(this);
                currencyList.Add(change.GetNext(this));
            }

            return currencyList;
        }

        /// <summary>
        /// return exchange rate to currency of the best path
        /// </summary>
        /// <returns></returns>
        public double GetRateWithPrevious()
        {
            return ChangeList.FirstOrDefault(_ => _.HasCurrency(Previous)).GetRateTo(this);
        }

        #region Equals

        public override bool Equals(object obj)
        {
            var currency = obj as Currency;
            if (currency == null) return false;

            return currency.IsoCode == IsoCode;
        }

        public override int GetHashCode()
        {
            return IsoCode.GetHashCode();
        }

        #endregion
    }
}
