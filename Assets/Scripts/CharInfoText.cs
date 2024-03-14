using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharInfoText : MonoBehaviour
{

    //text editors
    public TMP_Text infoText;

    // Update updates the text
    public void updateText(string info) {
        infoText.text = info;
    }
}
