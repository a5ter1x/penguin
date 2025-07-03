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

            _model.Created += OnTowerCreated;
            _model.Shifted += OnTowerShifted;
            _model.SideBarrierDestroyed += OnSideBarrierDestroyed;
        }

        private void OnTowerCreated(List<IceBall> balls)
        {
            _view.Create(balls);
        }

        private void OnTowerShifted(IceBall ball)
        {
            _view.Shift(ball);
        }

        private void OnSideBarrierDestroyed()
        {
            _view.DestroySideBarrier();
        }

        public void Dispose()
        {
            _model.Created -= OnTowerCreated;
            _model.Shifted -= OnTowerShifted;
            _model.SideBarrierDestroyed -= OnSideBarrierDestroyed;
        }
    }
}
