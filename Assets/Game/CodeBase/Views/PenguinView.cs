using DG.Tweening;
using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class PenguinView : MonoBehaviour
    {
        [Required, SerializeField] private Transform _leftTransform;
        [Required, SerializeField] private Transform _rightTransform;

        private Vector3 _initialScale;

        private void Start()
        {
            _initialScale = transform.localScale;
        }

        public void Eat()
        {
            transform.DOKill();

            var squash = DOTween.Sequence();

            squash.Append(transform.DOScaleY(_initialScale.y * 0.8f, 0.1f))
                  .Join(transform.DOScaleX(_initialScale.x * 1.1f, 0.1f))
                  .Append(transform.DOScale(_initialScale, 0.1f))
                  .SetEase(Ease.OutQuad);
        }

        public void UpdateSide(Side side)
        {
            Eat();

            Transform targetTransform;

            switch(side)
            {
                case Side.Left:
                    targetTransform = _leftTransform;
                    break;

                case Side.Right:
                    targetTransform = _rightTransform;
                    break;
                default:
                    return;
            }

            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }
    }
}
