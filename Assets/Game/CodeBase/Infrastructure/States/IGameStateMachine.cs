namespace Game.CodeBase.Infrastructure.States
{
    public interface IGameStateMachine
    {
        IState CurrentState { get; }

        public void Enter<TState>() where TState : class, IEnterState;
    }
}
