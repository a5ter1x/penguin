using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.CodeBase.Infrastructure.States
{
    public class BootstrapState : IEnterState, IExitState
    {
        private const string GameSceneName = "GameplayScene";

        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            LoadScene(name: GameSceneName, onLoaded: EnterLoadLevel).Forget();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<GameplayState>();
        }

        private async UniTask LoadScene(string name, Action onLoaded = null)
        {
            if (string.Equals(SceneManager.GetActiveScene().name, name, StringComparison.Ordinal))
            {
                onLoaded?.Invoke();
                return;
            }

            var loading = SceneManager.LoadSceneAsync(name);

            await loading.ToUniTask();

            onLoaded?.Invoke();
        }
    }
}
