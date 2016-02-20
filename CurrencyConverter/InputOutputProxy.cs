using System;
using System.Collections.Generic;
using System.Globalization;

namespace CurrencyConverter
{
    /// <summary>
    /// Matching between user interface (Console) and the fonction of currencyConvert.
    /// Prepare data for call the converter (object and graph for input data).
    /// Input format:
    /// <code>
    /// InputCurrency;InputAmount;OuputCurrency
    /// NumberChangeLine
    /// OriginalCurrency;TargetCurrency;ExchangeRate
    /// OriginalCurrency;TargetCurrency;ExchangeRate
    /// OriginalCurrency;TargetCurrency;ExchangeRate
    /// ...
    /// </code>
    /// Outpur format:
    /// <code>
    /// OutputAmount
    /// </code>
    /// </summary>
    public class InputOutputProxy
    {
        private const int INPUT_CURRENCY_LINE = 0;
        private const int INPUT_CURRENCY_COL  = 0;
        private const int INPUT_AMOUNT_LINE = 0;
        private const int INPUT_AMOUNT_COL = 1;
        private const int OUTPUT_CURRENCY_LINE = 0;
        private const int OUTPUT_CURRENCY_COL = 2;

        private const int CHANGE_NUMBER_LINE = 1;
        private const int CHANGE_LINE_START = 2;
        private const int CHANGE_BEGIN_CURRENCY_COL = 0;
        private const int CHANGE_END_CURRENCY_COL = 1;
        private const int CHANGE_RATE_COL = 2;

        private readonly List<string> _inputLineList;
        private readonly CurrencyFlyweight _currencyFlyweight;

        public InputOutputProxy(List<string> inputLineList)
        {
            _inputLineList = inputLineList;
            _currencyFlyweight = new CurrencyFlyweight();
        }

        public Currency GetInputCurrency()
        {
            var codeIso = _inputLineList[INPUT_CURRENCY_LINE].Split(';')[INPUT_CURRENCY_COL];
            return _currencyFlyweight.Get(codeIso);
        }

        public double GetInputAmount()
        {
            var amountString = _inputLineList[INPUT_AMOUNT_LINE].Split(';')[INPUT_AMOUNT_COL];
            return double.Parse(amountString, CultureInfo.InvariantCulture);
        }

        public Currency GetOutputCurrency()
        {
            var codeIso = _inputLineList[OUTPUT_CURRENCY_LINE].Split(';')[OUTPUT_CURRENCY_COL];
            return _currencyFlyweight.Get(codeIso);
        }

        public int GetNumberChange()
        {
            var numberChangeString = _inputLineList[CHANGE_NUMBER_LINE];
            return int.Parse(numberChangeString, CultureInfo.InvariantCulture);
        }

        public List<Change> GetChangeList()
        {
            var numberChange = GetNumberChange();
            var changeList = new List<Change>(numberChange);

            var changeLineMax = CHANGE_LINE_START + numberChange;
            for (var i = CHANGE_LINE_START; i < changeLineMax; i++)
                changeList.Add(GetChangeFromLine(_inputLineList[i]));

            return changeList;
        }

        private Change GetChangeFromLine(string line)
        {
            var input = line.Split(';');

            var beginCurrency = _currencyFlyweight.Get(input[CHANGE_BEGIN_CURRENCY_COL]);
            var endCurrency = _currencyFlyweight.Get(input[CHANGE_END_CURRENCY_COL]);
            var rate = double.Parse(input[CHANGE_RATE_COL], CultureInfo.InvariantCulture);

            return new Change(beginCurrency, endCurrency, rate);
        }

        public string FormatResult(double outputAmount)
        {
            return Math.Round(outputAmount, 0).ToString(CultureInfo.InvariantCulture);
        }
    }
}
