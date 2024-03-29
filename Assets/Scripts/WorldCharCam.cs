using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharCam : MonoBehaviour
{
    public GameObject camChar;
    public GameObject camWorld;
    public bool mapActivate = false;
    // Update is called once per frame
    void Update()
    {

        if ((Input.GetButtonDown("mKey"))) {
            camWorld.SetActive(true);
            camChar.SetActive(false);
            // mapActivate = true;
        }
        else if (Input.GetButtonDown("nKey"))
        {
            camChar.SetActive(true);
            camWorld.SetActive(false);
            // mapActivate = true;
        }
        
    }
}
