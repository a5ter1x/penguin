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
        [Required, SerializeField] private GameObject _defaultBarrier;
        [Required, SerializeField] private GameObject _damagedBarrier;
        private Side? _barrierSide;

        public void SetModel(Side? barrierSide, Color color)
        {
            _barrierSide = barrierSide;

            _defaultBarrier.SetActive(false);
            _damagedBarrier.SetActive(false);

            if (_barrierSide.HasValue)
            {
                ActivateBarrier(_defaultBarrier.gameObject);
            }

            _spriteRenderer.color = color;
        }

        public void ActivateDamagedBarrier()
        {
            if (_barrierSide.HasValue)
            {
                _defaultBarrier.gameObject.SetActive(false);
                ActivateBarrier(_damagedBarrier.gameObject);
            }
        }

        public void SetOrder(int order)
        {
            _spriteRenderer.sortingOrder = order;
        }

        private void ActivateBarrier(GameObject barrier)
        {
            barrier.gameObject.SetActive(true);

            Transform targetPoint = null;

            switch (_barrierSide)
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
                barrier.transform.SetParent(targetPoint, false);
                barrier.transform.localPosition = Vector3.zero;
                barrier.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
