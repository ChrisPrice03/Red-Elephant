using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class ScoreUi : MonoBehaviour
{
    public RowUi rowUi; // Reference to the prefab/UI element used to display each score
    public string savedScoresFolderPath; // Path to the folder where the JSON files are stored

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        // Ensure savedScoresFolderPath is not null or empty
        if (string.IsNullOrEmpty(savedScoresFolderPath))
        {
            Debug.LogWarning("Saved scores folder path is not set.");
            return;
        }

        // Check if the folder exists
        if (Directory.Exists(savedScoresFolderPath))
        {
            // Get all JSON files in the directory
            string[] files = Directory.GetFiles(savedScoresFolderPath, "*.json");

            // Iterate through each JSON file
            foreach (string file in files)
            {
                // Read JSON data from file
                string jsonData = File.ReadAllText(file);

                // Deserialize JSON data to ScoresData object
                ScoresData data = JsonUtility.FromJson<ScoresData>(jsonData);

                // Instantiate a row UI element
                RowUi row = Instantiate(rowUi, transform).GetComponent<RowUi>();

                // Set the name and score in the row UI element
                row.name.text = data.userName;
                row.score.text = data.score.ToString();
            }
        }
        else
        {
            Debug.LogWarning("Saved scores folder not found.");
        }
    }

    [System.Serializable]
    public class ScoresData
    {
        public int score;
        public string userName;
    }
}
