using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NET1.S._2018.Petrovich._04
{
    /// <summary>
    /// Contain TransformToWords method.
    /// </summary>
    public static class SimpleTasks
    {
        /// <summary>
        /// Transform array of doubles into array of strings.
        /// Each digit and symbol represent by text equivalent.
        /// </summary>
        /// <param name="realNumbers">
        /// Array of doubles for transform.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws, when method is call with null array reference.
        /// </exception>>
        /// <exception cref="ArgumentException">
        /// Throws, when method is call with array containing no elements.
        /// </exception>>
        /// <returns>
        /// Resulted array fo strings.
        /// </returns>
        public static string[] TransformToWords(double[] realNumbers)
        {
            if (realNumbers == null)
            {
                throw new ArgumentNullException(nameof(realNumbers));
            }

            if (realNumbers.Length == 0)
            {
                throw new ArgumentException($"Array {nameof(realNumbers)} does not contain elements.");
            }

            string[] TransformToWordsAlgo(double[] arrayOfDouble)
            {
                string[] stringsArray = new string[arrayOfDouble.Length];

                for (int i = 0; i < arrayOfDouble.Length; i++)
                {
                    stringsArray[i] = TransformToWord(arrayOfDouble[i]).TrimEnd();
                }

                return stringsArray;
            }

            return TransformToWordsAlgo(realNumbers);
        }

        private static string TransformToWord(double num)
        {
            StringBuilder accumulator = new StringBuilder();

            foreach (var ch in num.ToString("R"))
            {
                switch (ch)
                {
                    case '-':
                        accumulator.Append("minus ");
                        break;
                    case ',':
                        accumulator.Append("point ");
                        break;
                    case '0':
                        accumulator.Append("zero ");
                        break;
                    case '1':
                        accumulator.Append("one ");
                        break;
                    case '2':
                        accumulator.Append("two ");
                        break;
                    case '3':
                        accumulator.Append("three ");
                        break;
                    case '4':
                        accumulator.Append("four ");
                        break;
                    case '5':
                        accumulator.Append("five ");
                        break;
                    case '6':
                        accumulator.Append("six ");
                        break;
                    case '7':
                        accumulator.Append("seven ");
                        break;
                    case '8':
                        accumulator.Append("eight ");
                        break;
                    case '9':
                        accumulator.Append("nine ");
                        break;
                    case 'E':
                        accumulator.Append("exp ");
                        break;
                }
            }

            return new string(accumulator.ToString().ToCharArray());
        }

    }
    /// <summary>
    /// Extension class for type double.
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// Extension method for type double, which represent number in IEEE 754 format.
        /// </summary>
        /// <param name="inputDouble">
        /// Number.
        /// </param>
        /// <returns>
        /// String representation.
        /// </returns>
        public static string ToIEEE754(this double inputDouble)
        {
            switch (inputDouble)
            {
                case 0.0:
                    if ((double.PositiveInfinity/inputDouble)*Double.PositiveInfinity == double.PositiveInfinity)
                        return $"0000000000000000000000000000000000000000000000000000000000000000";
                    else
                        return $"1000000000000000000000000000000000000000000000000000000000000000";
                case Double.NaN:
                    return $"1111111111111000000000000000000000000000000000000000000000000000";
                case Double.NegativeInfinity:
                    return $"1111111111110000000000000000000000000000000000000000000000000000";
                case Double.PositiveInfinity:
                    return $"0111111111110000000000000000000000000000000000000000000000000000";
            }

            int exp = 0;
            string acc = string.Empty;

            if (inputDouble < 0.0)
            {
                acc += '1';
                inputDouble *= -1.0;
            }
            else
            {
                acc += '0';
            }

            double mant = inputDouble;

            while (mant > 2.0)
            {
                mant /= 2.0;
                exp++;
            }

            while (mant < 1.0)
            {
                mant *= 2.0;
                exp--;
            }

            acc += Convert.ToString(exp + 1023, 2);

            if (mant == 1.0)
            {
                return acc.PadRight(64, '0');
            }
            else
            {
                mant -= 1.0;
            }

            int count = 0;
            while (mant != 0.0 && count++ < 52)
            {
                mant *= 2.0;
                acc += ((int)mant).ToString();
                if (mant >= 1.0)
                {
                    mant -= 1.0;
                }
            }

            return acc.PadRight(64, '0');
        }
    }
}
