using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStore : MonoBehaviour
{
    public GameObject portal;
    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {
            other.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
        }
        
    }
}
