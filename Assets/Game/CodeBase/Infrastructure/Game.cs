using Game.CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}
