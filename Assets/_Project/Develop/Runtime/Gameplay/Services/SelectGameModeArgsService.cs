using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.EnumTypes;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Infrastructure;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace _Project.Develop.Runtime.Gameplay.Services
{
    public class SelectGameModeArgsService : IDisposable
    {
        public event Action<GameplayInputArgs> GameModeSelected;

        private readonly MainMenuPlayerInputs  _mainMenuPlayerInputs;
        private readonly ConfigsProviderService _configsProviderService;

        public SelectGameModeArgsService(MainMenuPlayerInputs mainMenuPlayerInputs, ConfigsProviderService configsProviderService)
        {
            _mainMenuPlayerInputs = mainMenuPlayerInputs;
            _configsProviderService = configsProviderService;

            _mainMenuPlayerInputs.LoadNumbersModeKeyDown += OnLoadNumbersMode;
            _mainMenuPlayerInputs.LoadCharactersModeKeyDown += OnLoadCharacterMode;
        }

        private void OnLoadCharacterMode() => SelectGameModeArgs(GameplayMode.Characters);
        private void OnLoadNumbersMode() => SelectGameModeArgs(GameplayMode.Numbers);

        private void SelectGameModeArgs(GameplayMode gameplayMode)
        {
            LevelConfig config = GetLevelConfig();
            List<char> characters = config.SymbolsConfig.GetSymbolsBy(gameplayMode);
            GameModeSelected?.Invoke(new GameplayInputArgs(characters, config.SequenceLenght));
        }

        private LevelConfig GetLevelConfig() => _configsProviderService.GetConfig<LevelConfig>();

        public void Dispose()
        {
            _mainMenuPlayerInputs.LoadNumbersModeKeyDown -= OnLoadNumbersMode;
            _mainMenuPlayerInputs.LoadCharactersModeKeyDown -= OnLoadCharacterMode;
        }
    }
}
