using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Modules.Inventory.Essentials
{
    public class EssentialSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI amountText;

        public int Amount => int.Parse(amountText.text);

        public void Construct(Sprite sprite, int amount)
        {
            image.sprite = sprite;
            amountText.text = amount.ToString();
        }

        public void AddAmount(int amount)
        {
            var currentAmount = int.Parse(amountText.text);
            amountText.text = (currentAmount + amount).ToString();
        }

        public void ReduceAmount(int amount)
        {
            var currentAmount = int.Parse(amountText.text);
            amountText.text = (currentAmount - amount).ToString();
        }
    }
}