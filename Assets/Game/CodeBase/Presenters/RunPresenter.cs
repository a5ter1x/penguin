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

            UpdateView();
        }

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
        }
    }
}
