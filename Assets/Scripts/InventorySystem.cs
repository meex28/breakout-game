using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Serializable]
    public class ItemEntry
    {
        public Items item;
        public GameObject gameObject;
    }

    [Header("General Fields")]
    public Dictionary<Items, List<GameObject>> itemsMap = new Dictionary<Items, List<GameObject>>();
    private List<GameObject> pickedItems = new List<GameObject>(); // use it to set it active after death
    public bool isOpen;
    [Header("UI Items Section")]
    public GameObject ui_Window;
    public List<Image> itemsImages = new List<Image>();
    public List<Text> itemsQuantityText = new List<Text>();
    public List<Text> itemsSlotNumberText = new List<Text>();

    void Awake()
    {
        ClearInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        CheckItemUsage();
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);

        Update_UI();
    }

    public void PickUp(GameObject item)
    {
        Items itemType = item.GetComponent<Item>().item;
        if (!itemsMap.ContainsKey(itemType))
        {
            itemsMap[itemType] = new List<GameObject>();
        }
        itemsMap[itemType].Add(item);
        pickedItems.Add(item);
        Debug.Log(itemsMap[itemType].Count);
        Update_UI();
    }

    void Update_UI()
    {
        HideAll();
        var slotIndex = 0;
        foreach (var itemEntry in itemsMap.Values)
        {
            if (itemEntry.Count > 0) {
                var itemToDisplay = itemEntry[0];
                SpriteRenderer spriteRenderer = itemToDisplay.GetComponent<SpriteRenderer>();
                Sprite sprite = spriteRenderer.sprite;
                float aspectRatio = sprite.rect.width / sprite.rect.height;
                RectTransform rectTransform = itemsImages[slotIndex].GetComponent<RectTransform>();
                float newSizeX = rectTransform.rect.height * aspectRatio;
                float newSizeY = rectTransform.rect.height;
                rectTransform.sizeDelta = new Vector2(newSizeX, newSizeY);
                itemsImages[slotIndex].sprite = sprite;
                itemsImages[slotIndex].gameObject.SetActive(true);

                itemsQuantityText[slotIndex].text = "x" + itemEntry.Count.ToString();
                itemsSlotNumberText[slotIndex].text = (slotIndex + 1).ToString();
            }
            slotIndex++;
        }
    }

    void HideAll()
    {
        foreach (var i in itemsImages) { i.gameObject.SetActive(false); }
        foreach (var i in itemsQuantityText) { i.text = ""; }
        foreach (var i in itemsSlotNumberText) { i.text = ""; }
    }

    private void CheckItemUsage()
    {
        Items[] itemsKeyCodeMapping = { Items.ENERGY_DRINK, Items.CAMO_SHIRT, Items.DONUT, Items.KEY, Items.PICKLOCK };
        KeyCode[] keyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                UseItem(itemsKeyCodeMapping[i]);
            }
        }
    }

    private void UseItem(Items item)
    {
        Debug.Log("Use item " + item);
        if (!IncludesItem(item))
        {
            Debug.Log(item + " not found in inventory!");
            return;
        }
        switch (item)
        {
            case Items.ENERGY_DRINK:
                Debug.Log("Boosting player speed!");
                GameObject.FindWithTag("Player").GetComponent<Player>().BoostSpeed();
                RemoveItem(item);
                break;
            case Items.CAMO_SHIRT:
                Debug.Log("Wearing camo shirt!");
                GameObject.FindWithTag("Player").GetComponent<Player>().UseCamoShirt();
                RemoveItem(item);
                break;
            default:
                break;
        }
    }

    public void ClearInventory()
    {
        itemsMap.Clear();

        // initialize and set order of inventory
        itemsMap[Items.ENERGY_DRINK] = new List<GameObject>();
        itemsMap[Items.CAMO_SHIRT] = new List<GameObject>();
        itemsMap[Items.DONUT] = new List<GameObject>();
        itemsMap[Items.KEY] = new List<GameObject>();
        itemsMap[Items.PICKLOCK] = new List<GameObject>();

        pickedItems.ForEach(item => item.SetActive(true));
        pickedItems.Clear();

        Update_UI();
    }

    public void RemoveItem(Items item)
    {
        itemsMap[item].RemoveAt(0);
        Update_UI();
    }

    public bool IncludesItem(Items searchedItem)
    {
        return itemsMap.ContainsKey(searchedItem) && itemsMap[searchedItem].Count > 0;
    }
}