using Game.CodeBase.Infrastructure.Services.Input;
using Game.CodeBase.Infrastructure.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.CodeBase.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [Required, SerializeField] private Game _game;
        [Required, SerializeField] private InputActionAsset _inputAsset;

        public override void InstallBindings()
        {
            Container.Bind<InputActionAsset>().FromInstance(_inputAsset).AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<Game>()
                     .FromComponentInNewPrefab(_game)
                     .AsSingle()
                     .NonLazy();
        }
    }
}
