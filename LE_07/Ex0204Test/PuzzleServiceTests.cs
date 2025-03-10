using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ex0204.Services;
using Ex0204.Models;
using Ex0204.Utilities;
using System;
using System.IO;
using System.Reflection;

namespace Ex0204Test
{
    [TestClass]
    public class PuzzleServiceTests
    {
        private ScoreService _scoreService;
        private PuzzleService _puzzleService;

        [TestInitialize]
        public void Setup()
        {
            _scoreService = new ScoreService();
            _puzzleService = new PuzzleService(_scoreService);
        }


        private void InjectFakeMathPuzzle(MathPuzzle fakeMathPuzzle)
        {
            var field = typeof(PuzzleService)
                .GetField("_mathPuzzle", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(_puzzleService, fakeMathPuzzle);
        }
        private void InjectFakeLogicPuzzle(LogicPuzzle fakeLogicPuzzle)
        {
            var field = typeof(PuzzleService)
                .GetField("_logicPuzzle", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(_puzzleService, fakeLogicPuzzle);
        }

        [TestMethod]
        public void SolveMathPuzzle_ShouldIncreaseScore_WhenCorrectAnswerProvided()
        {
            InjectFakeMathPuzzle(new FakeMathPuzzle());
            var originalIn = Console.In;
            try
            {
                Console.SetIn(new StringReader("4"));
                _puzzleService.SolveMathPuzzle();
            }
            finally
            {
                Console.SetIn(originalIn);
            }
            Assert.AreEqual(2, _scoreService.GetScore());
        }

        [TestMethod]
        public void SolveMathPuzzle_ShouldDecreaseScore_WhenIncorrectAnswerProvided()
        {
            InjectFakeMathPuzzle(new FakeMathPuzzle());
            var originalIn = Console.In;
            try
            {
                Console.SetIn(new StringReader("5"));
                _puzzleService.SolveMathPuzzle();
            }
            finally
            {
                Console.SetIn(originalIn);
            }
            Assert.AreEqual(-1, _scoreService.GetScore());
        }

        [TestMethod]
        public void SolveLogicPuzzle_ShouldIncreaseScore_WhenCorrectAnswerProvided()
        {
            InjectFakeLogicPuzzle(new FakeLogicPuzzle());

            var originalIn = Console.In;
            try
            {
                Console.SetIn(new StringReader("3"));
                _puzzleService.SolveLogicPuzzle();
            }
            finally
            {
                Console.SetIn(originalIn);
            }
            Assert.AreEqual(2, _scoreService.GetScore());
        }

        [TestMethod]
        public void SolveLogicPuzzle_ShouldDecreaseScore_WhenIncorrectAnswerProvided()
        {
            InjectFakeLogicPuzzle(new FakeLogicPuzzle());
            var originalIn = Console.In;
            try
            {
                Console.SetIn(new StringReader("1"));
                _puzzleService.SolveLogicPuzzle();
            }
            finally
            {
                Console.SetIn(originalIn);
            }
            Assert.AreEqual(-1, _scoreService.GetScore());
        }
    }
}


