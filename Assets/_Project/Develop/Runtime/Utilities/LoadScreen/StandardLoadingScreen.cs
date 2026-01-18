using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.LoadScreen
{
    public class StandardLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        public bool IsShowing => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
