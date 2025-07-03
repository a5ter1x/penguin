using Game.CodeBase.Infrastructure.States;

namespace Game.CodeBase.Infrastructure.Commands
{
    public class RestartCommand : IRestartCommand
    {
        private readonly IGameStateMachine _stateMachine;

        public RestartCommand(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Execute()
        {
            if (_stateMachine.CurrentState is GameplayState gameplayState)
            {
                gameplayState.ReloadCurrentScene();
            }
        }
    }
}