using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2Int mousePos;
    public terrainGeneration terrainGenerator; 
    private Rigidbody2D player; 
    public bool hit;
    public GameObject camChar;
    public GameObject camWorld;

    private Vector2 lastPosition;
    private float totalDistance;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(xAxis * 7f, player.velocity.y);

        if(Input.GetButtonDown("Jump")) 
        {
            player.velocity = new Vector2(player.velocity.x, 7f);
        }

        hit = Input.GetMouseButton(0);
        
        if (Input.GetButtonDown("mKey")) 
        {
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
        
        if (hit) 
        {
            terrainGenerator.OpenLootBox(mousePos.x, mousePos.y);
        }
        
        if((mousePos.x == -106 || mousePos.x == -107) && (mousePos.y == -80 || mousePos.y == -81)) 
        {
            terrainGenerator.LoadGame();
        }

        // Calculate the distance moved since the last frame
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);

        // Add the distance moved to the total distance
        totalDistance += distanceMoved;

        // Update the last position for the next frame
        lastPosition = transform.position;
    }

    // Getter method to access the total distance moved
    public float GetTotalDistance()
    {
        return totalDistance;
    }
}
