using System;
using Game.CodeBase.Infrastructure.Services.Input;
using Game.CodeBase.Models;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Views
{
    public class RunView : MonoBehaviour
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;

        private IInputService _inputService;
        private Penguin _penguin;
        private GameplayUI _gameplayUI;
        private Transform _left;
        private Transform _right;

        [Inject]
        public void Construct(IInputService inputService, Penguin penguin, GameplayUI gameplayUI, LevelPoints levelPoints)
        {
            _inputService = inputService;

            _inputService.MoveLeft += HandleMoveLeft;
            _inputService.MoveRight += HandleMoveRight;

            _penguin = penguin;

            _gameplayUI = gameplayUI;

            _left = levelPoints.LeftPoint;
            _right = levelPoints.RightPoint;
        }

        public void DisplayScore(int score)
        {
            _gameplayUI.ScoreField.text = score.ToString();
        }

        public void DisplayPosition(PlayerPosition position)
        {
            switch(position)
            {
                case PlayerPosition.Left:
                    _penguin.transform.position = _left.position;
                    break;

                case PlayerPosition.Right:
                    _penguin.transform.position = _right.position;
                    break;
                default:
                    break;
            }
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
