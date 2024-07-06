using Sources.MVPPassiveView.Presentations.Implementation.Views;

namespace Sources.ObjectPools.Interfaces.Destroyers
{
    public interface IPODestroyerService
    {
        public void Destroy<T>(T view)
            where T : View;
    }
}