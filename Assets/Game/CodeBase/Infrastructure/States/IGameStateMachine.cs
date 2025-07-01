namespace Game.CodeBase.Infrastructure.States
{
    public interface IGameStateMachine
    {
        public void Enter<TState>() where TState : class, IEnterState;
    }
}
