using Game.CodeBase.Infrastructure.States;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [Required, SerializeField] private Game _game;

        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<Game>()
                     .FromComponentInNewPrefab(_game)
                     .AsSingle()
                     .NonLazy();
        }
    }
}
