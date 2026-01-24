using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.Factories;
using _Project.Develop.Runtime.Utilities.LoadScreen;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            DIContainer projectContainer = new DIContainer();
            ProjectContextRegistrations.Process(projectContainer);

            projectContainer.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(projectContainer));
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ProjectServicesFactory projectServicesFactory = new ProjectServicesFactory(container);

            ILoadingScreen loadingScreen = projectServicesFactory.GetStandardLoadingScreen();
            SceneSwitcherService sceneSwitcherService = projectServicesFactory.GetSceneSwitcherService();

            loadingScreen.Show();

            yield return projectServicesFactory.GetConfigsProviderService().LoadAsync();

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
