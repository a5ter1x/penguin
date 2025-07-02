using System.Collections.Generic;
using DG.Tweening;
using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class IceBallTowerView : MonoBehaviour
    {
        [Required, SerializeField] private Transform _bottomPoint;
        [Required, SerializeField] private IceBallView _iceBallPrefab;
        [Min(0), SerializeField] private float _spacing = 0.85f;
        [Min(0), SerializeField] private float _moveDuration = 0.1f;

        private readonly List<IceBallView> _balls = new();

        public void Create(List<IceBall> balls)
        {
            Clear();

            foreach (var ballModel in balls)
            {
                var ballView = Instantiate(_iceBallPrefab, transform);
                ballView.SetModel(null, ballModel.Color);
                _balls.Add(ballView);
            }

            UpdatePositions();
        }

        public void Shift(IceBall ball)
        {
            if (_balls.Count == 0)
            {
                return;
            }

            var bottomBall = _balls[0];
            _balls.RemoveAt(0);

            ReuseBallWithNewModel(bottomBall, ball);
            bottomBall.transform.position = _bottomPoint.position + _balls.Count * Vector3.up * _spacing;
            _balls.Add(bottomBall);

            UpdatePositions();
        }

        public void DestroySideBarrier()
        {
            _balls[0].ActivateDamagedBarrier();
        }

        private void ReuseBallWithNewModel(IceBallView view, IceBall newModel)
        {
            view.SetModel(newModel.BarrierSide, newModel.Color);
        }

        private void UpdatePositions()
        {
            for (var i = 0; i < _balls.Count; i++)
            {
                _balls[i].SetOrder(i);

                var targetY = _bottomPoint.position.y + i * _spacing;

                _balls[i].transform.DOKill();
                _balls[i].transform.DOMoveY(targetY, _moveDuration);
            }
        }

        private void Clear()
        {
            foreach (var ball in _balls)
            {
                Destroy(ball.gameObject);
            }

            _balls.Clear();
        }
    }
}
