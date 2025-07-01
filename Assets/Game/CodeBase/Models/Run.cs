using System;

namespace Game.CodeBase.Models
{
    public class Run
    {
        public event Action OnScoreUpdated;
        public event Action OnPlayerPositionUpdated;

        private readonly IceBallTower _iceBallTower;

        public int Score { get; private set; }
        public Side Side { get; private set; }

        public Run(IceBallTower iceBallTower)
        {
            _iceBallTower = iceBallTower;
            _iceBallTower.Create();
        }

        public void SetScore(int newScore)
        {
            Score = newScore;
            OnScoreUpdated?.Invoke();
        }

        public void MoveNext(Side side)
        {
            SetPlayerPosition(side);
            _iceBallTower.Shift();
            IncrementScore();
        }

        private void IncrementScore()
        {
            Score++;
            OnScoreUpdated?.Invoke();
        }

        private void SetPlayerPosition(Side position)
        {
            Side = position;
            OnPlayerPositionUpdated?.Invoke();
        }
    }
}
