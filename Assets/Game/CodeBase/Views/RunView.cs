using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.CodeBase.Models;
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

        [RequiredListLength(minLength: 0, maxLength: int.MaxValue), Required, SerializeField]
        private List<VerticalGradient> _skyColorStages = new();

        private GameplayUI _gameplayUI;

        [Inject]
        public void Construct(GameplayUI gameplayUI)
        {
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

        private async UniTaskVoid AnimateSkyCycle()
        {
            var index = 0;

            while (true)
            {
                var current = _skyColorStages[index];
                var next = _skyColorStages[(index + 1) % _skyColorStages.Count];

                await LerpColors(current, next, _skyStageDuration);

                index = (index + 1) % _skyColorStages.Count;
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
            _skyMaterial.SetColor(TopColorId, top);
            _skyMaterial.SetColor(BottomColorId, bottom);
        }
    }
}
