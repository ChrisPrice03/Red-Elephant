using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonPressScript : MonoBehaviour
{
    public string SceneName;
    public void LoadScene()
    {
        Debug.Log("loaded Scene");
        SceneManager.LoadScene(SceneName);
    }
}
