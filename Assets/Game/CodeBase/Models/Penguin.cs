using System;

namespace Game.CodeBase.Models
{
    public class Penguin
    {
        public event Action<Side> SideChanged;
        public event Action<IceBall> IceBallEaten;

        private readonly IceBallTower _iceBallTower;

        private Side _side;

        public Penguin(IceBallTower iceBallTower)
        {
            _iceBallTower = iceBallTower;
        }

        public void MoveToSide(Side side)
        {
            SetSide(side);
            Eat(_iceBallTower.BottomBall);
        }

        public void SetSide(Side side)
        {
            if (side == _side)
            {
                return;
            }

            _side = side;
            SideChanged?.Invoke(_side);
        }

        private void Eat(IceBall iceBall)
        {
            IceBallEaten?.Invoke(iceBall);
        }
    }
}
