using System;
using System.Collections.Generic;

namespace Game.CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine()
        {
        }
    }
}
