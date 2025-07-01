using System;
using System.Collections.Generic;
using Game.CodeBase.Models;
using Game.CodeBase.Views;

namespace Game.CodeBase.Presenters
{
    public class IceBallTowerPresenter : IDisposable
    {
        private readonly IceBallTower _model;
        private readonly IceBallTowerView _view;

        public IceBallTowerPresenter(IceBallTower model, IceBallTowerView view)
        {
            _model = model;
            _view = view;

            _model.OnCrated += CreateTower;
            _model.OnShift += ShiftTower;
        }

        private void CreateTower(List<IceBall> balls)
        {
            _view.Create(balls);
        }

        private void ShiftTower(IceBall ball)
        {
            _view.Shift(ball);
        }

        public void Dispose()
        {
            _model.OnCrated -= CreateTower;
            _model.OnShift -= ShiftTower;
        }
    }
}
