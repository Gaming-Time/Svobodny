using System.Collections;
using UnityEngine;

namespace CodeBase.Windows
{
    public class DoorWindow : WindowBase
    {
        [SerializeField] private float stayActiveTime;
        [SerializeField] private CanvasGroup canvasGroup;

        public override void Activate()
        {
            base.Activate();
            
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            StartCoroutine(WaitAndFadeOut());
        }
        
        private IEnumerator WaitAndFadeOut()
        {
            yield return new WaitForSeconds(stayActiveTime);
            
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            
            Hide();
        }
        
    }
}