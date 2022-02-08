using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public abstract class Person
    {
        public string Name { get; set; }

        public void UpdateName(string newName)
        {
            Name = newName;
        }
    }

    public class Customer : Person
    {
        public int CustomerId { get; set; }
    }

    public class Supplier : Person
    {
        public int SupplierId { get; set; }
    }
}
