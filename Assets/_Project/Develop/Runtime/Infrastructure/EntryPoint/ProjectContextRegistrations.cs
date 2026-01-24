using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.AssetsManagement;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using _Project.Develop.Runtime.Utilities.LoadScreen;
using _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            container.RegisterAsSingle(CreateConfigsProviderService);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            container.RegisterAsSingle(CreateObjectsUpdater);
        }

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = 
                resourcesAssetsLoader.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

            return Object.Instantiate(coroutinesPerformerPrefab);
        }

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);
            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c) => new SceneLoaderService();

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c) 
            => new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandardLoadingScreen standardLoadingScreenPrefab = 
                resourcesAssetsLoader.Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

            return Object.Instantiate(standardLoadingScreenPrefab);
        }

        private static ObjectsUpdater CreateObjectsUpdater(DIContainer c) => new ObjectsUpdater();
    }
}
