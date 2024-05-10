using UnityEngine;
using UnityEngine.VFX;

namespace CodeBase.Modules.Enemies.VFX
{
    public class EnemyVFXController : MonoBehaviour
    {
        [SerializeField] private VisualEffect bloodEffect;
        [SerializeField] private VisualEffect sliceEffect;

        public void PlayBlood(Vector3 direction)
        {
            var originalRotation = bloodEffect.transform.rotation.eulerAngles.x;
            bloodEffect.transform.forward = direction;
            var currentRotation = bloodEffect.transform.rotation.eulerAngles;
            bloodEffect.transform.rotation =
                Quaternion.Euler(new Vector3(originalRotation, currentRotation.y, currentRotation.z));
            bloodEffect.Play();
        }

        public void PlaySlice() => sliceEffect.Play();
    }
}