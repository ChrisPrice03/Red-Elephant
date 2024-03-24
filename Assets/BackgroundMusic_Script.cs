using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic_Script : MonoBehaviour
{
    public static BackgroundMusic_Script instance;
    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
