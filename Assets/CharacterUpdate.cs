using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CharacterUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public void Update()
    {
        SceneManager.LoadScene(0);
    }
}
