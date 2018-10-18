using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NET1.S._2018.Petrovich._04.SimpleTasks;

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
    }
}
