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
            _model.Lose += OnLose;

            UpdateScore();
        }

        private void UpdateScore()
        {
            _view.DisplayScore(_model.Score);
        }

        private void OnLose()
        {
            _view.Lose();
        }

        public void Dispose()
        {
            _model.Lose -= OnLose;
            _model.ScoreUpdated -= UpdateScore;
        }
    }
}
