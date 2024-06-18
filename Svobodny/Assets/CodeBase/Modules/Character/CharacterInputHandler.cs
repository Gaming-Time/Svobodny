using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.WindowService;
using UnityEngine;

namespace CodeBase.Modules.Character
{
    public class CharacterInputHandler : MonoBehaviour
    {
        private IWindowService _windowService;
        private IInputService _inputService;

        private bool _isGameReady;

        public void Construct(IWindowService windowService, IInputService inputService)
        {
            _windowService = windowService;
            _inputService = inputService;
        }

        private void Update()
        {
            if(!_isGameReady)
                return;
            if(_inputService.IsEscapeButtonDown())
                _windowService.OpenOrCreateWindow(WindowID.PauseWindow);
        }

        public void GameReady()
        {
            _isGameReady = true;
        }
    }
}