using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.Utilities.Factories
{
    public class GameplayServicesFactory
    {
        private readonly DIContainer _container;

        public GameplayServicesFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayPlayerInputs GetGameplayPlayerInputs() => _container.Resolve<GameplayPlayerInputs>();
        public SymbolsSequenceGenerator GetSymbolsSequenceGenerator() => _container.Resolve<SymbolsSequenceGenerator>();
        public InputStringReader GetInputStringReader() => _container.Resolve<InputStringReader>();
    }
}
