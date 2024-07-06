using Sources.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Core.Services.Common
{
    public interface IViewService : IAwake, IEnable, IDisable, IDestroy
    {
    }
}