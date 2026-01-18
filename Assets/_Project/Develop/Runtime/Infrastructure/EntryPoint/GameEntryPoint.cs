using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            DIContainer container = new DIContainer();
            EntryPointRegistrations.Process(container);

            container.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(container));
        }

        private IEnumerator Initialize(DIContainer container)
        {
            yield return container.Resolve<ConfigsProviderService>().LoadAsync();
        }
    }
}
