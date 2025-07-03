using System;
using Cysharp.Threading.Tasks;
using Game.CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace Game.CodeBase.Models
{
    public class Run : IDisposable
    {
        public const float RunMaxDuration = 100;

        public event Action ScoreUpdated;
        public event Action Started;
        public event Action<float> TimerTicked;
        public event Action Lost;

        private IceBallTower _iceBallTower;
        private Penguin _penguin;
        private IInputService _inputService;

        private bool _isRunning;
        private float _elapsedTime;

        public Run(IceBallTower iceBallTower, Penguin penguin, IInputService inputService)
        {
            _iceBallTower = iceBallTower;
            _penguin = penguin;
            _inputService = inputService;

            _penguin.IceBallEaten += PenguinOnIceBallEaten;

            _inputService.Enable();

            _iceBallTower.Create();
            _penguin.SetSide(Side.Left);
        }

        private float RemainingTimeNormalized => Mathf.Clamp01((RunMaxDuration - _elapsedTime) / RunMaxDuration);

        public int Score { get; private set; }


        private void PenguinOnIceBallEaten(IceBall obj)
        {
            if (!_isRunning)
            {
                StartTimerAsync().Forget();
                Started?.Invoke();
            }

            _iceBallTower.Shift();
            IncrementScore();

            if (_iceBallTower.BottomBall.BarrierSide == _penguin.Side)
            {
                _penguin.ReactToLoss();
                _iceBallTower.DestroySideBarrier();
                Lose();
            }
        }

        private void IncrementScore()
        {
            Score++;
            ScoreUpdated?.Invoke();
        }

        private async UniTaskVoid StartTimerAsync()
        {
            _isRunning = true;
            _elapsedTime = 0;

            while (_elapsedTime < RunMaxDuration && _isRunning)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _elapsedTime += Time.deltaTime;
                TimerTicked?.Invoke(RemainingTimeNormalized);
            }

            if (_isRunning)
            {
                Lose();
            }
        }

        private void Lose()
        {
            _isRunning = false;
            _inputService.Disable();
            Lost?.Invoke();
        }

        public void Dispose()
        {
            _penguin.IceBallEaten -= PenguinOnIceBallEaten;

            _inputService.Disable();

            _iceBallTower = null;
            _penguin = null;
            _inputService = null;
        }
    }
}
