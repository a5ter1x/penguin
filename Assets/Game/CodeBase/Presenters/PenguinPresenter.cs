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

            _model.SideChanged += OnSideChanged;
            _model.IceBallEaten += OnIceBallEaten;
            _model.ReactedToLoss += OnReactedToLoss;

            _view.OnMovedLeft += OnMovedLeft;
            _view.OnMovedRight += OnMovedRight;
        }

        private void OnSideChanged(Side side)
        {
            _view.ChangeSide(side);
        }

        private void OnIceBallEaten(IceBall iceBall)
        {
            _view.Eat(iceBall);
        }

        private void OnReactedToLoss()
        {
            _view.PlayLossAnimation();
        }

        private void OnMovedLeft()
        {
            _model.MoveToSide(Side.Left);
        }

        private void OnMovedRight()
        {
            _model.MoveToSide(Side.Right);
        }

        public void Dispose()
        {
            _model.SideChanged -= OnSideChanged;
            _model.IceBallEaten -= OnIceBallEaten;
            _model.ReactedToLoss -= OnReactedToLoss;

            _view.OnMovedLeft -= OnMovedLeft;
            _view.OnMovedRight -= OnMovedRight;
        }
    }
}
