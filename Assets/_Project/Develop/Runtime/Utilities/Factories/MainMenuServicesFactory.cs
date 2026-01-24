using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Infrastructure;

namespace _Project.Develop.Runtime.Utilities.Factories
{
    public class MainMenuServicesFactory
    {
        private readonly DIContainer _container;

        public MainMenuServicesFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuPlayerInputs GetMainMenuPlayerInputs() => _container.Resolve<MainMenuPlayerInputs>();
        public SelectGameModeArgsService GetSelectGameModeArgsService() => _container.Resolve<SelectGameModeArgsService>();
    }
}
