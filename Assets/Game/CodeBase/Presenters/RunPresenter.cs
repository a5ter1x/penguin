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

            _model.OnScoreUpdated += UpdateScore;

            _view.OnMoveLeft += OnMoveLeft;
            _view.OnMoveRight += OnMoveRight;

            UpdateScore();
        }

        private void OnMoveLeft()
        {
            _model.MoveNext(Side.Left);
        }

        private void OnMoveRight()
        {
            _model.MoveNext(Side.Right);
        }

        private void UpdateScore()
        {
            _view.DisplayScore(_model.Score);
        }

        public void Dispose()
        {
            _model.OnScoreUpdated -= UpdateScore;

            _view.OnMoveLeft -= OnMoveLeft;
            _view.OnMoveRight -= OnMoveRight;
        }
    }
}
