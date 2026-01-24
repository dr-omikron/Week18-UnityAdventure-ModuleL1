using System;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.PlayerInput;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Inputs
{
    public class GameplayPlayerInputs : IUpdatable
    {
        public event Action EndGameKeyDown;

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyboardInputKeys.EndGameKey))
                EndGameKeyDown?.Invoke();
        }
    }
}
