using System.Collections;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private ProjectServicesFactory _projectServicesFactory;
        private ObjectsUpdater _objectsUpdater;
        private SelectGameModeArgsService _selectGameModeArgsService;
        private bool _isRun;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _projectServicesFactory = new ProjectServicesFactory(_container);
            MainMenuServicesFactory mainMenuServicesFactory = new MainMenuServicesFactory(_container);

            _objectsUpdater = _projectServicesFactory.GetObjectsUpdater();
            MainMenuPlayerInputs mainMenuPlayerInputs = mainMenuServicesFactory.GetMainMenuPlayerInputs();
            _selectGameModeArgsService = mainMenuServicesFactory.GetSelectGameModeArgsService();

            _selectGameModeArgsService.GameModeSelected += OnSelectedGameModeArgs;

            _objectsUpdater.Add(mainMenuPlayerInputs);

            yield return null;
        }

        public override void Run()
        {
            _isRun = true;
            Debug.Log("Выбрать режим игры: 1 - сгенерировать цифры, 2 - сгенерировать буквы");
        }

        private void Update()
        {
            if (_isRun == false)
                return;

            _objectsUpdater.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _selectGameModeArgsService.GameModeSelected -= OnSelectedGameModeArgs;
            _selectGameModeArgsService.Dispose();
        }

        private void OnSelectedGameModeArgs(GameplayInputArgs args)
        {
            SceneSwitcherService sceneSwitcherService = _projectServicesFactory.GetSceneSwitcherService();
            ICoroutinesPerformer coroutinesPerformer = _projectServicesFactory.GetCoroutinesPerformer();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, args));
        }
    }
}
