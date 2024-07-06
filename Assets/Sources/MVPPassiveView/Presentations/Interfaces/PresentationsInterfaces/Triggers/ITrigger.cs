﻿namespace Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Triggers
{
    public interface ITrigger<out T> : IEnteredTrigger<T>, IExitedTrigger<T>
    {
    }
}