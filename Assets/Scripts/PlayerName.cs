using UnityEngine;
using System.IO;
using TMPro;

public class PlayerName : MonoBehaviour
{
    public TMP_Text userNameText;
    private string fileName = "Character.json";

    [System.Serializable]
    public class CharacterData
    {
        public string userName;
    }

    public void ReadUsername()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "SavedGames", "NewGame", fileName);

        if (File.Exists(filePath))
        {
            // Read the JSON file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON data into CharacterData object
            CharacterData characterData = JsonUtility.FromJson<CharacterData>(json);

            // Access the username
            string username = characterData.userName;

            Debug.Log("Username: " + username);

            // Assign the retrieved username to the TMP_Text component
            userNameText.text = username;
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }
    }
}
