using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ex0204.Services;

namespace Ex0204Test
{
    [TestClass]
    public class ScoreServiceTests
    {
        private ScoreService _scoreService;

        [TestInitialize]
        public void setup()
        {
            _scoreService = new ScoreService();
        }

        [TestMethod]
        public void Score_ShouldStartAtZero()
        {
            var scoreService = new ScoreService();
            Assert.AreEqual(0, scoreService.GetScore());
        }

        [TestMethod]
        public void AddPoints_ShouldIncreaseScore()
        {
            var scoreService = new ScoreService();
            scoreService.AddPoints(5);
            Assert.AreEqual(5, scoreService.GetScore());
        }

        [TestMethod]
        public void DedeuctPoints_ShouldDecreaseScore()
        {
            var scoreService = new ScoreService();
            scoreService.AddPoints(5);
            scoreService.DeductPoints(3);
            Assert.AreEqual(2, scoreService.GetScore());
        }
    }
}
