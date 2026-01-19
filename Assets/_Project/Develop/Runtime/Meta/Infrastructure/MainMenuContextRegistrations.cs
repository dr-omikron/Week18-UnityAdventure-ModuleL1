using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateObjectsUpdater);
            container.RegisterAsSingle(CreateMainMenuPlayerInputs);
        }

        private static ObjectsUpdater CreateObjectsUpdater(DIContainer c) => new ObjectsUpdater();
        private static MainMenuPlayerInputs CreateMainMenuPlayerInputs(DIContainer c) => new MainMenuPlayerInputs();
    }
}
