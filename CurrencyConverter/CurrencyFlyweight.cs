using System;
using System.Collections.Generic;
using System.Linq;
namespace CurrencyConverter
{
    /// <summary>
    /// Gurantee the uniqueness instance for each currency (Iso code)
    /// </summary>
    public class CurrencyFlyweight
    {
        /// <summary>
        /// Cache
        /// </summary>
        private readonly List<Currency> _currencyListCache;

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyFlyweight()
        {
            _currencyListCache = new List<Currency>();
        }

        /// <summary>
        /// CreateOrGet currency from iso code
        /// </summary>
        /// <param name="currencyIsoCode">iso currency code</param>
        /// <returns>Unique instance currency for this iso code</returns>
        public Currency Get(string currencyIsoCode)
        {
            var currency = _currencyListCache.FirstOrDefault(_ => _.IsoCode == currencyIsoCode);
            if (currency != null) return currency;

            currency = new Currency(currencyIsoCode);
            _currencyListCache.Add(currency);
            return currency;
        }
    }
}
