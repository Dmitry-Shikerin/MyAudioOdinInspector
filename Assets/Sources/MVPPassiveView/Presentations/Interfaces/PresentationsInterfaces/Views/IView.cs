using UnityEngine;

namespace Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Views
{
    public interface IView
    {
        void Show();
        void Hide();
        void SetParent(Transform parent);
        void SetLocalePosition(Vector3 position);
        void SetPosition(Vector3 position);
        void SetRotation(Vector3 rotation);
        public void SetRotation(Quaternion rotation);
        void Destroy();
    }
}