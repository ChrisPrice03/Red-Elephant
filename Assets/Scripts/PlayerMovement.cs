using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public Vector2Int mousePos;
    public terrainGeneration terrainGenerator; 
    //player body
    private Rigidbody2D player; 
    public bool hit;

    public GameObject camChar;
    public GameObject camWorld;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        
    }
    // Change the newly loaded Scene to be the active Scene if it is loaded
    

    // Update is called once per frame
    private void Update()
    {


        //gets the x position of player
        float xAxis = Input.GetAxis("Horizontal");
        //if player is moved changed the players postion
        player.velocity = new Vector2(xAxis * 7f, player.velocity.y);
        //if jump is slected move player upwards
        if(Input.GetButtonDown("Jump")) {
            player.velocity = new Vector3(player.velocity.x, 7f);
        }

        hit = Input.GetMouseButton(0);

        if (Input.GetButtonDown("mKey")) {
            camWorld.SetActive(true);
            camChar.SetActive(false);
        }
        else if (Input.GetButtonDown("nKey"))
        {
            camChar.SetActive(true);
            camWorld.SetActive(false);
        }
        
        mousePos.x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        mousePos.y = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (hit) {
            
            terrainGenerator.OpenLootBox(mousePos.x, mousePos.y);
        }
        
        
    }
}
