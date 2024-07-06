using System.Collections.Generic;
using Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Views;

namespace Sources.ObjectPools.Interfaces.Generic
{
    public interface IObjectPool<T> : IObjectPool
        where T : IView
    {
        IReadOnlyList<T> Collection { get; }

        void AddToCollection(T @object);
        bool Contains(T @object);
    }
}