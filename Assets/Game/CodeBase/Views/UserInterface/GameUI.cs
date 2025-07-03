using DG.Tweening;
using Game.CodeBase.Infrastructure.Commands;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.CodeBase.Views.UserInterface
{
    public class GameUI : MonoBehaviour
    {
        [Header("Hint Arrows"),Required,SerializeField]
                private RectTransform _hintArrowsContainer;
        [Required, SerializeField] private RectTransform _leftHintArrow;
        [Required, SerializeField] private RectTransform _rightHintArrow;
        [SerializeField] private float _hintMoveOffset = 20f;
        [SerializeField] private float _hintMoveDuration = 0.5f;
        [SerializeField] private float _hintHideScale = 0.8f;
        [SerializeField] private float _hintHideScaleDuration = 0.2f;
        [SerializeField] private float _hintHideOffsetY = -200f;
        [SerializeField] private float _hintHideMoveDuration = 0.6f;

        [Header("Gameplay"),Required,SerializeField]
                private TextMeshProUGUI _gameplayScoreText;
        [SerializeField] private float _scorePunchScale = 1.2f;
        [SerializeField] private float _scorePunchDuration = 0.3f;
        [SerializeField] private int _scorePunchVibrato = 10;
        [SerializeField] private float _scorePunchElasticity = 0.9f;

        [Required, SerializeField] private Slider _timeSlider;

        [Required, SerializeField] private Button _restartFromGameplayButton;

        [Header("Loss"),Required,SerializeField]
                private GameObject _lossPanel;
        [Required, SerializeField] private TextMeshProUGUI _lossPanelScoreText;
        [Required, SerializeField] private Button _restartFromLossButton;

        private Tween _scorePunchTween;
        private Tween _leftHintTween;
        private Tween _rightHintTween;
        private IRestartCommand _restartCommand;

        [Inject]
        private void Construct(IRestartCommand restartCommand)
        {
            _restartCommand = restartCommand;

            _restartFromGameplayButton.onClick.AddListener(_restartCommand.Execute);
            _restartFromLossButton.onClick.AddListener(_restartCommand.Execute);
        }

        private void OnDestroy()
        {
            _restartFromGameplayButton.onClick.RemoveAllListeners();
            _restartFromLossButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            AnimateHintArrowsLoop();
            AnimateHintArrowsOut();
        }

        private void AnimateHintArrowsOut()
        {
            _leftHintTween?.Kill();
            _rightHintTween?.Kill();

            var scale = _hintArrowsContainer.DOScale(_hintHideScale, _hintHideScaleDuration);
            var move = _hintArrowsContainer.DOLocalMoveY(_hintArrowsContainer.localPosition.y + _hintHideOffsetY, _hintHideMoveDuration).SetEase(Ease.InBack);

            DOTween.Sequence().Join(scale).Append(move).OnComplete(() => _hintArrowsContainer.gameObject.SetActive(false));
        }
        private void AnimateHintArrowsLoop()
        {
            _leftHintTween = _leftHintArrow.DOAnchorPosX(_leftHintArrow.anchoredPosition.x - _hintMoveOffset, _hintMoveDuration)
                                           .SetLoops(-1, LoopType.Yoyo)
                                           .SetEase(Ease.InOutSine);

            _rightHintTween = _rightHintArrow.DOAnchorPosX(_rightHintArrow.anchoredPosition.x + _hintMoveOffset, _hintMoveDuration)
                                             .SetLoops(-1, LoopType.Yoyo)
                                             .SetEase(Ease.InOutSine);
        }

        public void UpdateGameplayScoreField(int score)
        {
            _gameplayScoreText.text = score.ToString();
            AnimateScorePunch();
        }

        private void AnimateScorePunch()
        {
            _scorePunchTween?.Kill();

            _gameplayScoreText.transform.localScale = Vector3.one;

            _scorePunchTween = _gameplayScoreText.transform
                                                 .DOPunchScale(Vector3.one * (_scorePunchScale - 1f), _scorePunchDuration, _scorePunchVibrato, _scorePunchElasticity)
                                                 .SetEase(Ease.OutBack);
        }

        public void UpdateTimebar(float remainingTimeNormalized)
        {
            _timeSlider.value = remainingTimeNormalized;
        }

        public void ShowLossPanel(int score)
        {
            _lossPanel.SetActive(true);

            _lossPanelScoreText.text = score.ToString();
        }
    }
}
