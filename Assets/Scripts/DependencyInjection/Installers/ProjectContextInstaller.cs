using Factories;
using Infrastructure.Services;
using Input;
using UnityEngine;
using Zenject;

namespace DependencyInjection.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private MonoService _monoService;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MonoService>().FromInstance(_monoService).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AssetProvider>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EventProvider.EventProvider>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DesktopInputService>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameFactory>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HudFactory>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}