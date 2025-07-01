using System;
using Game.CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Views
{
    public class RunView : MonoBehaviour
    {
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
    }
}
