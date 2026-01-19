using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Gameplay/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public SymbolsConfig SymbolsConfig { get; private set; }
        [field: SerializeField] public int SequenceLenght { get; private set; }
    }
}
