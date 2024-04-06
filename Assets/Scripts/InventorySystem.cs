using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    public List<GameObject> items = new List<GameObject>();
    public bool isOpen;
    [Header("UI Items Section")]
    public GameObject ui_Window;
    public List<Image> itemsImages = new List<Image>();
    [Header("UI Item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Title;
    public Text description_Text;

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
        items.Add(item);
        Update_UI();
    }

    void Update_UI()
    {
        HideAll();
        for (int i = 0; i < items.Count; i++)
        {
            SpriteRenderer spriteRenderer = items[i].GetComponent<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            RectTransform rectTransform = itemsImages[i].GetComponent<RectTransform>();
            float newSizeX = rectTransform.rect.height * aspectRatio;
            float newSizeY = rectTransform.rect.height;
            rectTransform.sizeDelta = new Vector2(newSizeX, newSizeY);
            itemsImages[i].sprite = sprite;
            itemsImages[i].gameObject.SetActive(true);
        }
    }

    void HideAll()
    {
        foreach (var i in itemsImages) { i.gameObject.SetActive(false); }

        // HideDescription();
    }

    public void ShowDescription(int id)
    {
        description_Image.sprite = itemsImages[id].sprite;
        description_Title.text = items[id].name;
        description_Text.text = items[id].GetComponent<Item>().descriptionText;
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
    }

    public void Consume(int id)
    {
        if (items[id].GetComponent<Item>().type == Item.ItemType.Consumables)
        {
            Debug.Log($"CONSUMED {items[id].name}");
            items[id].GetComponent<Item>().consumeEvent.Invoke();
            Destroy(items[id], 0.1f);
            items.RemoveAt(id);
            Update_UI();
        }
    }

    private void CheckItemUsage()
    {
        KeyCode[] keyCodes = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5};
        for(int i = 0; i<keyCodes.Length; i++) {
            if(Input.GetKeyDown(keyCodes[i])) {
                UseItem(i);
            }
        }
    }

    private void UseItem(int index) {
        var selectedItem = items[index].GetComponent<Item>();
        Debug.Log(selectedItem);
    }

    public void ClearInventory() {
        items.ForEach(delegate(GameObject item) {
            item.SetActive(true);
        });
        items.Clear();
        Update_UI();
    }

    public bool IncludesItem(Items searchedItem)
    {
        return items.Any(item => item.GetComponent<Item>().item == searchedItem);
    }
}