using UnityEngine;

namespace CodeBase.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private void OnEnable()
        {
            Enable();
        }

        protected virtual void Enable(){}

        protected virtual void Activate() => gameObject.SetActive(true);

        protected virtual void Hide() => gameObject.SetActive(false);
    }
}