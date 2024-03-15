using System;

namespace CodeBase.Infrastructure.Services.ButtonMediator
{
    public interface IButtonMediator : IService
    {
        event Action ExitToMenuEvent;
        event Action RestartEvent;
        void CallMethod(ButtonType buttonType);
    }
}