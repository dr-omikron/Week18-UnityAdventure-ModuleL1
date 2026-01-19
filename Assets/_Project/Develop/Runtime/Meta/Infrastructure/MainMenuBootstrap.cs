using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.EnumTypes;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private ObjectsUpdater _objectsUpdater;
        private MainMenuPlayerInputs _mainMenuPlayerInputs;
        private bool _isRun;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _objectsUpdater = _container.Resolve<ObjectsUpdater>();
            _mainMenuPlayerInputs = _container.Resolve<MainMenuPlayerInputs>();
            _objectsUpdater.Add(_mainMenuPlayerInputs);

            _mainMenuPlayerInputs.LoadNumbersModeKeyDown += OnLoadNumbersMode;
            _mainMenuPlayerInputs.LoadCharactersModeKeyDown += OnLoadCharacterMode;

            yield return null;
        }

        private void Update()
        {
            if (_isRun == false)
                return;

            _objectsUpdater.Update();
        }

        private void OnDestroy()
        {
            _mainMenuPlayerInputs.LoadNumbersModeKeyDown -= OnLoadNumbersMode;
            _mainMenuPlayerInputs.LoadCharactersModeKeyDown -= OnLoadCharacterMode;
        }

        public override void Run()
        {
            _isRun = true;
            Debug.Log("Выбрать режим игры: 1 - сгенерировать цифры, 2 - сгенерировать буквы");
        }

        private void OnLoadCharacterMode()
        {
            LevelConfig config = GetLevelConfig();
            List<char> characters = config.SymbolsConfig.GetSymbolsBy(GameplayMode.Characters);
            SwitchSceneTo(Scenes.Gameplay, new GameplayInputArgs(characters, config.SequenceLenght));
        }

        private void OnLoadNumbersMode()
        {
            LevelConfig config = GetLevelConfig();
            List<char> characters = config.SymbolsConfig.GetSymbolsBy(GameplayMode.Numbers);
            SwitchSceneTo(Scenes.Gameplay, new GameplayInputArgs(characters, config.SequenceLenght));
        }

        private LevelConfig GetLevelConfig()
        {
            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
            return configsProviderService.GetConfig<LevelConfig>();
        }

        private void SwitchSceneTo(string sceneName, GameplayInputArgs args)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(sceneName, args));
        }

    }
}
