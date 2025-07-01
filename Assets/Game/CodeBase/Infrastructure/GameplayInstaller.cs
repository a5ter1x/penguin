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

        public override void InstallBindings()
        {
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
