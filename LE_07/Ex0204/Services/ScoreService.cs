namespace Ex0204.Services
{
    public class ScoreService
    {
        private int _score;

        public ScoreService()
        {
            _score = 0;
        }

        public int GetScore()
        {
            return _score;
        }

        public void AddPoints(int points)
        {
            _score += points;
        }

        public void DeductPoints(int points)
        {
            _score -= points;
        }
    }
}