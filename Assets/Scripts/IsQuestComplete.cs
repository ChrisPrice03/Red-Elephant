using UnityEngine;
using System.Collections;
 
public class IsQuestComplete : MonoBehaviour {
    public GameObject ending;
 
    void Update () {
 
        print (GameObject.FindGameObjectsWithTag("Bad Panda").Length);
 
        if (GameObject.FindGameObjectsWithTag("Bad Panda").Length == 0)
        {
            // Show 'Cleared' UI menu
            ending.SetActive(true);
        }
    }
}