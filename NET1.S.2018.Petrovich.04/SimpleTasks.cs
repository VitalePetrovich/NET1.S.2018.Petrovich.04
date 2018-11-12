using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
                stringsArray[i] = TransformToWord(realNumbers[i]);
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

        /// <summary>
        /// Transform double array into string array.
        /// </summary>
        /// <param name="sourceArray">
        /// Array of doubles.
        /// </param>
        /// <param name="transformer">
        /// Delegate-transformer. Provide logic of transform.
        /// </param>
        /// /// <exception cref="ArgumentNullException">
        /// Throws, when method is call with null array or delegate-transformer reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws, when method is call with array containing no elements.
        /// </exception>
        /// <returns>
        /// New string array.
        /// </returns>
        public static TResult[] TransformArray<TSource, TResult>(TSource[] sourceArray, Func<TSource, TResult> transformer)
        {
            if (ReferenceEquals(sourceArray, null))
                throw new ArgumentNullException(nameof(sourceArray));

            if (sourceArray.Length == 0)
                throw new ArgumentException($"{nameof(sourceArray)} doesn't contain elements.");
            
            if (ReferenceEquals(transformer, null))
                throw new ArgumentNullException(nameof(transformer));

            TResult[] resultsArray = new TResult[sourceArray.Length];

            for (int i = 0; i < sourceArray.Length; i++)
            {
                resultsArray[i] = transformer(sourceArray[i]);
            }

            return resultsArray;
        }
        
        /// <summary>
        /// Transform double value to string representation.
        /// </summary>
        /// <param name="doubleValue">Value for transforming.</param>
        /// <returns>String in which each digit of the original number is represented by the word.</returns>
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

            return stringValue.TrimEnd();
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
        public delegate int CalcGcdDelegat(int x, int y);

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

            int result = GcdCore(GcdEuclid, firstNumber, secondNumber);

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

            int result = GcdCore(GcdEuclid, firstNumber, secondNumber, thirdNumber);

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

            int result = GcdCore(GcdEuclid, param);

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

            int result = GcdCore(GcdStein, firstNumber, secondNumber);

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

            int result = GcdCore(GcdStein, firstNumber, secondNumber, thirdNumber);

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

            int result = GcdCore(GcdStein, param);

            elapsedMilliseconds = timer.ElapsedMilliseconds;
            timer.Stop();

            return result;
        }

        private static int GcdCore(CalcGcdDelegat calc, int firstNumber, int secondNumber)
            => calc.Invoke(firstNumber, secondNumber);
      
        private static int GcdCore(CalcGcdDelegat calc, int firstNumber, int secondNumber, int thirdNumber)
            => calc.Invoke(calc.Invoke(firstNumber, secondNumber), thirdNumber);
      
        private static int GcdCore(CalcGcdDelegat calc, params int[] param)
        {
            if (param.Length < 2)
            {
                throw new ArgumentException();
            }

            if (param.Length == 2)
            {
                return calc.Invoke(param[0], param[1]);
            }

            int[] newParam = new int[param.Length / 2 + 1];

            if (param.Length % 2 != 0)
            {
                newParam[param.Length / 2] = param[param.Length - 1];
            }

            for (int i = 0; i < newParam.Length - 1; i++)
            {
                newParam[i] = calc.Invoke(param[2 * i], param[2 * i + 1]);
            }

            return GcdCore(calc, newParam);
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

    public static class EnumerableExtention
    {

        /// <summary>
        /// Interface which must be implemented by class-condition. 
        /// </summary>
        /// <typeparam name="TSource">Type of processed elements.</typeparam>
        public interface ICondition<TSource>
        {
            bool Check(TSource item);
        }

        /// <summary>
        /// Interface which must be implemented by class-transrotmer. 
        /// </summary>
        /// <typeparam name="TSource">Type of processed elements.</typeparam>
        /// <typeparam name="TResult">Type of resulted elements.</typeparam>
        public interface ITransformer<TSource, TResult>
        {
            TResult Transform(TSource source);
        }

        /// <summary>
        /// Filter for enumerable collections.
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements.</typeparam>
        /// <param name="input">Input collection.</param>
        /// <param name="condition">Predicate-condition.</param>
        /// <exception cref="ArgumentNullException">Throws, if input or condition is null.</exception>
        /// <returns>Filtered collection.</returns>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> input, Predicate<TSource> condition)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            return FilterCore(input, condition);

            IEnumerable<TSource> FilterCore(IEnumerable<TSource> source, Predicate<TSource> cond)
            {
                foreach (var item in source)
                {
                    if (cond(item))
                        yield return item;
                }
            }
        }

        /// <summary>
        /// Filter for enumerable collections.
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements.</typeparam>
        /// <param name="input">Input collection.</param>
        /// <param name="cond">Class-condition.</param>
        /// <exception cref="ArgumentNullException">Throws, if input or condition is null.</exception>
        /// <returns>Filtered collection.</returns>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> input, ICondition<TSource> cond)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (cond == null)
                throw new ArgumentNullException(nameof(cond));

            return input.Filter(cond.Check);
        }

        /// <summary>
        /// Transformer for enumerable collections.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection elements.</typeparam>
        /// <typeparam name="TResult">Type of resulted collection elements.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="transformer">Delegate-transformer. Provide logic of transform.</param>
        /// <exception cref="ArgumentNullException">
        /// Throws, when method is call with null array or delegate-transformer reference.
        /// </exception>
        /// <returns>Transformed collection.</returns>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> transformer)
        {
            if (ReferenceEquals(source, null))
                throw new ArgumentNullException(nameof(source));
            
            if (ReferenceEquals(transformer, null))
                throw new ArgumentNullException(nameof(transformer));

            return Transform(source, transformer);

            IEnumerable<TResult> Transform(IEnumerable<TSource> src, Func<TSource, TResult> transf)
            {
                foreach (var item in src)
                {
                    yield return transf(item);
                }
            }
        }

        /// <summary>
        /// Transformer for enumerable collections.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection elements.</typeparam>
        /// <typeparam name="TResult">Type of resulted collection elements.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="transformer">Class-transformer. Provide logic of transform.</param>
        /// <returns>Transformed collection.</returns>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, ITransformer<TSource, TResult> transformer)
        {
            if (ReferenceEquals(source, null))
                throw new ArgumentNullException(nameof(source));

            if (ReferenceEquals(transformer, null))
                throw new ArgumentNullException(nameof(transformer));

            return source.Transform(transformer.Transform);
        }
    }
}
