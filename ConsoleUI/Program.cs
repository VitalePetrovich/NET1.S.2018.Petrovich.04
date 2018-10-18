using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NET1.S._2018.Petrovich._04.SimpleTasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] arr = { -0.4431, 3.14 };
            string[] str = TransformToWords(new double[] { -29.0043, 0.234, -0.00001 });
            foreach (var s in str)
            {
                Console.WriteLine(s);
            }
            
            Console.ReadKey();
        }
    }
}
