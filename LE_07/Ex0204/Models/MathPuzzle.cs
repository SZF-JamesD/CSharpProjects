using System;

namespace Ex0204.Models
{
    public class MathPuzzle
    {
        private static readonly Random rand = new Random();

        public (string question, int answer) Generate()
        {
            int num1 = rand.Next(1, 10);
            int num2 = rand.Next(1, 10);
            int operation = rand.Next(4);

            switch (operation) 
            {
                case 0:
                    return ($"{num1} + {num2} = ", num1 + num2);
                case 1:
                    return($"{num1} - {num2} = ", num1 - num2);
                case 2:
                    return ($"{num1} * {num2} = ", num1 * num2);
                case 3:
                    return(num2 != 0 ? $"{num1} / {num2} = " : $"{num1} / 1 = ", num1 / (num2 != 0 ? num2 : 1));
                default:
                    return ("Error", 0);
            };
        }
    }
}