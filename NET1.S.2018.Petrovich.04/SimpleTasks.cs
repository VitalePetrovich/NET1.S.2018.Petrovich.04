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

            Tuple<double, int> tuple = Normalize(inputDouble);
           
            acc += Convert.ToString(tuple.Item2 + 1023, 2).PadRight(11, '0');

            double mant = tuple.Item1;

            if (mant == 1.0)
            {
                return acc.PadRight(64, '0');
            }
            else
            {
                if (mant > 1.0)
                {
                    mant -= 1.0;
                }
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

        private static Tuple<double, int> Normalize(double inputDouble)
        {
            int exp = 0;
            while (inputDouble > 2.0)
            {
                inputDouble /= 2.0;
                exp++;
            }

            while (inputDouble < 1.0)
            {
                inputDouble *= 2.0;
                exp--;
                if (exp == -1023 && inputDouble < 1.0)
                {
                    inputDouble /= 2.0;
                    return new Tuple<double, int>(inputDouble, exp);
                }
            }

            return new Tuple<double, int>(inputDouble, exp);
        }
    }

    /// <summary>
    /// Contain methods, that allow to calculate the greatest common divisor of numbers.
    /// </summary>
    public static class CalculationGDC
    {
        /// <summary>
        /// Classic Euclidean algorithm for two numbers.
        /// </summary>
        /// <param name="a">
        /// 1st number.
        /// </param>
        /// <param name="b">
        /// 2nd number.
        /// </param>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GCD(int a, int b)
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

            //if (b == 0)
            //    return Math.Abs(a);
            //return GCD(b, a % b);
        }

        /// <summary>
        /// Classic Euclidean algorithm for three numbers.
        /// </summary>
        /// <param name="a">
        /// 1st number.
        /// </param>
        /// <param name="b">
        /// 2nd number.
        /// </param>
        /// <param name="c">
        /// 3rd number.
        /// </param>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GCD(int a, int b, int c)
        {
            return GCD(GCD(a, b), c);
        }

        /// <summary>
        /// Classic Euclidean algorithm for several numbers.
        /// </summary>
        /// <param name="param">
        /// Numbers.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws, when entered less than two arguments.
        /// </exception>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GCD(params int[] param)
        {
            if (param.Length < 2)
            {
                throw new ArgumentException();
            }

            if (param.Length == 2)
            {
                return GCD(param[0], param[1]);
            }

            int[] newParam = new int[param.Length / 2 + 1];

            if (param.Length % 2 != 0)
            {
                newParam[param.Length / 2] = param[param.Length - 1];
            }
            
            for (int i = 0; i < newParam.Length - 1; i++)
            {
                newParam[i] = GCD(param[2 * i], param[2 * i + 1]);
            }

            return GCD(newParam);
        }

        /// <summary>
        /// Binary Stain algorithm for two numbers.
        /// </summary>
        /// <param name="a">
        /// 1st number.
        /// </param>
        /// <param name="b">
        /// 2nd number.
        /// </param>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GCDBin(int a, int b)
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
                    return GCDBin(a >> 1, b >> 1) << 1;
                }
                else
                {
                    return GCDBin(a >> 1, b);
                }
            }
            else
            {
                if ((b & 1) == 0)
                {
                    return GCDBin(a, b >> 1);
                }
                else
                {
                    if (a > b)
                    {
                        return GCDBin((a - b) >> 1, b);
                    }
                    else
                    {
                        return GCDBin((b - a) >> 1, a);
                    }
                }
            }
        }

        /// <summary>
        /// Binary Stain algorithm for three numbers.
        /// </summary>
        /// <param name="a">
        /// 1st number.
        /// </param>
        /// <param name="b">
        /// 2nd number.
        /// </param>
        /// <param name="c">
        /// 3rd number.
        /// </param>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GCDBin(int a, int b, int c)
        {
            return GCDBin(GCDBin(a, b), c);
        }

        /// <summary>
        /// Binary Stain algorithm for several numbers.
        /// </summary>
        /// <param name="param">
        /// Numbers.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws, when entered less than two arguments.
        /// </exception>>
        /// <returns>
        /// The greatest common divisor.
        /// </returns>
        public static int GCDBin(params int[] param)
        {
            if (param.Length < 2)
            {
                throw new ArgumentException();
            }

            if (param.Length == 2)
            {
                return GCDBin(param[0], param[1]);
            }

            int[] newParam = new int[param.Length / 2 + 1];

            if (param.Length % 2 != 0)
            {
                newParam[param.Length / 2] = param[param.Length - 1];
            }

            for (int i = 0; i < newParam.Length - 1; i++)
            {
                newParam[i] = GCDBin(param[2 * i], param[2 * i + 1]);
            }

            return GCDBin(newParam);
        }
    }
}
