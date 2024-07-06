using Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Views;

namespace Sources.MVPPassiveView.Presentations.Implementation.Views
{
    public class ContainerView : View
    {
        public void AppendChild(IView view) =>
            view.SetParent(transform);
    }
}
