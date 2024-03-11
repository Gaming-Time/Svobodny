using UnityEngine;

namespace CodeBase.Windows
{
    public class DeathWindow : WindowBase
    {
        protected override void Activate()
        {
            base.Activate();
            Time.timeScale = 0;
        }

        protected override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
        }
    }
}