using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Inputs;
using _Project.Develop.Runtime.Gameplay.Services;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.PlayerInput;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameCycle : IDisposable
    {
        private readonly SymbolsSequenceGenerator _symbolsSequenceGenerator;
        private readonly InputStringReader _inputStringReader;
        private readonly GameplayPlayerInputs _gameplayPlayerInputs;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly List<char> _symbols;
        private readonly int _sequenceLength;

        public GameCycle(
            SymbolsSequenceGenerator symbolsSequenceGenerator, 
            InputStringReader inputStringReader, 
            GameplayPlayerInputs gameplayPlayerInputs, 
            ICoroutinesPerformer coroutinesPerformer, 
            SceneSwitcherService sceneSwitcherService, 
            List<char> symbols, 
            int sequenceLength)
        {
            _symbolsSequenceGenerator = symbolsSequenceGenerator;
            _inputStringReader = inputStringReader;
            _gameplayPlayerInputs = gameplayPlayerInputs;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _symbols = new List<char>(symbols);
            _sequenceLength = sequenceLength;
        }

        public IEnumerator Start()
        {
            string generated = _symbolsSequenceGenerator.Generate(_symbols, _sequenceLength);

            Debug.Log($"Retry symbols sequence - { generated }");

            yield return _coroutinesPerformer.StartPerform(_inputStringReader.StartProcess(_sequenceLength));

            if (string.Equals(_inputStringReader.CurrentInput, generated, StringComparison.OrdinalIgnoreCase))
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
