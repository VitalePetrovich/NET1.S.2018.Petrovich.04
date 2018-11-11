using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NET1.S._2018.Petrovich._04.SimpleTasks;
using static NET1.S._2018.Petrovich._04.CalculationGcd;

namespace NET1.S._2018.Petrovich._04.Test
{
    using System.Security.Cryptography.X509Certificates;

    [TestFixture]
    public class SimpleTasksTest
    {
        #region Filter tests

        [TestCase(new[] { 1, 2, 3, 4 }, ExpectedResult = new[] { 3, 4 })]
        [TestCase(new[] { 3, 4, 5, 6 }, ExpectedResult = new[] { 3, 4, 5, 6})]
        [TestCase(new[] { 1, 2, 1, 2 }, ExpectedResult = new int[] {})]
        public IEnumerable<int> FilterForIntArray_ValidIn_ValidOut(IEnumerable<int> input)
            => input.Filter((a) => a > 2);

        [TestCase(new[] { 1067, 1977, 968, 1917 }, ExpectedResult = new[] { 1067, 1977, 1917 })]
        [TestCase(new[] { 7, 0, 12345, 9865 }, ExpectedResult = new[] { 7 })]
        [TestCase(new[] { 987, 654, 432, 78 }, ExpectedResult = new[] { 987, 78 })]
        public IEnumerable<int> FilterIntArrayContainDigit_ValidIn_ValidOut(IEnumerable<int> input)
            => input.Filter((a) => a.ToString().Contains(7.ToString()));

        [Test]
        public void FilterForArrayOfStrings_ValidIn_ValidOut()
        {
            var input = new[] { "1234", "3456", "5678" };
            var expected = new[] { "1234", "3456" };

            var actual = input.Filter((a) => a.Contains("4"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FilterForLinkedListOfInt_ValidIn_ValidOut()
        {
            var input = new LinkedList<int>();
            input.AddLast(1);
            input.AddLast(2);
            input.AddLast(3);
            input.AddLast(4);
            var expected = new List<int>() { 2, 3, 4 };

            var actual = input.Filter((x) => x > 1);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region delegate tests

        [TestCase(new double[] { -29.0043d, 0.234d, -54.00001d },
            ExpectedResult = new string[] { "minus two nine point zero zero four three", "zero point two three four", "minus five four point zero zero zero zero one" })]
        [TestCase(new double[] { 845.33d, 43.09d },
            ExpectedResult = new string[] { "eight four five point three three", "four three point zero nine" })]
        [TestCase(new double[] { -1.279d, 76542.73d, -3.31d },
            ExpectedResult = new string[] { "minus one point two seven nine", "seven six five four two point seven three", "minus three point three one" })]
        [TestCase(new double[] { 1.76E+32, double.NaN }, ExpectedResult = new string[] { "one point seven six exp plus three two", "NaN" })]
        public string[] TransformDoubleArrayByTransformingToWords_ValidIn_ValidOut(double[] realNumbers)
            => TransformArray(realNumbers, TransformToWord);

        [Test]
        public void TransformDoubleArray_CallWithNullReference_ThrowArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => TransformToWords(null));

        [Test]
        public void TransformDoubleArray_CallWithEmptyArray_ThrowArgumentException()
            => Assert.Throws<ArgumentException>(() => TransformToWords(new double[0]));

        #endregion

        #region previous tests
        
        [TestCase(new double[] { -29.0043d, 0.234d, -54.00001d },
            ExpectedResult = new string[] { "minus two nine point zero zero four three", "zero point two three four", "minus five four point zero zero zero zero one" })]
        [TestCase(new double[] { 845.33d, 43.09d },
            ExpectedResult = new string[] { "eight four five point three three", "four three point zero nine" })]
        [TestCase(new double[] { -1.279d, 76542.73d, -3.31d },
            ExpectedResult = new string[] { "minus one point two seven nine", "seven six five four two point seven three", "minus three point three one" })]
        [TestCase(new double[] { 1.76E+32 , double.NaN}, ExpectedResult = new string[] {"one point seven six exp plus three two", "NaN"})]
        public string[] TransformToWords_RightIput_RightOut(double[] realNumbers)
            => TransformToWords(realNumbers);

        [Test]
        public void TransformToWords_CallWithNullReference_ThrowArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => TransformToWords(null));

        [Test]
        public void TransformToWords_CallWithEmptyArray_ThrowArgumentException()
            => Assert.Throws<ArgumentException>(() => TransformToWords(new double[0]));

        [TestCase(-255.255, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN, ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(
            double.NegativeInfinity,
            ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(
            double.PositiveInfinity,
            ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        public string DoubleClassExtensionToIEEE754_RightIn_RightOut(double input)
            => input.ToIEEE754();

        [TestCase(18, 3, 9, 6, ExpectedResult = 3)]
        [TestCase(3, 15, ExpectedResult = 3)]
        [TestCase(15, 5, 45, ExpectedResult = 5)]
        [TestCase(-10, 35, 90, 55, -105, ExpectedResult = 5)]
        [TestCase(1, 213124, -54654, -123124, 65765, 44444, -7, 1234567, ExpectedResult = 1)]
        [TestCase(18, 0, ExpectedResult = 18)]
        [TestCase(12, 21, 91, 17, 0, ExpectedResult = 1)]
        [TestCase(3, 3, ExpectedResult = 3)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(-7, -7, ExpectedResult = 7)]
        public int Gcd_RightIn_RightOut(params int[] param)
            => Gcd(out _, param);

        [Test]
        public void Gcd_CallWith1Argument_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => Gcd(out _, 3));

        [TestCase(18, 3, 9, 6, ExpectedResult = 3)]
        [TestCase(3, 15, ExpectedResult = 3)]
        [TestCase(15, 5, 45, ExpectedResult = 5)]
        [TestCase(-10, 35, 90, 55, -105, ExpectedResult = 5)]
        [TestCase(1, 213124, -54654, -123124, 65765, 44444, -7, 1234567, ExpectedResult = 1)]
        [TestCase(18, 0, ExpectedResult = 18)]
        [TestCase(12, 21, 91, 17, 0, ExpectedResult = 1)]
        [TestCase(3, 3, ExpectedResult = 3)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(-7, -7, ExpectedResult = 7)]
        public int GcdBin_RightIn_RightOut(params int[] param)
            => GcdBin(out _, param);

        [Test]
        public void GcdBin_CallWith1Argument_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => GcdBin(out _, 3));

        #endregion
    }
}
