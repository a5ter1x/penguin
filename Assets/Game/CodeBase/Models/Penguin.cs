using System;

namespace Game.CodeBase.Models
{
    public class Penguin
    {
        public event Action<Side> SideChanged;
        public event Action<IceBall> IceBallEaten;
        public event Action ReactedToLoss;

        private IceBallTower _iceBallTower;

        public Penguin(IceBallTower iceBallTower)
        {
            _iceBallTower = iceBallTower;
        }

        public Side Side { get; private set; }

        public void MoveToSide(Side side)
        {
            if (side != Side)
            {
                SetSide(side);
            }

            Eat(_iceBallTower.BottomBall);
        }

        public void SetSide(Side side)
        {
            Side = side;
            SideChanged?.Invoke(Side);
        }

        public void ReactToLoss()
        {
            ReactedToLoss?.Invoke();
        }

        private void Eat(IceBall iceBall)
        {
            IceBallEaten?.Invoke(iceBall);
        }
    }
}
