using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary2
{
    public class MyObject
    {
        public MyObject()
        {
            _number1 = 10;
            _number2 = 5;
        }

        public MyObject(int num1, int num2)
        {
            _number1 = num1;
            _number2 = num2;
        }

        int _number1 = 0;
        int _number2 = 0;

        public int number1
        {
            get { return _number1; }
            set { _number1 = value; }
        }

        public int number2
        {
            get { return _number2; }
            set { _number2 = value; }
        }

        public int number3
        {
            get { return _number1 + _number2; }
        }

        public int Calculate(int number1, int number2)
        {
            return number1 + number2;
        }

        public int Calculate()
        {
            return _number1 + _number2;
        }

        public string MyMethod()
        {
            return "some value";
        }
    }
}
