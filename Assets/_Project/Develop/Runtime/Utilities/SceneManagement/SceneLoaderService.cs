using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Utilities.SceneManagement
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            yield return new WaitWhile(() => asyncOperation?.isDone == false);
        }

        public IEnumerator UnloadAsync(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            yield return new WaitWhile(() => asyncOperation?.isDone == false);
        }
    }
}
