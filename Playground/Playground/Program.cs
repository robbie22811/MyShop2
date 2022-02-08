using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new MyLibrary.Customer();

            MyLibrary.ICalculate function = new MyLibrary.MultiplyCalculate();
            //MyLibrary.ICalculate subtractFunction = new MyLibrary.SubtractCalculate();
            //MyLibrary.ICalculate multiplyFunction = new MyLibrary.MultiplyCalculate();

            var result = function.PerformCal(10, 10);
            Console.WriteLine(result);

            //MyLibrary.MyObject myObject = new MyLibrary.MyObject();
            //MyLibrary2.MyObject myObject2 = new MyLibrary2.MyObject(10,12);
            

            //Console.WriteLine($"{myObject.number3}");
            //Console.WriteLine($"{myObject2.number3}");
            //Console.WriteLine($"The sum is {myObject.Calculate()}");
            //Console.WriteLine($"The sum is {myObject.Calculate(10,20)}");

        }
    }


}
