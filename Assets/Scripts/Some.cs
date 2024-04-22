using UnityEngine;

public class scoreMultiplier : MonoBehaviour
{
    // Reference to the object to be shown
    public GameObject objectToShow;

    void Start()
    {
        // Show the object when the scene loads
        objectToShow.SetActive(true);
    }
}
