using System;

namespace Game.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        public event Action MoveLeft;
        public event Action MoveRight;

        public void Enable();
        public void Disable();
    }
}
