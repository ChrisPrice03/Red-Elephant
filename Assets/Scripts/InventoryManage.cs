using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManage : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;

    public ItemizeSlot[] itemizeSlot;
   
    void Update()
    {
        if(Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for(int i = 0; i < itemizeSlot.Length; i++)
        {
            if (itemizeSlot[i].isFull == false && itemizeSlot[i].itemName == itemName || itemizeSlot[i].quantity == 0) 
            {
                int leftOverItems = itemizeSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                }
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i  < itemizeSlot.Length; i++)
        {
            itemizeSlot[i].selectedShader.SetActive(false);
            itemizeSlot[i].thisItemSelected = false;
        }
    }
}
