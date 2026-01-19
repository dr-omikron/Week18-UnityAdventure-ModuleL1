using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private ObjectsUpdater _objectsUpdater;
        private GameplayPlayerInputs _gameplayPlayerInputs;
        private ICoroutinesPerformer _coroutinesPerformer;
        private GameCycle _gameCycle;
        private bool _isRun;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)}");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            _objectsUpdater = _container.Resolve<ObjectsUpdater>();
            _gameplayPlayerInputs = _container.Resolve<GameplayPlayerInputs>();
            _objectsUpdater.Add(_gameplayPlayerInputs);
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            SymbolsSequenceGenerator symbolsSequenceGenerator = _container.Resolve<SymbolsSequenceGenerator>();
            InputStringReader inputStringReader = _container.Resolve<InputStringReader>();
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();

            _gameCycle = new GameCycle(
                symbolsSequenceGenerator,
                inputStringReader,
                _gameplayPlayerInputs,
                _coroutinesPerformer,
                sceneSwitcherService,
                _inputArgs.Symbols,
                _inputArgs.SequenceLenght);

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
            _gameCycle.Dispose();
        }

        public override void Run()
        {
            _isRun = true;
            Debug.Log("Gameplay running...");
            _coroutinesPerformer.StartPerform(_gameCycle.Start());
        }
    }
}
