using UnityEngine;

namespace Game.CodeBase.Models
{
    public class IceBall
    {
        public Side? BarrierSide { get; private set; }

        public Color Color { get; private set;}

        public void SetData(Side? barrierSide, Color color)
        {
            BarrierSide = barrierSide;
            Color = color;
        }
    }
}
