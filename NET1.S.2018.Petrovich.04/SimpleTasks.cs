using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

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
        /// Resulted array of strings.
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
            
            string[] stringsArray = new string[realNumbers.Length];

            for (int i = 0; i < realNumbers.Length; i++)
            {
                stringsArray[i] = TransformToWord(realNumbers[i]).TrimEnd();
            }

            return stringsArray;
            
        }

        /// <summary>
        /// Transform array of doubles into array of strings.
        /// Each double represent in IEEE754 format.
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
        /// Resulted array of strings.
        /// </returns>
        public static string[] TransformToIEEE754Format(double[] realNumbers)
        {
            if (realNumbers == null)
            {
                throw new ArgumentNullException(nameof(realNumbers));
            }

            if (realNumbers.Length == 0)
            {
                throw new ArgumentException($"Array {nameof(realNumbers)} does not contain elements.");
            }
            
            string[] stringsArray = new string[realNumbers.Length];

            for (int i = 0; i < realNumbers.Length; i++)
            {
                stringsArray[i] = realNumbers[i].ToIEEE754();
            }

            return stringsArray;

        }

        public delegate string TransformDoubleToString(double number);

        public static string[] TransformDoubleArray(double[] realNumbers, TransformDoubleToString transformer)
        {
            if (ReferenceEquals(realNumbers, null))
            {
                throw new ArgumentNullException(nameof(realNumbers));
            }

            if (realNumbers.Length == 0)
            {
                throw new ArgumentException($"{nameof(realNumbers)} doesn't contain elements.");
            }

            string[] stringsArray = new string[realNumbers.Length];

            for (int i = 0; i < realNumbers.Length; i++)
            {
                stringsArray[i] = transformer.Invoke(realNumbers[i]).TrimEnd();
            }

            return stringsArray;
        }
        
        public static string TransformToWord(double doubleValue)
        {
            if (double.IsPositiveInfinity(doubleValue))
            {
                return "PositiveInfinity";
            }

            if (double.IsNegativeInfinity(doubleValue))
            {
                return "NegativeInfinity";
            }

            if (double.IsNaN(doubleValue))
            {
                return "NaN";
            }

            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                ["-"] = "minus ",
                ["+"] = "plus ",
                ["E"] = "exp ",
                [","] = "point ",
                ["."] = "point ",
                ["0"] = "zero ",
                ["1"] = "one ",
                ["2"] = "two ",
                ["3"] = "three ",
                ["4"] = "four ",
                ["5"] = "five ",
                ["6"] = "six ",
                ["7"] = "seven ",
                ["8"] = "eight ",
                ["9"] = "nine "
            };

            string stringValue = doubleValue.ToString();

            foreach (var symbol in dict)
            {
                stringValue = stringValue.Replace(symbol.Key, symbol.Value);
            }

            return stringValue;
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
            var inputDoubleAsLong = new DoubleAsLong(inputDouble);
            
            if (inputDoubleAsLong.longValue >= 0)
                return Convert.ToString(inputDoubleAsLong.longValue, 2).PadLeft(64, '0');

            return Convert.ToString(inputDoubleAsLong.longValue, 2);
        }

        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DoubleAsLong
        {
            [FieldOffset(0)]
            private readonly double doubleValue;

            [FieldOffset(0)]
            public readonly long longValue;
            
            public DoubleAsLong(double value)
            {
                this.longValue = 0;
                this.doubleValue = value;
            }
        }
    }
        
    /// <summary>
    /// Contain methods, that allow to calculate the greatest common divisor of numbers.
    /// </summary>
    public static class CalculationGcd
    {
        /// <summary>
        /// Classic Euclidean algorithm for two numbers.
        /// </summary>
        /// <param name="firstNumber">
        /// 1st number.
        /// </param>
        /// <param name="secondNumber">
        /// 2nd number.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// Method execution time.
        /// </param>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int Gcd(int firstNumber, int secondNumber, out long elapsedMilliseconds)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int result = GcdEuclid(firstNumber, secondNumber);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        /// <summary>
        /// Classic Euclidean algorithm for three numbers.
        /// </summary>
        /// <param name="firstNumber">
        /// 1st number.
        /// </param>
        /// <param name="secondNumber">
        /// 2nd number.
        /// </param>
        /// <param name="thirdNumber">
        /// 3rd number.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// Method execution time.
        /// </param>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int Gcd(int firstNumber, int secondNumber, int thirdNumber, out long elapsedMilliseconds)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int result = GcdEuclid(GcdEuclid(firstNumber, secondNumber), thirdNumber);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        /// <summary>
        /// Classic Euclidean algorithm for several numbers.
        /// </summary>
        /// <param name="param">
        /// Numbers.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// Method execution time.
        /// </param>>
        /// <exception cref="ArgumentException">
        /// Throws, when entered less than two arguments.
        /// </exception>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int Gcd(out long elapsedMilliseconds, params int[] param)
        {
            Stopwatch timer = Stopwatch.StartNew();

            if (param.Length < 2)
            {
                elapsedMilliseconds = timer.ElapsedMilliseconds;
                timer.Stop();

                throw new ArgumentException();
            }

            if (param.Length == 2)
            {
                timer.Stop();

                return Gcd(param[0], param[1], out elapsedMilliseconds);
            }

            int[] newParam = new int[param.Length / 2 + 1];

            if (param.Length % 2 != 0)
            {
                newParam[param.Length / 2] = param[param.Length - 1];
            }
            
            for (int i = 0; i < newParam.Length - 1; i++)
            {
                newParam[i] = GcdEuclid(param[2 * i], param[2 * i + 1]);
            }

            int result = Gcd(out _, newParam);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        /// <summary>
        /// Binary Stain algorithm for two numbers.
        /// </summary>
        /// <param name="firstNumber">
        /// 1st number.
        /// </param>
        /// <param name="secondNumber">
        /// 2nd number.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// Method execution time.
        /// </param>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GcdBin(int firstNumber, int secondNumber, out long elapsedMilliseconds)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int result = GcdStein(firstNumber, secondNumber);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        /// <summary>
        /// Binary Stain algorithm for three numbers.
        /// </summary>
        /// <param name="firstNumber">
        /// 1st number.
        /// </param>
        /// <param name="secondNumber">
        /// 2nd number.
        /// </param>
        /// <param name="thirdNumber">
        /// 3rd number.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// Method execution time.
        /// </param>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GcdBin(int firstNumber, int secondNumber, int thirdNumber, out long elapsedMilliseconds)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int result = GcdStein(GcdStein(firstNumber, secondNumber), thirdNumber);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        /// <summary>
        /// Binary Stain algorithm for several numbers.
        /// </summary>
        /// <param name="param">
        /// Numbers.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// Method execution time.
        /// </param>>
        /// <exception cref="ArgumentException">
        /// Throws, when entered less than two arguments.
        /// </exception>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GcdBin(out long elapsedMilliseconds, params int[] param)
        {
            Stopwatch timer = Stopwatch.StartNew();

            if (param.Length < 2)
            {
                elapsedMilliseconds = timer.ElapsedMilliseconds;
                timer.Stop();

                throw new ArgumentException();
            }

            if (param.Length == 2)
            {
                timer.Stop();

                return GcdBin(param[0], param[1], out elapsedMilliseconds);
            }

            int[] newParam = new int[param.Length / 2 + 1];

            if (param.Length % 2 != 0)
            {
                newParam[param.Length / 2] = param[param.Length - 1];
            }

            for (int i = 0; i < newParam.Length - 1; i++)
            {
                newParam[i] = GcdStein(param[2 * i], param[2 * i + 1]);
            }

            int result = GcdBin(out _, newParam);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        private static int GcdEuclid(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }

            return a;

            //if (secondNumber == 0)
            //    return Math.Abs(firstNumber);
            //return Gcd(secondNumber, firstNumber % secondNumber);
        }

        private static int GcdStein(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == b)
            {
                return a;
            }

            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            if ((a & 1) == 0)
            {
                if ((b & 1) == 0)
                {
                    return GcdStein(a >> 1, b >> 1) << 1;
                }
                else
                {
                    return GcdStein(a >> 1, b);
                }
            }
            else
            {
                if ((b & 1) == 0)
                {
                    return GcdStein(a, b >> 1);
                }
                else
                {
                    if (a > b)
                    {
                        return GcdStein((a - b) >> 1, b);
                    }
                    else
                    {
                        return GcdStein((b - a) >> 1, a);
                    }
                }
            }
        }
    }
}
