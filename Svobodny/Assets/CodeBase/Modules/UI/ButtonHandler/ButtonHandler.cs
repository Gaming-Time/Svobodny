using CodeBase.Infrastructure.Services.ButtonMediator;
using UnityEngine;

namespace CodeBase.Modules.UI.ButtonHandler
{
    public class ButtonHandler : MonoBehaviour
    {
        private IButtonMediator _buttonMediator;

        [SerializeField] private ButtonType buttonType;

        public void Construct(IButtonMediator buttonMediator)
        {
            _buttonMediator = buttonMediator;
        }

        public void InvokeEvent()
        {
            _buttonMediator.CallMethod(buttonType);
        }
    }
}