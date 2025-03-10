using System;
using Ex0204.Services;

namespace Ex0204
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Brain Tech Games!");
            ScoreService scoreService = new ScoreService();
            PuzzleService puzzleService = new PuzzleService(scoreService);
            bool playAgain = true;

            while (playAgain)
            {
                Console.WriteLine("Select a puzzle type:");
                Console.WriteLine("1: Math Puzzle");
                Console.WriteLine("2: Logic Puzzle");
                Console.WriteLine("3: Exit");
                Console.Write("Enter your selection now: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        puzzleService.SolveMathPuzzle();
                        break;
                    case "2":
                        puzzleService.SolveLogicPuzzle();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        continue;
                }

                Console.WriteLine($"Your score: {scoreService.GetScore()}");
                Console.WriteLine("Do you want to play again? (y/n)");
                playAgain = Console.ReadLine()?.ToLower() == "y";
            }

            Console.WriteLine($"Final Score: {scoreService.GetScore()}");
            Console.WriteLine("Thanks for playing!");
        }
    }
}
