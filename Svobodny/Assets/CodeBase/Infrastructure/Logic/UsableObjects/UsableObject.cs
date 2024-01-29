using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.UsableObjects
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class UsableObject : MonoBehaviour
    {
        protected abstract IInputService InputService { get; set; }
        private bool _isRequested;


        private void Update()
        {
            OnUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Character Trigger"))
                return;

            _isRequested = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Character Trigger"))
                return;

            _isRequested = false;
        }

        public abstract void Use();

        protected virtual void OnUpdate()
        {
            if (_isRequested && InputService.IsUseButtonDown())
                Use();
        }
    }
}