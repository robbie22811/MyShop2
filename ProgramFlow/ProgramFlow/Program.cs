using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramFlow
{
    class Program
    {
        static int GetGuess()
        {
            int result = 0;
            Console.Write("Please guess a number: ");
            string answer = Console.ReadLine();
            Int32.TryParse(answer, out result);
            return result;
        }

        static void Main(string[] args)
        {
            const int ourNumber = 1234;
            int guessNumber;
            const int numOfGuesses = 10;
            
            for(int guessCount = numOfGuesses; guessCount > 0; guessCount--)
            {
                guessNumber = GetGuess();

                if (guessNumber == ourNumber)
                {
                    Console.WriteLine("Correct, you are the best number guesser!");
                    break;
                }
                else if (guessNumber <= 10)
                    Console.WriteLine("WAAAAYYYY Too Low!! You have {0} guesses left!", guessCount - 1);
                else if (guessNumber <= 20)
                    Console.WriteLine("c'mon, think bigger! You have {0} guesses left!", guessCount - 1);
                else if (guessNumber > 9999)
                    Console.WriteLine("Way too big! You have {0} guesses left!", guessCount - 1);
                else
                {
                    if (guessNumber > ourNumber)
                    {
                        Console.WriteLine("Too High!  You have {0} guesses left!", guessCount - 1);
                    }
                    else
                    {
                        Console.WriteLine("Too Low!  You have {0} guesses left!", guessCount - 1);
                    }
                }
                
            }

            //while(ourNumber != guessNumber)
            //{                
            //    if (guessNumber > ourNumber)
            //    {
            //        Console.WriteLine("Too High, guess again!");
            //        guessNumber = GetGuess();
            //    }
            //    else if (guessNumber < ourNumber)
            //    {
            //        Console.WriteLine("Too Low, guess again!");
            //        guessNumber = GetGuess();
            //    }                
            //}

            
        }
    }
}
