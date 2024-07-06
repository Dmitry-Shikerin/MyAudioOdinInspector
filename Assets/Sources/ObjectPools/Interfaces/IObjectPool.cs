using System;
using Sources.MVPPassiveView.Presentations.Implementation.Views;
using Sources.ObjectPools.Implementation.Objects;

namespace Sources.ObjectPools.Interfaces
{
    public interface IObjectPool
    {
        event Action<int> ObjectCountChanged;
        
        T Get<T>()
            where T : View;
        void Return(PoolableObject poolableObject);
        void PoolableObjectDestroyed(PoolableObject poolableObject);
    }
}