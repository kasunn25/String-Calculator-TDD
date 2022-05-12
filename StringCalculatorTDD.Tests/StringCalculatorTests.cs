using System;
using System.Linq;
using StringCalculator;
using StringCalculatorTDD;
using Xunit;

namespace StringCalculatorUnitTests
{
    public class StringCalculatorTests
    {
        private StringCalculatorTDD.StringCalculator _stringCalculator
        {
            get
            {
                return new StringCalculatorTDD.StringCalculator(new DelimiterParser());
            }
        }

        /// <summary>
        /// General test cases
        /// </summary>
        /// 
        [Fact]
        public void Add_WhenStringHasNegativeNumber_ThrowsArgumentException()
        {
            const int TestValue = -2;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => _stringCalculator.Add(TestValue.ToString()));
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Add_WhenPassedNullNumbersString_ReturnsZero()
        {
            var result = _stringCalculator.Add(null);

            const int ExpectedResult = 0;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedWhitespaceFilledNumbersString_ReturnsZero()
        {
            var result = _stringCalculator.Add(new String(' ', 10));

            const int ExpectedResult = 0;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingOneNumber_ReturnsProvidedNumber()
        {
            const int TestValue = 6;
            var result = _stringCalculator.Add(TestValue.ToString());

            Assert.Equal(TestValue, result);
        }

        /// <summary>
        /// Step 1
        /// “” should return 0
        /// “1” should return 1
        /// “1,2” should return 3
        /// </summary>
        /// 
        [Fact]
        public void Add_WhenPassedEmtpyNumbersString_ReturnsZero()
        {
            var result = _stringCalculator.Add("");

            const int ExpectedResult = 0;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedOneNumbersString_ReturnsOne()
        {
            const int TestValue = 1;
            var result = _stringCalculator.Add(TestValue.ToString());

            const int ExpectedResult = 1;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingTwoNumbers_ReturnsSumOfBothNumbers()
        {
            var result = _stringCalculator.Add("1,2");

            const int ExpectedResult = 3;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingMultipleNumbers_ReturnsSumOfAllNumbers()
        {
            Assert.Equal(3 + 2 + 6 + 10 + 100, _stringCalculator.Add("3,2,6,10,100"));
            Assert.Equal(3 + 2 + 6 + 10, _stringCalculator.Add("3,2,6,10"));
            Assert.Equal(1 + 2 + 500 + 230 + 965, _stringCalculator.Add("1,2,500,230,965"));
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingMultipleNumbersWithDelimeter_ReturnsSumOfAllNumbers()
        {
            Assert.Equal(3 + 2 + 6 + 10, _stringCalculator.Add("3\n2,6\n10"));
            Assert.Equal(3 + 2 + 6 + 10 + 100, _stringCalculator.Add("3\n2,6\n10,100"));
        }


        /// <summary>
        /// Step 2
        /// Allow the Add method to handle an unknown amount of numbers.
        /// </summary>
        ///
        [Fact]
        public void Add_WhenPassedNumbersStringContainingMoreThanTwoNumbers_ReturnsSumOfAllNumbers()
        {
            var testNumbers = Enumerable.Range(0, 100);
            var expectedResult = testNumbers.Sum();

            const string Delimiter = ",";
            var result = _stringCalculator.Add(string.Join(Delimiter, testNumbers));

            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Step 3
        /// Calling Add with a negative number will throw an exception “negatives not allowed” - and the negative that was passed.
        /// If there are multiple negatives, show all of them in the exception message
        /// </summary>
        ///
        [Fact]
        public void Add_WhenPassedNumbersStringContainingANegativeNumber_ThrowsArgumentException()
        {
            var negativeNumber = -10;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => _stringCalculator.Add($"//|\n{negativeNumber}"));
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingANegativeNumber_ThrowsArgumentExceptionWithNegativeValueInMessage()
        {
            var negativeNumber = -14;

            try
            {
                var result = _stringCalculator.Add($"//|\n{negativeNumber}");

            }
            catch (ArgumentException argumentException)
            {
                const string ExceptionMainMessage = "NEGATIVES NOT ALLOWED";

                Assert.StartsWith(ExceptionMainMessage, argumentException.Message.ToUpper());
                Assert.Contains(negativeNumber.ToString(), argumentException.Message.ToUpper());
            }
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingANegativeNumbers_ThrowsArgumentExceptionWithNegativeValuesInMessage()
        {
            var negativeNumbers = "-12|-18|-17";

            try
            {
                var result = _stringCalculator.Add($"//|\n{negativeNumbers}");

            }
            catch (ArgumentException argumentException)
            {
                const string ExceptionMainMessage = "NEGATIVES NOT ALLOWED";

                Assert.StartsWith(ExceptionMainMessage, argumentException.Message.ToUpper());
                Assert.Contains(negativeNumbers.Replace("|", ","), argumentException.Message.ToUpper());
            }
        }

        /// <summary>
        /// Step 4
        /// Numbers bigger than 1000 should be ignored, CORRECTION : for example “1001,2” should return 2
        /// </summary>
        ///
        [Fact]
        public void Add_WhenPassedNumbersStringBiggerThanThousand_ReturnOne()
        {
            var result = _stringCalculator.Add("1,1001");

            const int ExpectedResult = 1;
            Assert.Equal(ExpectedResult, result);
        }


        /// <summary>
        /// Step 5
        /// Support different delimiters Delimiters can be of any length with the following format:
        /// </summary>
        ///
        [Fact]
        public void Add_WhenPassedNumbersStringContainingOneNumbersWithVariableLengthDelimiter_ReturnSum()
        {
            var result = _stringCalculator.Add("//\n20");

            const int ExpectedResult = 20;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingMultplieNumbersWithVariableLengthDelimiter_ReturnsSumOfAllNumbers()
        {
            var result = _stringCalculator.Add("//[***]\n10***90***15");

            const int ExpectedResult = 115;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingMultplieNumbersWithMultipleSingleLengthDelimiters_ReturnsSumOfAllNumbers()
        {
            var result = _stringCalculator.Add("//[*][%]\n1*2%3");

            const int ExpectedResult = 6;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingMultplieNumbersWithMultipleVariableLengthDelimiters_ReturnsSumOfAllNumbers()
        {
            var result = _stringCalculator.Add("//[**][%%%%]\n130**260%%%%20**10");

            const int ExpectedResult = 420;
            Assert.Equal(ExpectedResult, result);
        }

        /// <summary>
        /// Step 6
        /// Allow multiple delimiters like this:
        /// “//[*][%]//1*2%3”
        /// </summary>
        ///
        [Fact]
        public void Add_WhenPassedNumbersStringContainingMultipleNumbersWithMultipleDelimeters_ReturnsSumOfAllNumbers()
        {
            var result = _stringCalculator.Add("//[*][%]\n1*2%3");

            const int ExpectedResult = 6;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringSpecifyingDelimiterAndSingleNumber_ReturnsNumberValue()
        {
            var result = _stringCalculator.Add("//;\n10");

            const int ExpectedResult = 10;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringSpecifyingDelimiterAndMultipleNumbers_ReturnsSumOfNumbers()
        {
            var result = _stringCalculator.Add("//|\n10|10|5");

            const int ExpectedResult = 25;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingNumbersLargerThanAThousand_ReturnSumOfNumbersLessThanAThousand()
        {
            var result = _stringCalculator.Add("//|\n1050|10|5");

            const int ExpectedResult = 15;
            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void Add_WhenPassedNumbersStringContainingNumbersEqualToAThousand_ReturnSumOfAllNumbers()
        {
            var result = _stringCalculator.Add("//|\n1000|10|5");

            const int ExpectedResult = 1015;
            Assert.Equal(ExpectedResult, result);
        }
    }
}
