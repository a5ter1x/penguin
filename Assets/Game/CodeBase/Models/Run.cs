using System;

namespace Game.CodeBase.Models
{
    public class Run
    {
        public const float MaxDurationInSeconds = 100;

        public event Action OnScoreUpdated;

        private readonly IceBallTower _iceBallTower;
        private readonly Penguin _penguin;

        public int Score { get; private set; }

        public Run(IceBallTower iceBallTower, Penguin penguin)
        {
            _iceBallTower = iceBallTower;
            _penguin = penguin;

            _iceBallTower.Create();
            _penguin.UpdateSide(Side.Left);
        }

        public void SetScore(int newScore)
        {
            Score = newScore;
            OnScoreUpdated?.Invoke();
        }

        public void MoveNext(Side side)
        {
            _penguin.UpdateSide(side);
            _iceBallTower.Shift();

            IncrementScore();
        }

        private void IncrementScore()
        {
            Score++;
            OnScoreUpdated?.Invoke();
        }
    }
}
