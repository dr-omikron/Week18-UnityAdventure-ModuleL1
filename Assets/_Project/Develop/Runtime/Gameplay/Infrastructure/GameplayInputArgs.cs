using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(List<char> symbols, int sequenceLenght)
        {
            SequenceLenght = sequenceLenght;
            Symbols = new List<char>(symbols);
        }
        public List<char> Symbols { get; }
        public int SequenceLenght { get; }
    }
}
