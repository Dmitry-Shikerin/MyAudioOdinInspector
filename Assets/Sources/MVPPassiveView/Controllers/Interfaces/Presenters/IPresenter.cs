using Sources.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.MVPPassiveView.Controllers.Interfaces.Presenters
{
    public interface IPresenter : IInitialize, IEnable, IDisable, IDestroy
    {
    }
}