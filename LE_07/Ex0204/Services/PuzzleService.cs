using Ex0204.Models;
using Ex0204.Utilities;
using System;

namespace Ex0204.Services
{
    public class PuzzleService
    {
        private readonly MathPuzzle _mathPuzzle;
        private readonly LogicPuzzle _logicPuzzle;
        private readonly ScoreService _scoreService;

        public PuzzleService(ScoreService scoreService)
        {
            _mathPuzzle = new MathPuzzle();
            _logicPuzzle = new LogicPuzzle();
            _scoreService = scoreService;
        }

        public void SolveMathPuzzle()
        {
            var (mathQuestion, mathAnswer) = _mathPuzzle.Generate();
            Console.WriteLine($"Solve: {mathQuestion}");
            Console.WriteLine("You have 30 seconds to answer!");
            var mathUserAnswer = TimedInput.ReadInputWithTimeout(30);

            if (mathUserAnswer == null)
            {
                Console.WriteLine("Time's up! You didn't answer in time.");
                _scoreService.DeductPoints(1);
                return;
            }
            if (int.TryParse(mathUserAnswer, out int userAnswer) && userAnswer == mathAnswer)
            {
                _scoreService.AddPoints(2);
                Console.WriteLine("Correct!");
            }
            else
            {
                _scoreService.DeductPoints(1);
                Console.WriteLine("Incorrect!");
            }
        }


        public void SolveLogicPuzzle()
        {
            var (logicQuestion, options, correctIndex) = _logicPuzzle.GetRandom();
            Console.WriteLine(logicQuestion);
            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{i + 1}. {options[i]}");

            Console.WriteLine("You have 30 seconds to answer!");
            var logicUserAnswer = TimedInput.ReadInputWithTimeout(30);

            if (logicUserAnswer == null)
            {
                Console.WriteLine("Time's up! You didn't answer in time.");
                _scoreService.DeductPoints(1);
                return;
            }

            if (int.TryParse(logicUserAnswer, out int choiceNum) && choiceNum == correctIndex)
            {
                _scoreService.AddPoints(2);
                Console.WriteLine("Correct!");
            }
            else
            {
                _scoreService.DeductPoints(1);
                Console.WriteLine("Incorrect");
            }
        }
    }
} 