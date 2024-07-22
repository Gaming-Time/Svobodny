using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Modules.UI.MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Levels to load")] [SerializeField]
        private string newGameLevel;

        [Header("Graphics menu")] [SerializeField]
        private TMP_Dropdown resolutionDropDown;

        [SerializeField]
        private Toggle fullScreenToggle;

        private List<Resolution> _resolutions;
        private RefreshRate _currentRefreshRate;
        private int _currentResolutionIndex;

        private Resolution _chosenResolution;
        private bool _isFullScreen;

        private GameStateMachine _gameStateMachine;
        private string _levelToLoad;

        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            InitializeResolutionsDropdown();
        }

        private void InitializeResolutionsDropdown()
        {
            resolutionDropDown.ClearOptions();
            _currentRefreshRate = Screen.currentResolution.refreshRateRatio;
            _chosenResolution = Screen.currentResolution;
            _isFullScreen = Screen.fullScreen;

            fullScreenToggle.isOn = _isFullScreen;
            _resolutions = Screen.resolutions
                .Where(resolution => resolution.refreshRateRatio.value == _currentRefreshRate.value)
                .ToList();

            var resolutionOptions = new List<string>();
            foreach (var resolution in _resolutions)
            {
                resolutionOptions.Add($"{resolution.width}x{resolution.height} {resolution.refreshRateRatio} Hz");
                
                if(resolution.width == Screen.width && resolution.height == Screen.height)
                    _currentResolutionIndex = _resolutions.IndexOf(resolution);
            }
            
            resolutionDropDown.AddOptions(resolutionOptions);
            resolutionDropDown.value = _currentResolutionIndex;
            resolutionDropDown.RefreshShownValue();
        }

        public void ChangeResolution(int resolutionIndex)
        {
            _chosenResolution = _resolutions[resolutionIndex];
        }

        public void ChangeFullScreen(bool isFullScreen) => _isFullScreen = isFullScreen;

        public void ApplyResolution() =>
            Screen.SetResolution(_chosenResolution.width, _chosenResolution.height, _isFullScreen);

        public void NewGameDialogYes()
        {
            PlayerPrefs.DeleteKey("Progress");
            _gameStateMachine.Enter<BootstrapState>();
        }

        public void LoadGameDialogYes()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }

        public void ExitButton()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}