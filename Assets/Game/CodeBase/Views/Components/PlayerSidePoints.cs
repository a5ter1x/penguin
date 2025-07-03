using System;
using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views.Components
{
    public class PlayerSidePoints : MonoBehaviour
    {
        [Required, SerializeField] private Transform _left;
        [Required, SerializeField] private Transform _right;

        public Transform Get(Side side)
        {
            return side switch
            {
                Side.Left  => _left,
                Side.Right => _right,
                _          => throw new ArgumentOutOfRangeException(nameof(side), side, null),
            };
        }
    }
}
