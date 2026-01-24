using System;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.PlayerInput;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuPlayerInputs : IUpdatable
    {
        public event Action LoadCharactersModeKeyDown;
        public event Action LoadNumbersModeKeyDown;

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyboardInputKeys.LoadCharactersModeKey))
                LoadCharactersModeKeyDown?.Invoke();

            if(Input.GetKeyDown(KeyboardInputKeys.LoadNumbersModeKey))
                LoadNumbersModeKeyDown?.Invoke();
        }
    }
}
