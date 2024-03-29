using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static UIManager _instance;

    public static UIManager IUnstance {
        get {
            if (_instance == null) {
                Debug.LogError("UI Manager is Null");
            }

            return _instance;
        }
    }

    [SerializeField] TextMeshProUGUI playerCoinCountTexts;
    public Image selectionImg;

    public void OpenShop(int coinCount) {
        coinCount = 50;
        playerCoinCountTexts.text =  "" + coinCount + "G";
    }

    void Awake() {
        _instance = this;
    }
}
