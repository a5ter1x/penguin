using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.CodeBase.Models
{
    public class Run : IDisposable
    {
        public const float RunMaxDuration = 100;

        public event Action ScoreUpdated;
        public event Action<float> TimerTicked;
        public event Action Loss;

        private readonly IceBallTower _iceBallTower;
        private readonly Penguin _penguin;

        private bool _isRunning;
        private float _elapsedTime;

        private float RemainingTimeNormalized => Mathf.Clamp01((RunMaxDuration - _elapsedTime) / RunMaxDuration);

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
            if (!_isRunning)
            {
                _isRunning = true;
                StartTimerAsync().Forget();
            }

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

        private async UniTaskVoid StartTimerAsync()
        {
            _elapsedTime = 0;

            while (_elapsedTime < RunMaxDuration && _isRunning)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _elapsedTime += UnityEngine.Time.deltaTime;
                TimerTicked?.Invoke(RemainingTimeNormalized);
            }

            if (_isRunning)
            {
                _isRunning = false;
                Loss?.Invoke();
            }
        }

        public void Dispose()
        {
            _penguin.IceBallEaten -= PenguinOnIceBallEaten;
        }
    }
}
