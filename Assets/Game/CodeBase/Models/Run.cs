using System;

namespace Game.CodeBase.Models
{
    public class Run : IDisposable
    {
        public const float MaxDurationInSeconds = 100;

        public event Action ScoreUpdated;
        public event Action Loss;

        private readonly IceBallTower _iceBallTower;
        private readonly Penguin _penguin;

        public int Score { get; private set; }

        public Run(IceBallTower iceBallTower, Penguin penguin)
        {
            _iceBallTower = iceBallTower;
            _penguin = penguin;

            _penguin.IceBallEaten += PenguinOnIceBallEaten;

            _iceBallTower.Create();
            _penguin.SetSide(Side.Left);
        }

        private void PenguinOnIceBallEaten(IceBall obj)
        {
            _iceBallTower.Shift();
            IncrementScore();

            if (_iceBallTower.BottomBall.BarrierSide == _penguin.Side)
            {
                _penguin.ReactToLoss();
                _iceBallTower.DestroySideBarrier();
                Loss?.Invoke();
            }
        }

        private void IncrementScore()
        {
            Score++;
            ScoreUpdated?.Invoke();
        }

        public void Dispose()
        {
            _penguin.IceBallEaten -= PenguinOnIceBallEaten;
        }
    }
}
