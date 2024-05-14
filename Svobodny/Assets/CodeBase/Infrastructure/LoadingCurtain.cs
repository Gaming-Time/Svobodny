using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup Curtain;
        public bool IsHidden { get; private set; }
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
            IsHidden = false;
        }

        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            Debug.Log(Time.timeScale);
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            IsHidden = true;
            gameObject.SetActive(false);
        }
    }
}