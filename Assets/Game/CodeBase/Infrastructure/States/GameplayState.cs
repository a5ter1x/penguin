using UnityEngine.SceneManagement;

namespace Game.CodeBase.Infrastructure.States
{
    public class GameplayState : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;

        public GameplayState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
