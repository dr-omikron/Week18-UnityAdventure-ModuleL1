using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.ObjectsLifetimeManagement
{
    public class ObjectsUpdater
    {
        private readonly List<IUpdatable> _updatables =  new List<IUpdatable>();

        public void Add(IUpdatable updatable) => _updatables.Add(updatable);

        public void Update()
        {
            foreach (var updatable in _updatables)
                updatable.Update();
        }
    }
}
