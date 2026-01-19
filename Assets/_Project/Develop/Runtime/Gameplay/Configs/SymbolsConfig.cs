using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.EnumTypes;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "SymbolsConfig", menuName = "Configs/Gameplay/SymbolsConfig")]
    public class SymbolsConfig : ScriptableObject
    {
        [SerializeField] private List<SymbolsByMode> _symbols;

        [Serializable]
        private class SymbolsByMode
        {
            [field: SerializeField] public GameplayMode GameplayMode { get; private set; }
            [field: SerializeField] public List<char> Symbols { get; private set; }
        }

        public List<char> GetSymbolsBy(GameplayMode mode)
        {
            List<char> symbols = new List<char>();

            foreach (SymbolsByMode symbolsByMode in _symbols)
            {
                if (symbolsByMode.GameplayMode == mode)
                {
                    symbols = symbolsByMode.Symbols;
                    return symbols;
                }
            }

            throw new ArgumentException($"Symbols by {nameof(GameObject)} not found");
        }
    }
}
