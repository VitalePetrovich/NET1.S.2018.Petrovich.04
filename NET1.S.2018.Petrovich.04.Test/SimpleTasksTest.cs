using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NET1.S._2018.Petrovich._04.SimpleTasks;
using static NET1.S._2018.Petrovich._04.CalculationGDC;

namespace NET1.S._2018.Petrovich._04.Test
{
    [TestFixture]
    public class SimpleTasksTest
    {
        [TestCase(new double[] { -29.0043d, 0.234d, -54.00001d },
            ExpectedResult = new string[] { "minus two nine point zero zero four three", "zero point two three four", "minus five four point zero zero zero zero one" })]
        [TestCase(new double[] { 845.33d, 43.09d },
            ExpectedResult = new string[] { "eight four five point three three", "four three point zero nine" })]
        [TestCase(new double[] { -1.279d, 76542.73d, -3.31d },
            ExpectedResult = new string[] { "minus one point two seven nine", "seven six five four two point seven three", "minus three point three one" })]
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
        public int GDC_RightIn_RightOut(params int[] param)
            => GCD(param);

        [Test]
        public void GCD_CallWith1Argument_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => GCD(3));

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
        public int GDCBin_RightIn_RightOut(params int[] param)
            => GCDBin(param);

        [Test]
        public void GCDBin_CallWith1Argument_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => GCDBin(3));
    }
}
