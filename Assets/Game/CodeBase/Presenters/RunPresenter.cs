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

            _model.Updated += UpdateView;

            _view.OnMoveLeft += OnMoveLeft;
            _view.OnMoveRight += OnMoveRight;

            UpdateView();
        }

        private void OnMoveLeft() => _model.SetPlayerPosition(PlayerPosition.Left);

        private void OnMoveRight() => _model.SetPlayerPosition(PlayerPosition.Right);

        private void OnModelUpdated()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _view.DisplayScore(_model.Score);
            _view.DisplayPosition(_model.PlayerPosition);
        }

        public void Dispose()
        {
            _model.Updated -= OnModelUpdated;

            _view.OnMoveLeft -= OnMoveLeft;
            _view.OnMoveRight -= OnMoveRight;
        }
    }
}
