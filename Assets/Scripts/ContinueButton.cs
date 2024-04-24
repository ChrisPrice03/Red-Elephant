using UnityEngine;
using UnityEngine.UI; // Required for interacting with UI elements like Button

public class ContinueButton : MonoBehaviour
{
    public Button targetButton; // Assign this in the inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Check if 'T' is pressed
        {
            targetButton.onClick.Invoke(); // Trigger the button's onClick event
        }
    }
}