using Sources.MVPPassiveView.Presentations.Implementation.Views;
using Sources.ObjectPools.Interfaces;
using Sources.ObjectPools.Interfaces.Objects;

namespace Sources.ObjectPools.Implementation.Objects
{
    public class PoolableObject : View, IPoolableObject
    {
        private IObjectPool _pool;

        private void OnDestroy() =>
            _pool.PoolableObjectDestroyed(this);

        public void SetPool(IObjectPool pool) =>
            _pool = pool;

        public void ReturnToPool() =>
            _pool.Return(this);
    }
}