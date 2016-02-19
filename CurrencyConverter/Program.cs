using System;
using System.Collections.Generic;

namespace CurrencyConverter
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Read input paramters
            var lineParamList = new List<string>();
            lineParamList.Add(Console.ReadLine());
            lineParamList.Add(Console.ReadLine());

            // Number of line we need to read
            long numLineInput = 0;
            long.TryParse(lineParamList[1], out numLineInput);
            for (long i =0; i < numLineInput; i++)
                lineParamList.Add(Console.ReadLine());

            // Execute function of convertion
            var currencyConverter = new CurrencyConverter();
            var result = currencyConverter.Convert(lineParamList);

            // Display output
            Console.WriteLine(result);

#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
