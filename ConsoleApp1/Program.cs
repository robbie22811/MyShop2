using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>() { 1,2,3,4};
            numbers.Add(1);
            numbers.AddRange(new int[3] {5,6,7});
            foreach (var number in numbers) 
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("Index of 1: " + numbers.LastIndexOf(1));

            Console.WriteLine("Count of numbers " );
        }
    }
}