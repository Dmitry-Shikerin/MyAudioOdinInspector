namespace Sources.ObjectPools.Interfaces.Objects
{
    public interface IPoolableObject
    {
        void SetPool(IObjectPool pool);
        void ReturnToPool();
    }
}