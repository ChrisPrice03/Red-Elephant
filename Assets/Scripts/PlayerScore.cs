using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float scoringThreshold = 1f; // Distance threshold for scoring (adjust as needed)
    public int scoreIncrement = 10; // Amount to increment the score
    public TMP_Text scoreText; // Reference to the TextMeshPro text component

    private int score = 0;

    private Vector3 lastPlayerPosition;

    private void Start()
    {
        // Initialize last player position
        lastPlayerPosition = player.position;

        // Update the initial score display
        UpdateScoreText();
    }

    private void Update()
    {
        // Calculate the distance moved by the player
        float distanceMoved = Vector3.Distance(player.position, lastPlayerPosition);

        // If the distance moved is greater than the scoring threshold, update the score
        if (distanceMoved >= scoringThreshold)
        {
            // Increment the score
            score += scoreIncrement;

            // Update the last player position
            lastPlayerPosition = player.position;

            // Update the score display
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        // Update the TextMeshPro text component with the current score
        scoreText.text =  score.ToString();
    }
}
