using Game.CodeBase.Models;
using Game.CodeBase.Presenters;
using Game.CodeBase.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [Required, SerializeField] private RunView _runViewPrefab;
        [Required, SerializeField] private IceBallTowerView _iceBallTowerView;
        [Required, SerializeField] private PenguinView _penguinView;
        [Required, SerializeField] private GameplayUI _gameplayUI;

        public override void InstallBindings()
        {
            Container
                .Bind<GameplayUI>()
                .FromInstance(_gameplayUI)
                .AsSingle()
                .NonLazy();

            Container.Bind<Penguin>().AsSingle();

            Container
                .Bind<PenguinView>()
                .FromComponentInNewPrefab(_penguinView)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<PenguinPresenter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<IceBallTower>().AsSingle();

            Container
                .Bind<IceBallTowerView>()
                .FromInstance(_iceBallTowerView)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<IceBallTowerPresenter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Run>().AsSingle();

            Container
                .Bind<RunView>()
                .FromComponentInNewPrefab(_runViewPrefab)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<RunPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}
