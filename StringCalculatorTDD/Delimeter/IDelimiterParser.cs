using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator
{
    public interface IDelimiterParser
    {
        string[] GetDelimitersFrom(string numbers);

        bool NoDelimiterDataExistsIn(string numbers);

        string RemoveDelimiterDataFrom(string numbers);
    }
}
