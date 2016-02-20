using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter
{
    /// <summary>
    /// Convert currency with the smallest number of convertion (with A*)
    /// </summary>
    public class CurrencyConverter
    {
        /// <summary>Input amount that is to be converted</summary>
        public double InputAmount { get; set; }

        /// <summary>Input currency</summary>
        public Currency InputCurrency { get; set; }

        /// <summary>Ouput currency</summary>
        public Currency OutputCurrency { get; set; }

        /// <summary>Proxy for parse input data and format output data</summary>
        private InputOutputProxy _inputOutputProxy;

        /// <summary>
        /// Function to convert input flow
        /// </summary>
        /// <param name="inputLineList">input date from program</param>
        /// <returns>result in good format for program</returns>
        public string Convert(List<string> inputLineList)
        {
            PrepareInputData(inputLineList);

            CalculateBestPathConversion();

            if (!HasValidResult()) return "ERROR";
            var outputAmount = CalculateChange(InputAmount, InputCurrency, OutputCurrency);
            return _inputOutputProxy.FormatResult(outputAmount);
        }

        /// <summary>
        /// Initialize data from input for algo execution (draw the graph between currencies)
        /// </summary>
        /// <param name="lineList">input flux</param>
        private void PrepareInputData(List<string> lineList)
        {
            _inputOutputProxy = new InputOutputProxy(lineList);

            InputAmount = _inputOutputProxy.GetInputAmount();
            InputCurrency = _inputOutputProxy.GetInputCurrency();
            OutputCurrency = _inputOutputProxy.GetOutputCurrency();

            _inputOutputProxy.GetChangeList();
        }

        /// <summary>
        /// Execute A* algorithm for find the best path for convert the input amout to output currency.
        /// After calcule, each currency know the best previous currency (for change).
        /// </summary>
        private void CalculateBestPathConversion()
        {
            var currencyQueue = new Queue<Currency>();
            currencyQueue.Enqueue(InputCurrency);
            InputCurrency.Previous = InputCurrency;

            while (currencyQueue.Count > 0)
            {
                var actualCurrency = currencyQueue.Dequeue();
                foreach (var targetCurrency in actualCurrency.GetCurrencyListToChange())
                {
                    // If currency have already find best path
                    if (targetCurrency.Previous != null)
                        continue;

                    targetCurrency.Previous = actualCurrency;

                    // full path find => stop
                    if (Equals(targetCurrency, OutputCurrency))
                    {
                        currencyQueue.Clear();
                        break;
                    }

                    currencyQueue.Enqueue(targetCurrency);
                }
            }
        }

        /// <summary>
        /// Return error if A* don't find valid path
        /// </summary>
        private bool HasValidResult()
        {
            return OutputCurrency.Previous != null;
        }

        /// <summary>
        /// Calculate the new amount for the output currency from the input.
        /// </summary>
        /// <param name="amount">actual amount</param>
        /// <param name="origineCurrency">actual currency</param>
        /// <param name="targetCurrency">target currency</param>
        /// <returns>target amount</returns>
        private static double CalculateChange(double amount, Currency origineCurrency, Currency targetCurrency)
        {
            var rateListOrdered = RateListOrdered(origineCurrency, targetCurrency);
            return rateListOrdered.Aggregate(amount, (current, rate) => Math.Round(current*rate, 4));
        }

        /// <summary>
        /// Reverse path for calculate (because target to original with currency.previous).
        /// For the calcule (with round) we need original to target.
        /// </summary>
        /// <param name="origineCurrency">Original currency</param>
        /// <param name="targetCurrency">Target currency</param>
        /// <returns>Ordoned exchange rate</returns>
        private static IEnumerable<double> RateListOrdered(Currency origineCurrency, Currency targetCurrency)
        {
            var rateListOrdered = new List<double>();
            var currency = targetCurrency;
            while (!Equals(currency, origineCurrency))
            {
                rateListOrdered.Add(currency.GetRateWithPrevious());
                currency = currency.Previous;
            }

            rateListOrdered.Reverse();
            return rateListOrdered;
        }
    }
}
