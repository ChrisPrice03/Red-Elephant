using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxXP(int xp) {
        slider.maxValue = xp;
    }

    public void setXP(int xp) {
        slider.value = xp;
    }
}
