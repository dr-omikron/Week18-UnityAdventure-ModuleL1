using _Project.Develop.Runtime.Types;
using _Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameplayMode gameplayMode)
        {
            GameplayMode = gameplayMode;
        }

        public GameplayMode GameplayMode  { get; }
    }
}
