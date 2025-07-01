using System;
using Game.CodeBase.Models;
using Game.CodeBase.Views;

namespace Game.CodeBase.Presenters
{
    public class PenguinPresenter : IDisposable
    {
        private readonly Penguin _model;
        private readonly PenguinView _view;

        public PenguinPresenter(Penguin model, PenguinView view)
        {
            _model = model;
            _view = view;

            _model.OnSideUpdated += UpdateSide;
        }

        private void UpdateSide(Side side)
        {
            _view.UpdateSide(side);
        }

        public void Dispose()
        {
            _model.OnSideUpdated -= UpdateSide;
        }
    }
}
