using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance {
        get {
            if (_instance == null) {
                Debug.LogError("UI manager null");
            }
            return _instance;
        }
    }

    public Text goldCountText;
    public Image selection;

    public void OpenShop(int goldCount) {
        goldCountText.text = goldCount.ToString() + "G";
    }
    private void Awake() {
        _instance = this;
    }

    public void UpdateShopSelection(int yPos) {
        selection.rectTransform.anchoredPosition = new Vector2(selection.rectTransform.anchoredPosition.x, yPos);
    }

}
