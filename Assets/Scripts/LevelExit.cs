using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    [SerializeField] float levelLoadDelay = 1f;
    

    public bool isNextScene = true;

    [SerializeField]
    public SceneInfo sceneInfo;
    void OnTriggerEnter2D(Collider2D player) 
    {
        // sceneInfo.isNextScene = isNextScene;
        SceneManager.LoadScene(sceneName);
        // StartCoroutine(LoadNextLevel());

    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneIndex + 1);
        SceneManager.LoadScene("CharMoveTest");
    }
}
