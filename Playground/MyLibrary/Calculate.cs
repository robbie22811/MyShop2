using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class AddCalculate : ICalculate
    {
        public int PerformCal(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    public class SubtractCalculate : ICalculate
    {
        public int PerformCal(int num1, int num2)
        {
            return num1 - num2;
        }
    }

    public class MultiplyCalculate : ICalculate
    {
        public int PerformCal(int num1, int num2)
        {
            return num1 * num2;
        }
    }

}
