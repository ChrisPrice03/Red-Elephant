using UnityEngine;
using System;
using System.IO;
using TMPro;
using System;

public class CharacterChosen : MonoBehaviour
{
    public CharacterCreationMenu characterCreationMenu;

    [System.Serializable]
    public class CharactersData
    {
        public int[] indices;
        public string userName;
    }

    // Method to save character customization to JSON file
    public void SaveCharacterJson()
    {
<<<<<<< HEAD
        // Define the folder path in the Documents directory
        string folderPath = Path.Combine(Application.persistentDataPath, "SavedGames", "NewGame", "Character");
=======
        // Define the folder path using Application.persistentDataPath
        string folderPath = Path.Combine(Application.persistentDataPath, "SavedCharacters");
>>>>>>> Revert-Commitss

        // Check if the folder exists, if not, create it
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

<<<<<<< HEAD
        // Create an instance of CharacterData
=======
        // Create an instance of CharactersData
>>>>>>> Revert-Commitss
        CharactersData data = new CharactersData();

        // Get indices from the characterCreationMenu
        data.indices = characterCreationMenu.Indices();

        // Get the character name from the input field
<<<<<<< HEAD
        string characterName = characterCreationMenu.characterNameInput.text;

        // Check if the character name is empty
        if (string.IsNullOrEmpty(characterName))
        {
            // Generate username based on current date
            data.userName = "User" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        else
        {
            data.userName = characterName; // Use the provided character name
=======
        if (characterCreationMenu.characterNameInput.text == "")
        {
            data.userName = "User_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }
        else
        {
            data.userName = characterCreationMenu.characterNameInput.text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
>>>>>>> Revert-Commitss
        }

        // Serialize data to JSON
        string jsonData = JsonUtility.ToJson(data);

        // Define the file path to save JSON file
        string filePath = Path.Combine(folderPath, "Character.json");

        try
        {
            // Write JSON data to file
            File.WriteAllText(filePath, jsonData);
            Debug.Log("JSON file saved successfully: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving JSON file: " + e.Message);
        }
    }
}
