using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void MultipleLegs(PetStruct petStruct, PetClass petClass)
        {
            petStruct.Legs = petStruct.Legs * 2;
            petClass.Legs = petClass.Legs * 2;

            Console.WriteLine("Internal Method - A " + petStruct.Type + " has " + petStruct.Legs + " legs");
            Console.WriteLine("Internal Method - A " + petClass.Type + " has " + petClass.Legs + " legs");
        }

        static void Main(string[] args)
        {
            int[] intArray = new int[5];
            string[] stringArray = new string[5];

            int[] populatedIntArray = new int[] { 0, 1, 2, 3, 4, 5 };
            string[] populatedStringArray = new string[] { "One", "Two", "Three", "Four" };

            intArray[0] = 5;
            intArray[1] = 15;

            int firstValue = intArray[0];

            int[,] multiInt = new int[2,3];
            multiInt[0, 0] = 0;
            multiInt[0, 1] = 1;
            multiInt[0, 2] = 2;

            int[,] multiPopulatedInt = { { 1, 2, 3 }, { 5, 6, 7 } };
            int firstMultiValue = multiPopulatedInt[0, 0];
            int secondMultiValue = multiPopulatedInt[1, 2];

            List<string> listOfStrings = new List<string>();
            listOfStrings.Add("first string");
            listOfStrings.Add("second string");
            listOfStrings.Insert(0, "top string");
            listOfStrings.Remove("first string");
            listOfStrings.RemoveAt(0);
            listOfStrings.Sort();

            var firstStringis = listOfStrings[0];
            Console.WriteLine(firstStringis);

            Dictionary<string, string> names = new Dictionary<string, string>();
            names.Add("James", "Bond");
            names.Add("Money", "Penny");

            Console.WriteLine("Name is " + names["James"]);
            names.Remove("James");
            

            PetStruct dog = new PetStruct();
            dog.Type = PetType.Dog;
            dog.HasFur = true;
            dog.Legs = 4;

            PetClass duck = new PetClass();
            duck.Type = PetType.Duck;
            duck.HasFur = false;
            duck.Legs = 2;

            Console.WriteLine("A " + dog.Type + " has " + dog.Legs + " legs");
            Console.WriteLine("A " + duck.Type + " has " + duck.Legs + " legs");

            MultipleLegs(dog, duck);

            Console.WriteLine("A " + dog.Type + " has " + dog.Legs + " legs");
            Console.WriteLine("A " + duck.Type + " has " + duck.Legs + " legs");

            List<PetClass> pets = new List<PetClass>();
            pets.Add(new PetClass { HasFur = false, Legs = 2, Name = "Donald", Type = PetType.Duck });
            pets.Add(new PetClass { HasFur = true, Legs = 4, Name = "Pluto", Type = PetType.Dog });
            pets.Add(new PetClass { HasFur = true, Legs = 4, Name = "Goofy", Type = PetType.Dog });

            List<PetClass> results = (from p in pets
                                where p.HasFur == true
                                select p).ToList();

            results = pets.Where(p => p.Type == PetType.Dog).ToList();

            Console.WriteLine("found " + results.Count + " Dogs");
        }

    }

    class PetClass
    {
        public int Legs;
        public PetType Type;
        public string Name;
        public bool HasFur;
    }

    struct PetStruct
    {
        public int Legs;
        public PetType Type;
        public string Name;
        public bool HasFur;
    }

    enum PetType
    {
        Dog,
        Duck
    }
}
