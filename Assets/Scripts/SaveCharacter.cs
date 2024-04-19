using UnityEngine;
using System.IO;
using System.Collections;
using TMPro;

public class SaveCharacter : MonoBehaviour
{
    public CharacterCreationMenu characterCreationMenu;
    public GameObject notificationsPanel;

    public TMP_Text notification; // Correct variable name

    [System.Serializable]
    public class CharacterData
    {
        public int[] indices;
        public string characterName;
        
    }

    // Method to save character customization to JSON file
    public void SaveCharacterToJson()
    {
        // Define the folder path in the Documents directory
        string folderPath = Path.Combine(Application.persistentDataPath, "SavedGames", "NewGame", "Character");

        // Check if the folder exists, if not, create it
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Check the number of files in the folder
        string[] files = Directory.GetFiles(folderPath, "*.json");
        if (files.Length >= 10)
        {
            // Display notification if there are already ten files
            notification.text = "Character List is Full. Delete a character to store new ones.";
            notificationsPanel.SetActive(true);
            StartCoroutine(HideNotificationPanel());

            return;
        }

        // Create an instance of CharacterData
        CharacterData data = new CharacterData();

        // Get indices from the characterCreationMenu
        data.indices = characterCreationMenu.Indices();

        // Get the character name from the input field
        data.characterName = characterCreationMenu.characterNameInput.text;

        // Serialize data to JSON
        string jsonData = JsonUtility.ToJson(data);

        // Define the file path to save JSON file
        string filePath = Path.Combine(folderPath, data.characterName + ".json");

        try
        {
            // Write JSON data to file
            File.WriteAllText(filePath, jsonData);
            Debug.Log("JSON file saved successfully: " + filePath);

            // Reset input field
            characterCreationMenu.characterNameInput.text = "Enter Name...";

            notification.text = "Successfully Added Character!"; // Correct variable name
            notificationsPanel.SetActive(true);
            StartCoroutine(HideNotificationPanel());
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving JSON file: " + e.Message);
            notification.text = "Error saving character data!"; // Correct variable name
            notificationsPanel.SetActive(true);
            StartCoroutine(HideNotificationPanel());
        }
    }

    IEnumerator HideNotificationPanel()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2f);

        // Hide the notification panel after 3 seconds
        notificationsPanel.SetActive(false);
    }
}
