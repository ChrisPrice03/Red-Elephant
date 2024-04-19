using UnityEngine;
using System.IO;
using TMPro;

public class PlayerName : MonoBehaviour
{
    public TMP_Text userNameText;

    public void ReadUsername()
    {
        string filePath = @"C:\Users\Ife Ogunbanjo\OneDrive\Documents\SavedGames\NewGame\Character.json";

        if (File.Exists(filePath))
        {
            // Read the JSON file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON data into CharacterData object
            CharacterChosen.CharactersData charactersData = JsonUtility.FromJson<CharacterChosen.CharactersData>(json);

            // Access the username
            string username = charactersData.userName;

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
