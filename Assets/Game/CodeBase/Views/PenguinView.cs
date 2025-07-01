using DG.Tweening;
using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class PenguinView : MonoBehaviour
    {
        [Min(0), SerializeField] private float _eatAnimationDuration = 0.1f;
        [Required, SerializeField] private Transform _leftTransform;
        [Required, SerializeField] private Transform _rightTransform;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        public void UpdateSide(Side side)
        {
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

            PlayEatAnimation();
        }

        private void PlayEatAnimation()
        {
            transform.DOKill();

            var squash = DOTween.Sequence();

            squash.Append(transform.DOScaleY(_initialScale.y * 0.8f, _eatAnimationDuration))
                  .Join(transform.DOScaleX(_initialScale.x * 1.1f, _eatAnimationDuration))
                  .Append(transform.DOScale(_initialScale, _eatAnimationDuration))
                  .SetEase(Ease.OutQuad);
        }
    }
}
