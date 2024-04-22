using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Modules.Inventory.Slots;
using TMPro;
using UnityEngine;

namespace CodeBase.Modules.Inventory
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private float fadeOutTime = 3f;

        private InventoryHandler _inventoryHandler;
        private IStaticDataService _staticData;
        private IAssets _assetProvider;
        private IUIFactory _uiFactory;

        private Dictionary<ItemType, Slot> _slots = new();
        private LinkedList<Slot> _inventorySlots = new();
        private Transform _scrollRectangleTransform;
        private Transform _upSlotContainer;
        private Transform _centralSlotContainer;
        private Transform _downSlotContainer;
        private Transform _inactiveSlotsContainer;
        private LinkedListNode<Slot> _activeSlot;
        private TextMeshProUGUI _description;
        private AnimatorController _inventoryAnimatorController;
        private Coroutine _fadeOutCoroutine;
        private WaitForSeconds _waitForFadeOutTime;

        public void Construct(InventoryHandler inventoryHandler, IStaticDataService staticData, IAssets assetProvider,
            IUIFactory uiFactory)
        {
            _inventoryHandler = inventoryHandler;
            _staticData = staticData;
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;

            Subscribe();
            Initialize();
        }

        private void Update()
        {
            if (_activeSlot == null)
                return;

            if (Input.mouseScrollDelta.y < 0)
            {
                SelectNextItem();
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                SelectPreviousItem();
            }
        }

        private void Initialize()
        {
            var inventoryRoot = _uiFactory.CreateItemsInventory();
            _scrollRectangleTransform = inventoryRoot.transform;
            var inventoryScript = inventoryRoot.GetComponent<ItemsInventory>();
            _upSlotContainer = inventoryScript.UpSlotContainer;
            _centralSlotContainer = inventoryScript.CentralSlotContainer;
            _downSlotContainer = inventoryScript.DownSlotContainer;
            _description = inventoryScript.Description;
            _inventoryAnimatorController = inventoryScript.AnimatorController;

            _waitForFadeOutTime = new WaitForSeconds(fadeOutTime);
        }

        public void Cleanup()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _inventoryHandler.ItemAdded += OnItemAdded;
            _inventoryHandler.ItemRemoved += OnItemRemoved;
        }

        private void UnSubscribe()
        {
            _inventoryHandler.ItemAdded -= OnItemAdded;
            _inventoryHandler.ItemRemoved -= OnItemRemoved;
        }

        private void SelectPreviousItem()
        {
            if (_activeSlot.Previous == null)
                return;

            if (_activeSlot.Next != null)
                _activeSlot.Next.Value.transform.SetParent(_inactiveSlotsContainer, false);

            _activeSlot.Value.transform.SetParent(_downSlotContainer, false);
            _activeSlot = _activeSlot.Previous;
            _activeSlot.Value.transform.SetParent(_centralSlotContainer, false);
            _description.text = _staticData.ForItem(_activeSlot.Value.ItemType).Description;
            _inventoryAnimatorController.OpenDescription();
            StartFadeoutCoroutine();

            if (_activeSlot.Previous == null)
                return;

            _activeSlot.Previous.Value.transform.SetParent(_upSlotContainer, false);
        }

        private void SelectNextItem()
        {
            if (_activeSlot.Next == null)
                return;

            if (_activeSlot.Previous != null)
                _activeSlot.Previous.Value.transform.SetParent(_inactiveSlotsContainer, false);

            _activeSlot.Value.transform.SetParent(_upSlotContainer, false);

            _activeSlot = _activeSlot.Next;
            _activeSlot.Value.transform.SetParent(_centralSlotContainer, false);
            _description.text = _staticData.ForItem(_activeSlot.Value.ItemType).Description;
            _inventoryAnimatorController.OpenDescription();
            StartFadeoutCoroutine();

            if (_activeSlot.Next == null)
                return;

            _activeSlot.Next.Value.transform.SetParent(_downSlotContainer, false);
        }
        
        private void OnItemAdded(ItemType itemType)
        {
            var slot = _inventorySlots.FirstOrDefault(slot => slot.ItemType == itemType);
            if (slot)
            {
                slot.AddItem(1);
                return;
            }

            var itemStaticData = _staticData.ForItem(itemType);
            slot = _assetProvider.Instantiate<Slot>(AssetPath.UIPath.Slot);
            var sprite = itemStaticData.Sprite;
            slot.Construct(itemType, sprite, 1);

            if (_activeSlot == null)
            {
                _activeSlot = _inventorySlots.AddLast(slot);
                _activeSlot.Value.transform.SetParent(_centralSlotContainer, false);
                _description.text = itemStaticData.Description;
                _inventoryAnimatorController.OpenDescription();
                StartFadeoutCoroutine();
                return;
            }

            if (_activeSlot.Next == null)
            {
                var element = _inventorySlots.AddLast(slot);
                element.Value.transform.SetParent(_downSlotContainer, false);
                return;
            }

            _inventorySlots.AddLast(slot);
        }

        private void OnItemRemoved(ItemType itemType)
        {
        }

        private void StartFadeoutCoroutine()
        {
            if (_fadeOutCoroutine != null)
            {
                StopCoroutine(_fadeOutCoroutine);
            }

            _fadeOutCoroutine = StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            yield return _waitForFadeOutTime;

            _inventoryAnimatorController.CloseDescription();
        }
    }
}