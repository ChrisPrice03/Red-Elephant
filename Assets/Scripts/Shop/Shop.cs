using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;

    public int currentItemCost;
    private void OnTriggerEnter2D(Collider2D player) {
        // if (other.tag == "Player"){
            // Player player
            shopPanel.SetActive(true);
        // }
        
    }

    private void OnTriggerExit2D(Collider2D player) {
        // if (other.tag == "Player"){
            shopPanel.SetActive(false);
        // }
        
    }

    public void SelectItem (int item) {
        
        switch (item) {
            case 0:
                currentSelectedItem = 0;
                currentItemCost = 10;
                break;
            case 1:
                currentSelectedItem = 1;
                currentItemCost = 5;
                break;
            case 2:
                currentSelectedItem = 2;
                currentItemCost = 50;
                break;

        }
    }

    public void BuyItem() {
        
    }

}
