using System;
using UnityEngine.InputSystem;

namespace Game.CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private const string GameplayMapName = "Gameplay";
        private const string LeftActionName = "Left";
        private const string RightActionName = "Right";

        public event Action MoveLeft;
        public event Action MoveRight;

        private readonly InputActionAsset _actions;
        private readonly InputAction _leftAction;
        private readonly InputAction _rightAction;

        public InputService(InputActionAsset actions)
        {
            _actions = actions ?? throw new ArgumentNullException(nameof(actions));

            var gameplayMap = _actions.FindActionMap(GameplayMapName, throwIfNotFound: true);

            _leftAction = gameplayMap.FindAction(LeftActionName, throwIfNotFound: true);
            _rightAction = gameplayMap.FindAction(RightActionName, throwIfNotFound: true);

            _leftAction.performed += OnLeftPerformed;
            _rightAction.performed += OnRightPerformed;
        }

        public void Enable()
        {
            _actions.Enable();
        }

        public void Disable()
        {
            _actions.Disable();
        }

        private void OnLeftPerformed(InputAction.CallbackContext ctx)
        {
            MoveLeft?.Invoke();
        }

        private void OnRightPerformed(InputAction.CallbackContext ctx)
        {
            MoveRight?.Invoke();
        }

        public void Dispose()
        {
            _leftAction.performed -= OnLeftPerformed;
            _rightAction.performed -= OnRightPerformed;

            Disable();
        }
    }
}
