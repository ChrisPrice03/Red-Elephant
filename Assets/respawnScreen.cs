using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnScreen : MonoBehaviour
{
    public string SceneName;
    public GameObject respawnUI;
    public GameObject canvas;
    public bool GameisPaused = false;
    public static bool isPaused = false;
    public string s1 = "TitleScreenScene";
    
    // Load the Scene when this Button is pressed
    public void LoadScene()
    {
        
        
        // Loads the second Scene
        
        Time.timeScale = 1f;
        GameisPaused = false;
        SceneManager.LoadScene(SceneName);
        
        // Outputs the name of the current active Scene.
        // Notice it still outputs the name of the first Scene
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

    }

    public void SettingBack() {
        SceneManager.LoadScene(SceneName);
        respawnUI.SetActive(true);
        canvas.SetActive(false);

        Time.timeScale = 0f;
        GameisPaused = true;
        // Outputs the name of the current active Scene.
        // Notice it still outputs the name of the first Scene
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

    public void ResumeButton() {

        respawnUI.SetActive(false);
        canvas.SetActive(true);

        Time.timeScale = 1f;
        GameisPaused = false;
    }

    
    public void showDeath()
    {
        respawnUI.SetActive(true);
        canvas.SetActive(false);

        Time.timeScale = 0f;
        GameisPaused = true;
        // Allow this other Button to be pressed when the other Button has been pressed (Scene 2 is loaded)
        /**xpublic static GameObject pauseMenuUI;
            public static bool GameisPaused = false;
            public static bool isPaused = false;*/
    }

    public void Resume() {
        
        respawnUI.SetActive(false);
        canvas.SetActive(true);

        Time.timeScale = 1f;
        GameisPaused = false;
        //SceneManager.LoadScene("CharMoveTest");
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            if (GameisPaused) {
                Resume();
            } else {
                showDeath();
            }
        }
    }
    */
}
