using TMPro;
using UnityEngine;

namespace CodeBase.Modules.Inventory
{
    public class ItemsInventory : MonoBehaviour
    {
        [SerializeField] private Transform upSlotContainer;
        [SerializeField] private Transform centralSlotContainer;
        [SerializeField] private Transform downSlotContainer;
        [SerializeField] private Transform inactiveSlotsContainer;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private AnimatorController animatorController;

        public Transform UpSlotContainer => upSlotContainer;
        public Transform CentralSlotContainer => centralSlotContainer;
        public Transform DownSlotContainer => downSlotContainer;
        public TextMeshProUGUI Description => description;
        public AnimatorController AnimatorController => animatorController;
    }
}