using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.CodeBase.Models
{
    public class IceBallTower
    {
        private const int Size = 20;

        public event Action<List<IceBall>> Created;
        public event Action<IceBall> Shifted;
        public event Action SideBarrierDestroyed;

        private readonly List<IceBall> _balls = new();

        public IceBall BottomBall => _balls[0];

        public void Create()
        {
            for (var i = 0; i < Size; i++)
            {
                var iceBall = new IceBall();

                iceBall.SetData(barrierSide: null, color: RandomColor());

                _balls.Add(iceBall);
            }

            Created?.Invoke(_balls);
        }

        public void Shift()
        {
            var ball = _balls[0];
            _balls.RemoveAt(0);

            ball.SetData(barrierSide: RandomBarrierSide(), color: RandomColor());
            _balls.Add(ball);

            Shifted?.Invoke(ball);
        }

        public void DestroySideBarrier()
        {
            SideBarrierDestroyed?.Invoke();
        }

        private static Side? RandomBarrierSide()
        {
            if (Random.Range(0, 100) < 10)
            {
                return Random.Range(0, 2) > 0 ? Side.Left : Side.Right;
            }

            return null;
        }

        private static Color RandomColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
