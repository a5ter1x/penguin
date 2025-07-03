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
        [Required, SerializeField] private GameUI _gameUI;
        [Required, SerializeField] private IceBallTowerView _iceBallTowerView;
        [Required, SerializeField] private PlayerSidePoints _playerSidePoints;
        [Required, SerializeField] private RunView _runView;

        [Required, SerializeField] private AudioPlayer _audioPlayerPrefab;
        [Required, SerializeField] private DashAnimationPlayer _dashAnimationPlayerPrefab;
        [Required, SerializeField] private PenguinView _penguinViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<AudioPlayer>().FromComponentInNewPrefab(_audioPlayerPrefab).AsSingle();
            Container.Bind<DashAnimationPlayer>().FromComponentInNewPrefab(_dashAnimationPlayerPrefab).AsSingle();
            Container.Bind<PlayerSidePoints>().FromInstance(_playerSidePoints).AsSingle();

            Container.Bind<IRestartCommand>().To<RestartCommand>().AsTransient();
            Container.Bind<GameUI>().FromInstance(_gameUI);

            Container.Bind<IceBallTower>().AsSingle();
            Container.Bind<IceBallTowerView>().FromInstance(_iceBallTowerView).AsSingle();
            Container.Bind<IceBallTowerPresenter>().AsSingle().NonLazy();

            Container.Bind<Penguin>().AsSingle();
            Container.Bind<PenguinView>().FromComponentInNewPrefab(_penguinViewPrefab).AsSingle();
            Container.Bind<PenguinPresenter>().AsSingle().NonLazy();

            Container.Bind<Run>().AsSingle();
            Container.Bind<RunView>().FromInstance(_runView).AsSingle();
            Container.Bind<RunPresenter>().AsSingle().NonLazy();
        }
    }
}
