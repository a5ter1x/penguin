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

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.MoveLeft += HandleMoveLeft;
            _inputService.MoveRight += HandleMoveRight;
        }

        public void DisplayScore(int score)
        {
        }

        public void DisplayPosition(PlayerPosition position)
        {
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
