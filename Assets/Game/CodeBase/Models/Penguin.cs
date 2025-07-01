using System;

namespace Game.CodeBase.Models
{
    public class Penguin
    {
        public event Action<Side> OnSideUpdated;

        private Side _side;

        public void UpdateSide(Side side)
        {
            _side = side;

            OnSideUpdated?.Invoke(_side);
        }
    }
}
