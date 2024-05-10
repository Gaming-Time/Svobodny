using UnityEngine;
using UnityEngine.VFX;

namespace CodeBase.Modules.Character.VFX
{
    public class CharacterVFXController : MonoBehaviour
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