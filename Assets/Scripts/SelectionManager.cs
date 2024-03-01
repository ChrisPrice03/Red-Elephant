using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    Difficulty quiz;
    ReturnToPage endScreen;
    // Start is called before the first frame update
    void Start()
    {
        quiz = FindObjectOfType<Difficulty>();
        endScreen = FindObjectOfType<ReturnToPage>();

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(quiz.isComplete) {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
        }
    }

    public void OnBackMain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
