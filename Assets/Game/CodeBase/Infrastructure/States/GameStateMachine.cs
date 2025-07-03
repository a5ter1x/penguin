using System;
using System.Collections.Generic;

namespace Game.CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IEnterState> _states;

        private IEnterState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IEnterState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(GameplayState)] = new GameplayState(this),
            };

            Enter<BootstrapState>();
        }

        public IState CurrentState => _currentState;

        public void Enter<TState>() where TState : class, IEnterState
        {
            if (_currentState is IExitState exitState)
            {
                exitState.Exit();
            }

            var state = _states[typeof(TState)];

            _currentState = state;

            state.Enter();
        }
    }
}
