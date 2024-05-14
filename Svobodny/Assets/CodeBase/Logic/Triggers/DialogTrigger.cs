using CodeBase.Infrastructure.Services.WindowService;
using UnityEngine;

namespace CodeBase.Logic.Triggers
{
    public class DialogTrigger : MonoBehaviour
    {
        private IWindowService _windowService;
        
        public WindowID windowId;

        public void Construct(IWindowService windowService) => _windowService = windowService;

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player"))
                return;
            
            _windowService.OpenOrCreateWindow(windowId);
        }
    }
}