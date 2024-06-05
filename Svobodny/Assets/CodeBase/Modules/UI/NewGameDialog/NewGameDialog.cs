using System;
using CodeBase.Modules.UI.MainMenu;
using UnityEngine;

namespace CodeBase.Modules.UI.NewGameDialog
{
    public class NewGameDialog : MonoBehaviour
    {
        [SerializeField] private GameObject[] frames;
        [SerializeField] private MenuController menuController;

        private int _currentFrameIndex;

        private void OnEnable()
        {
            _currentFrameIndex = 0;
            frames[_currentFrameIndex].SetActive(true);
        }

        private void Update()
        {
            if(Input.anyKeyDown)
                OpenNextWindow();
        }

        private void OpenNextWindow()
        {
            frames[_currentFrameIndex].SetActive(false);
            
            _currentFrameIndex++;

            if (_currentFrameIndex >= frames.Length)
            {
                menuController.NewGameDialogYes();
                return;
            }
            
            frames[_currentFrameIndex].SetActive(true);
        }
    }
}