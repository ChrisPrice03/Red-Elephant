using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemizeSlot : MonoBehaviour, IPointerClickHandler
{
    //===ITEM DATA===//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    //===ITEM SLOT===//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    //===ITEM DESCRITPTION SLOT===//
    public Image itemDescriptionImage;
    public TMP_Text itemDescritptionNameText;
    public TMP_Text itemDescritptionText;


    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManage inventoryManage;

    public void Start()
    {
        inventoryManage = GameObject.Find("InventoryCanvas").GetComponent<InventoryManage>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManage.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescritptionNameText.text = itemName;
        itemDescritptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;

        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    public void OnRightClick()
    {

    }
}
