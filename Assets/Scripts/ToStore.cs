using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float levelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D other) 
    {
        SceneManager.LoadScene("TradingScene");
        // StartCoroutine(LoadNextLevel());

    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneIndex + 1);
        SceneManager.LoadScene("TradingScene");
    }
}
