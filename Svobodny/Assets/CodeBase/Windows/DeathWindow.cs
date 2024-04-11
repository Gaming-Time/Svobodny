using UnityEngine;

namespace CodeBase.Windows
{
    public class DeathWindow : WindowBase
    {
        public override void Activate()
        {
            base.Activate();
            Time.timeScale = 0;
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
        }
    }
}