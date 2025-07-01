using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.CodeBase.Infrastructure.Services.Input;
using Game.CodeBase.Models;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Views
{
    public class RunView : MonoBehaviour
    {
        private float _skyStageDuration;
        private Material _skyMaterial;

        [Required, SerializeField] private Renderer _skyRenderer;
        [Required, SerializeField] private List<VerticalGradient> _skyColorStages = new()
        {
            new VerticalGradient(new Color(0.8f, 0.9f, 1f), new Color(0.4f, 0.5f, 0.8f)),
            new VerticalGradient(new Color(0.3f, 0.7f, 1f), new Color(0.5f, 0.9f, 1f)),
            new VerticalGradient(new Color(1f, 0.5f, 0.3f), new Color(0.4f, 0.2f, 0.6f)),
            new VerticalGradient(new Color(0.05f, 0.02f, 0.1f), new Color(0.01f, 0.01f, 0.05f)),
        };

        public event Action OnMoveLeft;
        public event Action OnMoveRight;

        private IInputService _inputService;
        private GameplayUI _gameplayUI;

        [Inject]
        public void Construct(
            IInputService inputService,
            GameplayUI gameplayUI,
            IceBallTowerView towerView)
        {
            _inputService = inputService;

            _inputService.MoveLeft += HandleMoveLeft;
            _inputService.MoveRight += HandleMoveRight;

            _gameplayUI = gameplayUI;
        }

        private void Start()
        {
            _skyMaterial = _skyRenderer.material;
            _skyStageDuration = Run.MaxDurationInSeconds / _skyColorStages.Count;

            AnimateSkyCycle().Forget();
        }

        public void DisplayScore(int score)
        {
            _gameplayUI.ScoreField.text = score.ToString();
        }

        private void HandleMoveLeft()
        {
            OnMoveLeft?.Invoke();
        }

        private void HandleMoveRight()
        {
            OnMoveRight?.Invoke();
        }

        private void OnDestroy()
        {
            _inputService.MoveLeft -= HandleMoveLeft;
            _inputService.MoveRight -= HandleMoveRight;
        }

        private async UniTaskVoid AnimateSkyCycle()
        {
            for (var i = 0; i < _skyColorStages.Count; i++)
            {
                var current = _skyColorStages[i];
                var next = _skyColorStages[(i + 1) % _skyColorStages.Count];

                await LerpColors(current, next, _skyStageDuration);
            }
        }

        private async UniTask LerpColors(VerticalGradient from, VerticalGradient to, float duration)
        {
            var time = 0f;

            while (time < duration)
            {
                var t = time / duration;

                var top = Color.Lerp(from.TopColor, to.TopColor, t);
                var bottom = Color.Lerp(from.BottomColor, to.BottomColor, t);

                SetSkyColors(top, bottom);

                time += Time.deltaTime;
                await UniTask.Yield();
            }

            SetSkyColors(to.TopColor, to.BottomColor);
        }

        private void SetSkyColors(Color top, Color bottom)
        {
            const string topColorPropertyName = "_TopColor";
            const string bottomColorPropertyName = "_BottomColor";

            _skyMaterial.SetColor(topColorPropertyName, top);
            _skyMaterial.SetColor(bottomColorPropertyName, bottom);
        }
    }
}
