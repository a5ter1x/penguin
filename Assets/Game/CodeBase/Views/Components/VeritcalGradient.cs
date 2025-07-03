using System;
using UnityEngine;

namespace Game.CodeBase.Views.Components
{
    [Serializable]
    public class VerticalGradient
    {
        public Color TopColor;
        public Color BottomColor;

        public VerticalGradient(Color top, Color bottom)
        {
            TopColor = top;
            BottomColor = bottom;
        }
    }
}
