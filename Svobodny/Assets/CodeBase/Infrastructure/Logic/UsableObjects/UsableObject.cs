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

        private void OnTriggerStay(Collider other)
        {
            Use();
        }

        public abstract void Use();

        protected virtual void OnUpdate()
        {
            _isRequested = InputService.IsUseButtonDown();
        }
    }
}