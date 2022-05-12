using System;

namespace StringCalculator
{
    public class DelimiterParser : IDelimiterParser
    {
        private const string DelimiterLineStartMarker = "//";
        private const string DelimiterLineEndMarker = "\n";
        private const string DelimiterOpeningBracket = "[";
        private const string DelimiterEndBracket = "[";
        private const string DelimiterListSeperator = "][";

        public string[] GetDelimitersFrom(string numbers)
        {
            if (NoDelimiterDataExistsIn(numbers))
                return new string[] { ",", "\n" };

            const int DelimiterDataStartPosition = 2;

            int delimiterDataLength
                = numbers.IndexOf(DelimiterLineEndMarker) - DelimiterDataStartPosition;

            var delimiterData = numbers.Substring(DelimiterDataStartPosition, delimiterDataLength);

            if (delimiterData.Contains(DelimiterOpeningBracket) ||
               delimiterData.Contains(DelimiterEndBracket))
            {
                const int DelimiterListStartPosition = 1;
                const int DelimiterListEndAdjustment = 2;

                delimiterData = delimiterData
                    .Substring(DelimiterListStartPosition, delimiterData.Length - DelimiterListEndAdjustment);

                return delimiterData
                        .Split(new string[] { DelimiterListSeperator }, StringSplitOptions.RemoveEmptyEntries);
            }

            const int DefaultDelimiterLength = 1;
            var delimiterString = numbers.Substring(DelimiterDataStartPosition, DefaultDelimiterLength);

            return new string[] { delimiterString };
        }

        public bool NoDelimiterDataExistsIn(string numbers)
        {
            return !numbers.StartsWith(DelimiterLineStartMarker);
        }

        public string RemoveDelimiterDataFrom(string numbers)
        {
            if (numbers.StartsWith(DelimiterLineStartMarker))
            {
                int start = numbers.IndexOf(DelimiterLineEndMarker) + 1;
                return numbers.Substring(start);
            }

            return numbers;
        }
    }
}
