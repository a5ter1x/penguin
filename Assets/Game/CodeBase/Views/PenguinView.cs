using System;
using DG.Tweening;
using Game.CodeBase.Common;
using Game.CodeBase.Infrastructure.Services.Input;
using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Views
{
    public class PenguinView : MonoBehaviour
    {
        public event Action OnMovedLeft;
        public event Action OnMovedRight;


        [Header("Tilt")]
        [SerializeField] private float _tiltAngle = 7f;
        [Min(0), SerializeField] private float _tiltInTime = 0.10f;
        [Min(0), SerializeField] private float _tiltOutTime = 0.15f;

        [Header("Stretch")]
        [Min(0), SerializeField] private float _stretchInTime = 0.1f;
        [Min(0), SerializeField] private float _stretchOutTime = 0.1f;
        [SerializeField] private float _verticalStretch = 0.8f;
        [SerializeField] private float _horizontalStretch  = 1.1f;

        [Required, SerializeField] private Transform _spriteTransform;
        [Required, SerializeField] private Animator _animator;
        [Required, SerializeField] private DashAnimationPlayer _dashAnimationPlayer;
        [Required, SerializeField] private ParticleSystem _eatVfx;

        private IInputService _inputService;
        private PlayerSidePoints _playerSidePoints;
        private Tween _tiltTween;
        private Tween _stretchTween;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _defaultScale = _spriteTransform.localScale;
        }

        [Inject]
        private void Construct(IInputService inputService, PlayerSidePoints playerSidePoints)
        {
            _inputService = inputService;

            _inputService.MoveLeft += HandleMoveLeft;
            _inputService.MoveRight += HandleMoveRight;

            _playerSidePoints = playerSidePoints;
        }

        private void HandleMoveLeft()
        {
            OnMovedLeft?.Invoke();
        }

        private void HandleMoveRight()
        {
            OnMovedRight?.Invoke();
        }

        public void UpdateSide(Side side)
        {
            _dashAnimationPlayer.PlayOnce(transform.position, transform.localEulerAngles);

            var sidePoint = _playerSidePoints.Get(side);

            transform.position = sidePoint.position;
            transform.localEulerAngles = sidePoint.localEulerAngles;
        }

        public void Eat(IceBall iceBall)
        {
            PlayEatAnimation(iceBall);
            AnimateStretch();
            AnimateTilt();
        }

        public void PlayLossAnimation()
        {
            _stretchTween?.Kill();
            _eatVfx.Stop();

            _animator.SetState(AnimationState.Loss);
        }

        private void PlayEatAnimation(IceBall iceBall)
        {
            _animator.SetState(AnimationState.Eat);

            var main = _eatVfx.main;
            main.startColor = iceBall.Color;

            _eatVfx.Play();
        }

        private void AnimateStretch()
        {
            _stretchTween?.Kill();
            _stretchTween = DOTween.Sequence()
               .Append(_spriteTransform.DOScaleY(_defaultScale.y * _verticalStretch, _stretchInTime))
               .Join(_spriteTransform.DOScaleX(_defaultScale.x * _horizontalStretch, _stretchInTime))
               .Append(_spriteTransform.DOScale(_defaultScale, _stretchOutTime))
               .SetEase(Ease.OutQuad)
               .OnComplete(() =>
               {
                   _animator.SetState(AnimationState.Idle);
                   _eatVfx.Stop();
               })
               .OnKill(() =>
               {
                   _spriteTransform.localScale = _defaultScale;
               });
        }

        private void AnimateTilt()
        {
            _tiltTween?.Kill();
            _tiltTween = DOTween.Sequence()
                .Append(_spriteTransform.DOLocalRotate(new Vector3(0, 0, _tiltAngle), _tiltInTime).SetEase(Ease.OutQuad))
                .Append(_spriteTransform.DOLocalRotate(Vector3.zero, _tiltOutTime).SetEase(Ease.InOutQuad));
        }

        private void OnDestroy()
        {
            if (_inputService != null)
            {
                _inputService.MoveLeft -= HandleMoveLeft;
                _inputService.MoveRight -= HandleMoveRight;
            }

            _stretchTween?.Kill();
            _tiltTween?.Kill();
        }

        private enum AnimationState
        {
            Idle = 0,
            Eat = 1,
            Loss = 2,
        }
    }
}
