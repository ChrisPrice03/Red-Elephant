using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cam1;
    public GameObject cam2;
    void Update()
    {
        if (Input.GetButtonDown("mKey")){
            cam2.SetActive(true);
            cam1.SetActive(false);
        } else if (Input.GetButtonDown("nKey")) {
            cam1.SetActive(true);
            cam2.SetActive(false);
        } else {
            cam1.SetActive(true);
        }
    }
}
