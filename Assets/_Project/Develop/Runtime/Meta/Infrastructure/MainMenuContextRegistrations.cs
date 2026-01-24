using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {

            container.RegisterAsSingle(CreateMainMenuPlayerInputs);
            container.RegisterAsSingle(CreateSelectGameModeService);
        }

        private static MainMenuPlayerInputs CreateMainMenuPlayerInputs(DIContainer c) => new MainMenuPlayerInputs();
        private static SelectGameModeArgsService CreateSelectGameModeService(DIContainer c)
        {
            MainMenuPlayerInputs mainMenuPlayerInputs = c.Resolve<MainMenuPlayerInputs>();
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            return new SelectGameModeArgsService(mainMenuPlayerInputs, configsProviderService);
        }
    }
}
