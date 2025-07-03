using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.CodeBase.Models;
using Game.CodeBase.Views.Components;
using Game.CodeBase.Views.UserInterface;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Views
{
    public class RunView : MonoBehaviour
    {
        private static readonly int TopColorId = Shader.PropertyToID("_TopColor");
        private static readonly int BottomColorId = Shader.PropertyToID("_BottomColor");

        private float _skyStageDuration;
        private Material _skyMaterial;

        [Required, SerializeField] private Renderer _skyRenderer;

        [RequiredListLength(minLength: 1, maxLength: int.MaxValue), Required, SerializeField]
        private List<VerticalGradient> _skyColorStages = new();

        private GameUI _gameUI;

        [Inject]
        public void Construct(GameUI gameUI)
        {
            _gameUI = gameUI;
        }

        private void Start()
        {
            _skyMaterial = _skyRenderer.material;
            _skyStageDuration = Run.RunMaxDuration / _skyColorStages.Count;
        }

        public void UpdateScore(int score)
        {
            _gameUI.UpdateGameplayScoreField(score);
        }

        public void StartRun()
        {
            _gameUI.AnimateHintArrowsOut();
            AnimateSkyCycle().Forget();
        }

        public void TickTimer(float remainingTimeNormalized)
        {
            _gameUI.UpdateTimebar(remainingTimeNormalized);
        }

        public void Lose(int score)
        {
            _gameUI.ShowLossPanel(score);
        }

        private async UniTaskVoid AnimateSkyCycle()
        {
            var token = this.GetCancellationTokenOnDestroy();

            for (var i = 0; i < _skyColorStages.Count - 1; i++)
            {
                var current = _skyColorStages[i];
                var next = _skyColorStages[(i + 1) % _skyColorStages.Count];

                await LerpColors(current, next, _skyStageDuration, token);
            }
        }

        private async UniTask LerpColors(VerticalGradient from, VerticalGradient to, float duration, CancellationToken token)
        {
            var time = 0f;

            while (time < duration)
            {
                token.ThrowIfCancellationRequested();

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
            _skyMaterial.SetColor(TopColorId, top);
            _skyMaterial.SetColor(BottomColorId, bottom);
        }
    }
}
