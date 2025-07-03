using Game.CodeBase.Infrastructure.Commands;
using Game.CodeBase.Models;
using Game.CodeBase.Presenters;
using Game.CodeBase.Views;
using Game.CodeBase.Views.Components;
using Game.CodeBase.Views.UserInterface;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.CodeBase.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [Required, SerializeField] private AudioPlayer _audioPlayer;
        [Required, SerializeField] private PlayerSidePoints _playerSidePoints;
        [Required, SerializeField] private GameUI _gameUI;

        [Required, SerializeField] private IceBallTowerView _iceBallTowerView;
        [Required, SerializeField] private PenguinView _penguinView;
        [Required, SerializeField] private RunView _runView;

        public override void InstallBindings()
        {
            Container.Bind<AudioPlayer>().FromInstance(_audioPlayer).AsSingle().NonLazy();

            Container.Bind<PlayerSidePoints>().FromInstance(_playerSidePoints).AsSingle().NonLazy();

            Container.Bind<IRestartCommand>().To<RestartCommand>().AsTransient().NonLazy();
            Container.Bind<GameUI>().FromInstance(_gameUI).NonLazy();

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
