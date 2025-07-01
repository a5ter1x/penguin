using System.Collections.Generic;
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

        private readonly List<IceBallView> _balls = new();

        public void Create(List<IceBall> balls)
        {
            Clear();

            foreach (var ballModel in balls)
            {
                var ballView = Instantiate(_iceBallPrefab, transform);
                ballView.SetColor(ballModel.Color);
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

            bottomBall.SetColor(ball.Color);
            _balls.Add(bottomBall);

            UpdatePositions();
        }

        private void UpdatePositions()
        {
            for (var i = 0; i < _balls.Count; i++)
            {
                _balls[i].transform.position = _bottomPoint.position + Vector3.up * i * _spacing;
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
