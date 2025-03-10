using System;

namespace Ex0204.Models
{
    public class LogicPuzzle
    {
        private static readonly (string question, string[] options, int correctInt)[] puzzles =
        {
            ("what comes next: 2, 4, 6, ?", new string[] { "7", "8", "10" }, 2),
            ("Which shape has three sides?", new string[] { "Circle", "Square", "Triangle" }, 3),
            ("If a rooster lays an egg on a roof, where does it roll?", new string[] { "Left", "Right", "Nowhere" }, 3)
        };

        private static readonly Random rand = new Random();

        public (string question, string[] options, int correctInt) GetRandom()
        {
            int index = rand.Next(puzzles.Length);
            return puzzles[index];
        }
    }
}