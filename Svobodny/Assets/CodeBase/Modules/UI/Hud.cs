using CodeBase.Modules.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.Modules.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Transform upSlotContainer;
        [SerializeField] private Transform centralSlotContainer;
        [SerializeField] private Transform downSlotContainer;
        [SerializeField] private Transform inactiveSlotsContainer;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private AnimatorController animatorController;

        [SerializeField] private Transform knifeSlotContainer;
        [SerializeField] private Transform pistolSlotContainer;
        [SerializeField] private Transform bulletSlotContainer;
        [SerializeField] private Transform medicineSlotContainer;
        [SerializeField] private Slider healthSlider;

        public Transform UpSlotContainer => upSlotContainer;
        public Transform CentralSlotContainer => centralSlotContainer;
        public Transform DownSlotContainer => downSlotContainer;
        public TextMeshProUGUI Description => description;
        public AnimatorController AnimatorController => animatorController;

        public Transform KnifeSlotContainer => knifeSlotContainer;
        public Transform PistolSlotContainer => pistolSlotContainer;
        public Transform BulletSlotContainer => bulletSlotContainer;
        public Transform MedicineSlotContainer => medicineSlotContainer;
        public Slider HealthSlider => healthSlider;
    }
}