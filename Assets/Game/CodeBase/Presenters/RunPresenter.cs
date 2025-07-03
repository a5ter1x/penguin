using System;
using Game.CodeBase.Models;
using Game.CodeBase.Views;

namespace Game.CodeBase.Presenters
{
    public class RunPresenter : IDisposable
    {
        private readonly Run _model;
        private readonly RunView _view;

        public RunPresenter(Run model, RunView view)
        {
            _model = model;
            _view = view;

            _model.ScoreUpdated += UpdateScore;
            _model.Started += OnRunStarted;
            _model.TimerTicked += OnTimerTicked;
            _model.Lost += OnLost;

            UpdateScore();
        }

        private void UpdateScore()
        {
            _view.UpdateScore(_model.Score);
        }

        private void OnRunStarted()
        {
            _view.StartRun();
        }

        private void OnTimerTicked(float remainingTimeNormalized)
        {
            _view.TickTimer(remainingTimeNormalized);
        }

        private void OnLost()
        {
            _view.Lose(_model.Score);
        }

        public void Dispose()
        {
            _model.ScoreUpdated -= UpdateScore;
            _model.Started += OnRunStarted;
            _model.TimerTicked -= OnTimerTicked;
            _model.Lost -= OnLost;
        }
    }
}
