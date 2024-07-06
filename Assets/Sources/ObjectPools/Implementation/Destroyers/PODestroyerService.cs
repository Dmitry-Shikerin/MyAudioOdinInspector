using System;
using Sources.MVPPassiveView.Presentations.Implementation.Views;
using Sources.ObjectPools.Implementation.Objects;
using Sources.ObjectPools.Interfaces.Destroyers;
using Object = UnityEngine.Object;

namespace Sources.ObjectPools.Implementation.Destroyers
{
    public class PODestroyerService : IPODestroyerService
    {
        public void Destroy<T>(T view) where T : View
        {
            try
            {
                var poolableObject = CheckPoolableObject(view);
                poolableObject.ReturnToPool();
                CheckPoolableObject(view);
                view.Hide();
            }
            catch (NullReferenceException)
            {
            }
        }

        private PoolableObject CheckPoolableObject<T>(T view) where T : View
        {
            if (view.TryGetComponent(out PoolableObject poolableObject))
                return poolableObject;

            Object.Destroy(view.gameObject);

            throw new NullReferenceException(nameof(poolableObject));
        }
    }
}