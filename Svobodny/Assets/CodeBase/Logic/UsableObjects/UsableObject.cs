using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using UnityEngine;

namespace CodeBase.Logic.UsableObjects
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class UsableObject : MonoBehaviour
    {
        protected abstract IInputService InputService {  get; set; }
        protected abstract IWindowService WindowService { get; set; }
        private bool _isRequested;


        private void Update()
        {
            OnUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Character Trigger"))
                return;

            WindowService.OpenOrCreateWindow(WindowID.InteractionButton);
            _isRequested = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Character Trigger"))
                return;

            WindowService.CloseWindow(WindowID.InteractionButton);
            _isRequested = false;
        }

        protected virtual void Use()
        {
            WindowService.CloseWindow(WindowID.InteractionButton);   
        }

        protected virtual void OnUpdate()
        {
            if (_isRequested && InputService.IsUseButtonDown())
                Use();
        }
    }
}