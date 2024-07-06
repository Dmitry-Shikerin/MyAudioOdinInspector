using System;

namespace Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Triggers
{
    public interface IEnteredTrigger<out T>
    {
        public event Action<T> Entered;
    }
}