using StringCalculator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorTDD
{
    public class StringCalculator
    {
        private const int MaxNumberToCalculate = 1000;

        private readonly IDelimiterParser _delimiterParser;

        public StringCalculator()
        {
            this._delimiterParser = new DelimiterParser();
        }

        public StringCalculator(IDelimiterParser delimiterParser)
        {
            this._delimiterParser = delimiterParser;
        }

        public int Add(string numbers)
        {
            if (IsNullEmptyOrWhitespaceFilled(numbers))
                return 0;

            var delimiters = _delimiterParser.GetDelimitersFrom(numbers);
            numbers = _delimiterParser.RemoveDelimiterDataFrom(numbers);

            var integerNumbers
                = numbers
                    .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .Where(stringNumber => Convert.ToInt32(stringNumber) <= MaxNumberToCalculate)
                    .Select(stringNumber => Convert.ToInt32(stringNumber));

            Validate(integerNumbers);

            return integerNumbers.Sum();
        }

        private void Validate(IEnumerable<Int32> numbers)
        {
            var negativeNumbers
                 = numbers
                    .Where(number => number < 0)
                    .Select(number => number)
                    .ToList();

            if ( negativeNumbers.Any())
            {
                const string ExceptionMainMessage = "Negatives Not Allowed";

                var exceptionMessage 
                    = $"{ExceptionMainMessage} = {string.Join(",", negativeNumbers)}";

                throw new ArgumentException(exceptionMessage);
            }
        }

        private bool IsNullEmptyOrWhitespaceFilled(string numbers)
        {
            return string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers);
        }
    }
}
