using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonPressScript : MonoBehaviour
{
    public string SceneName;
    
    // Load the Scene when this Button is pressed
    public void LoadScene()
    {
        
        
        // Loads the second Scene
        SceneManager.LoadScene(SceneName);

        // Outputs the name of the current active Scene.
        // Notice it still outputs the name of the first Scene
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

    }

    public void ResumeButton() {

        SceneManager.LoadScene(SceneName);
    }
}

    
