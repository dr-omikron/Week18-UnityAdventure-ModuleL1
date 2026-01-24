using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            container.RegisterAsSingle(CreateGameplayPlayerInputs);
            container.RegisterAsSingle(CreateSymbolsSequenceGenerator);
            container.RegisterAsSingle(CreateInputStringReader);
        }

        private static GameplayPlayerInputs CreateGameplayPlayerInputs(DIContainer c) => new GameplayPlayerInputs();
        private static SymbolsSequenceGenerator CreateSymbolsSequenceGenerator(DIContainer c) => new SymbolsSequenceGenerator();
        private static InputStringReader CreateInputStringReader(DIContainer c)
        {
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            return new InputStringReader(coroutinesPerformer);
        }
    }
}
