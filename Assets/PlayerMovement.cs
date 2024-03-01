using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    
    //player body
    private Rigidbody2D player; 
    public static bool isPaused = false;
    bool m_SceneLoaded  = false;
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        
    }

   

    // Change the newly loaded Scene to be the active Scene if it is loaded
    public void Pause()
    {
        // Allow this other Button to be pressed when the other Button has been pressed (Scene 2 is loaded)
        if (m_SceneLoaded == true)
        {
            // Set Scene2 as the active Scene
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("PauseScreenScene"));

            // Ouput the name of the active Scene
            // See now that the name is updated
            Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
        } else {
            SceneManager.LoadScene("PauseScreenScene", LoadSceneMode.Additive);
            m_SceneLoaded = true;
        }
    }

    public void Resume() {
        SceneManager.LoadScene("CharMoveTest");
    }

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
        // mouse movement
        float mouseX = -9999; 
        float mouseY = -9999;

        //if mouse moves
        if (Input.GetAxis("Mouse X") != mouseX || Input.GetAxis("Mouse Y") != mouseY) {
            //get coordinate change
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            //determine if mouse is pressed
            bool click = Input.GetButtonDown("Fire1");

            //print mouse movement from last reading
            if(click == true) {
                Debug.Log("X,Y" + mouseX + " " + mouseY + " " + click);
            }
            
        }
        
        if (Input.GetButtonDown("Pause")) {
            if (isPaused) {
                Resume();
                isPaused = false;
            } else {
                Pause();
                isPaused = true;
            }
        }
        
    }
}
