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

            _model.Creted += OnTowerCreated;
            _model.Shifted += OnTowerShifted;
        }

        private void OnTowerCreated(List<IceBall> balls)
        {
            _view.Create(balls);
        }

        private void OnTowerShifted(IceBall ball)
        {
            _view.Shift(ball);
        }

        public void Dispose()
        {
            _model.Creted -= OnTowerCreated;
            _model.Shifted -= OnTowerShifted;
        }
    }
}
