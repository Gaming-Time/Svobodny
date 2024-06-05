using System;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Windows
{
    public class DialogWindow : WindowBase
    {
        [SerializeField] private List<GameObject> phrases;

        private int _currentPhraseIndex;
        
        public override void Activate()
        {
            base.Activate();
            Time.timeScale = 0;

            _currentPhraseIndex = 0;
            ShowCurrentPhrase();
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
        }

        public void OnNextPhraseButtonDown()
        {
            HideCurrentPhrase();
            _currentPhraseIndex++;
            if (_currentPhraseIndex >= phrases.Count)
            {
                Hide();
                return;
            }
            ShowCurrentPhrase();
        }

        private void HideCurrentPhrase() => phrases[_currentPhraseIndex].SetActive(false);
        private void ShowCurrentPhrase()
        {
            phrases[_currentPhraseIndex].SetActive(true);
        }
    }
}