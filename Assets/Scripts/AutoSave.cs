using UnityEngine;

public class AutoSave : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the player movement script
    public terrainGeneration terrainSave; // Reference to the save player script
    public float saveInterval = 20f; // Distance interval for saving (adjust as needed)
    
    private float totalDistanceMoved = 0f; // Total distance moved by the player
    
    void Update()
    {
        // Check if the player movement script is available
        if (playerMovement != null)
        {
            // Calculate the distance moved in this frame
            float distanceMovedThisFrame = playerMovement.GetTotalDistance();
            
            // Increment the total distance moved
            totalDistanceMoved += distanceMovedThisFrame;
            
            // Check if the total distance moved exceeds or equals the save interval
            while (totalDistanceMoved >= saveInterval)
            {
                // Trigger the save process
                terrainSave.SaveGame();
                
                // Deduct the save interval from the total distance moved
                totalDistanceMoved -= saveInterval;
            }
        }
        else
        {
            Debug.LogWarning("Player movement script reference not set!");
        }
    }
}
