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

        public virtual void Activate() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);
    }
}