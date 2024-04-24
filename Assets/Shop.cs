using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject notEnough;
    public int currentSelection;
    public int currentItemCost;

    private Player _player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            _player = other.GetComponent<Player>();

            if (_player != null) {
                UIManager.Instance.OpenShop(_player.gold);
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item) {
        //0 hp portion
        //1 xp portion
        Debug.Log("Select" + item);
        switch(item) {
            case 0:
                UIManager.Instance.UpdateShopSelection(93);
                currentSelection = 0;
                currentItemCost = 20;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-103);
                currentSelection = 1;
                currentItemCost = 60;
                break;
        }
    }

    public void Buy() {
        if(_player.gold >= currentItemCost) {
            notEnough.SetActive(false);
            _player.gold -= currentItemCost;
            if (currentSelection == 0) { //hp
                _player.curHp += 30;
                _player.healthBar.setHealth(_player.curHp);
            }
            shopPanel.SetActive(false);

        } else {
            notEnough.SetActive(true);
        }
    }
}
