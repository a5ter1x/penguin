using Game.CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        public IGameStateMachine StateMachine { get; private set; }

        [Inject]
        public void Construct(IGameStateMachine stateMachine)
        {
        }
    }
}
