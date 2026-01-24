using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.PlayerInput;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameCycle : IDisposable
    {
        private readonly GameplayServicesFactory _gameplayServicesFactory;
        private readonly GameplayPlayerInputs _gameplayPlayerInputs;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly GameplayInputArgs _inputArgs;

        public GameCycle(
            GameplayServicesFactory gameplayServicesFactory, 
            GameplayPlayerInputs gameplayPlayerInputs, 
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService,
            GameplayInputArgs inputArgs)
        {
            _gameplayServicesFactory = gameplayServicesFactory;
            _gameplayPlayerInputs = gameplayPlayerInputs;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _inputArgs = inputArgs;
        }

        public IEnumerator Start()
        {
            SymbolsSequenceGenerator symbolsSequenceGenerator = _gameplayServicesFactory.GetSymbolsSequenceGenerator();
            string generated = symbolsSequenceGenerator.Generate(_inputArgs.Symbols, _inputArgs.SequenceLenght);

            Debug.Log($"Retry symbols sequence - { generated }");

            InputStringReader inputStringReader = _gameplayServicesFactory.GetInputStringReader();
            yield return _coroutinesPerformer.StartPerform(inputStringReader.StartProcess(_inputArgs.SequenceLenght));

            if (string.Equals(inputStringReader.CurrentInput, generated, StringComparison.OrdinalIgnoreCase))
                ProcessWin();
            else
                ProcessDefeat();
        }

        private void ProcessWin()
        {
            Debug.Log("Win");
            Debug.Log($"Press { KeyboardInputKeys.EndGameKey } to Return in Main Menu");
            _gameplayPlayerInputs.EndGameKeyDown += OnMainMenuReturn;
        }

        private void ProcessDefeat()
        {
            Debug.Log("Defeat");
            Debug.Log($"Press { KeyboardInputKeys.EndGameKey } to Restart Game");
            _gameplayPlayerInputs.EndGameKeyDown += OnRestartGame;
        }

        private void OnMainMenuReturn()
        {
            UnsubscribeAll();
            _coroutinesPerformer.StartPerform(ReturnToMainMenu());
        }

        private void OnRestartGame()
        {
            UnsubscribeAll();
            _coroutinesPerformer.StartPerform(Start());
        }

        private IEnumerator ReturnToMainMenu()
        {
            yield return _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }

        private void UnsubscribeAll()
        {
            _gameplayPlayerInputs.EndGameKeyDown -= OnMainMenuReturn;
            _gameplayPlayerInputs.EndGameKeyDown -= OnRestartGame;
        }

        public void Dispose() => UnsubscribeAll();
    }
}
