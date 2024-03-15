using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.ButtonMediator
{
    public class ButtonMediator : IButtonMediator
    {
        private Dictionary<ButtonType, Action> _actions;

        public event Action ExitToMenuEvent;
        public event Action RestartEvent;

        public ButtonMediator()
        {
            _actions = new Dictionary<ButtonType, Action>
            {
                { ButtonType.Restart, RestartButtonPressed },
                { ButtonType.ExitToMenu, ExitToMenuButtonPressed },
            };
        }

        public void CallMethod(ButtonType buttonType)
        {
            if(!_actions.TryGetValue(buttonType, out var method))
                return;
            
            method.Invoke();
        }

        private void ExitToMenuButtonPressed()
        {
            ExitToMenuEvent?.Invoke();
        }

        private void RestartButtonPressed()
        {
            RestartEvent?.Invoke();
        }
    }

    public enum ButtonType
    {
        Restart,
        ExitToMenu,
    }
}