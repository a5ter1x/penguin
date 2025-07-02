using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class IceBallView : MonoBehaviour
    {
        [Required, SerializeField] private SpriteRenderer _spriteRenderer;
        [Required, SerializeField] private Transform _leftBarrierPoint;
        [Required, SerializeField] private Transform _rightBarrierPoint;
        [Required, SerializeField] private GameObject _barrier;
        private Side? _barrierSide;

        public void SetModel(Side? barrierSide, Color color)
        {
            _barrierSide = barrierSide;

            _barrier.SetActive(_barrierSide.HasValue);

            Transform targetPoint = null;

            switch (barrierSide)
            {
                case Side.Left:
                    targetPoint = _leftBarrierPoint;
                    break;

                case Side.Right:
                    targetPoint = _rightBarrierPoint;
                    break;
            }

            if (targetPoint != null)
            {
                _barrier.transform.SetParent(targetPoint, false);
                _barrier.transform.localPosition = Vector3.zero;
                _barrier.transform.localRotation = Quaternion.identity;
            }

            _spriteRenderer.color = color;
        }

        public void SetOrder(int order)
        {
            _spriteRenderer.sortingOrder = order;
        }
    }
}
