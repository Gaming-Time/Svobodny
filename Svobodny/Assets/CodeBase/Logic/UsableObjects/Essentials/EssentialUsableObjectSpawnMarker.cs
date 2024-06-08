using UnityEngine;

namespace CodeBase.Logic.UsableObjects.Essentials
{
    public class EssentialUsableObjectSpawnMarker : MonoBehaviour
    {
        public EssentialType EssentialType;
        public int Amount;
    }

    public enum EssentialType
    {
        Bullet,
        Medicine,
    }
}