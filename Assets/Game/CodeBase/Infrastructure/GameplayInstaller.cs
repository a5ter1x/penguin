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
        [Required, SerializeField] private PlayerSidePoints _playerSidePoints;
        [Required, SerializeField] private GameplayUI _gameplayUI;

        [Required, SerializeField] private IceBallTowerView _iceBallTowerView;
        [Required, SerializeField] private PenguinView _penguinView;
        [Required, SerializeField] private RunView _runView;

        public override void InstallBindings()
        {
            Container.Bind<PlayerSidePoints>().FromInstance(_playerSidePoints).AsSingle().NonLazy();

            Container.Bind<GameplayUI>().FromInstance(_gameplayUI).AsSingle().NonLazy();

            Container.Bind<IceBallTower>().AsSingle();
            Container.Bind<IceBallTowerView>().FromInstance(_iceBallTowerView).AsSingle().NonLazy();
            Container.Bind<IceBallTowerPresenter>().AsSingle().NonLazy();

            Container.Bind<Penguin>().AsSingle();
            Container.Bind<PenguinView>().FromInstance(_penguinView).AsSingle().NonLazy();
            Container.Bind<PenguinPresenter>().AsSingle().NonLazy();

            Container.Bind<Run>().AsSingle();
            Container.Bind<RunView>().FromInstance(_runView).AsSingle().NonLazy();
            Container.Bind<RunPresenter>().AsSingle().NonLazy();
        }
    }
}
