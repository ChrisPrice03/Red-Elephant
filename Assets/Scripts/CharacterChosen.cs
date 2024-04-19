using UnityEngine;
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
        // Define the folder path using Application.persistentDataPath
        string folderPath = Path.Combine(Application.persistentDataPath, "SavedCharacters");

        // Check if the folder exists, if not, create it
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Create an instance of CharactersData
        CharactersData data = new CharactersData();

        // Get indices from the characterCreationMenu
        data.indices = characterCreationMenu.Indices();

        // Get the character name from the input field
        if (characterCreationMenu.characterNameInput.text == "")
        {
            data.userName = "User_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }
        else
        {
            data.userName = characterCreationMenu.characterNameInput.text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
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
