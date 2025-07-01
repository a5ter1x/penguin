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
        [Required, SerializeField] private Penguin _penguinPrefab;
        [Required, SerializeField] private GameplayUI _gameplayUI;
        [Required, SerializeField] private LevelPoints _levelPoints;

        public override void InstallBindings()
        {
            Container
                .Bind<Penguin>()
                .FromComponentInNewPrefab(_penguinPrefab)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<GameplayUI>()
                .FromInstance(_gameplayUI)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LevelPoints>()
                .FromInstance(_levelPoints)
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
