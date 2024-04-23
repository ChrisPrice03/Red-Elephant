using UnityEngine;
using TMPro;
<<<<<<< HEAD

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
=======
using System.IO;
using System.Linq;

public class PlayerScore : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public int scoreMultiplier = 10; // Multiplier for the player's position to calculate score
    public TMP_Text scoreText; // Reference to the TMP_Text component for displaying the score
    public TMP_Text userName; // Reference to the TMP_Text component for displaying the username

    private int scoreValue; // Player's score

    [System.Serializable]
    public class ScoresData
    {
        public int score;
        public string userName;
    }

    void Start()
    {
        // Load the score from PlayerPrefs
        scoreValue = PlayerPrefs.GetInt("PlayerScore", 0);

        // Update the score text initially
        UpdateScoreText();
    }

    void Update()
    {
        // Check if the player has moved rightwards
        if (playerTransform.position.x > scoreValue)
        {
            // Calculate the score based on the player's position
            int newPositionScore = Mathf.FloorToInt(playerTransform.position.x * scoreMultiplier);

            // Check if the new score is greater than the current score
            if (newPositionScore > scoreValue)
            {
                // Update the score
                scoreValue = newPositionScore;

                // Save the score to PlayerPrefs
                PlayerPrefs.SetInt("PlayerScore", scoreValue);
                PlayerPrefs.Save();

                // Update the score text
                UpdateScoreText();

                // Save score data to JSON file
                SaveScoreJson();
            }
        }
    }

    void UpdateScoreText()
    {
        // Update the text component with the new score value
        scoreText.text = "Score: " + scoreValue;
    }

    // Method to save score and username to JSON file
    public void SaveScoreJson()
    {
        // Define the folder path using Application.persistentDataPath
        string folderPath = Path.Combine(Application.persistentDataPath, "SavedScores");

        // Check if the folder exists, if not, create it
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Create an instance of ScoresData
        ScoresData data = new ScoresData();

        // Assign score and username
        data.score = scoreValue;
        data.userName = userName.text;

        // Serialize data to JSON
        string jsonData = JsonUtility.ToJson(data);

        // Define the file path to save JSON file
        string filePath = Path.Combine(folderPath, userName.text + ".json");

        try
        {
            // Write JSON data to file
            File.WriteAllText(filePath, jsonData);
            Debug.Log("JSON file saved successfully: " + filePath);

            // Check if the directory contains more than 100 files
            string[] files = Directory.GetFiles(folderPath);
            if (files.Length > 100)
            {
                // Get the oldest file
                var oldestFile = files.OrderBy(f => new FileInfo(f).CreationTime).First();

                // Delete the oldest file
                File.Delete(oldestFile);
                Debug.Log("Oldest file deleted: " + oldestFile);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving JSON file: " + e.Message);
        }
>>>>>>> Revert-Commitss
    }
}
