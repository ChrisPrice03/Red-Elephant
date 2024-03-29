using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class SaveCharacter : MonoBehaviour
{
    public CharacterCreationMenu characterCreationMenu;

    [System.Serializable]
    public class CharacterData
    {
        public int[] indices;

        public string characterName;
    }

    public void SaveCharacterToJson()
    {
        // Create an instance of CharacterData
        CharacterData data = new CharacterData();

        // Get indices from the characterCreationMenu
        data.indices = characterCreationMenu.Indices();

        // Get the character name from the input field
        data.characterName = characterCreationMenu.characterNameInput.text;

        // Serialize data to JSON
        string jsonData = JsonUtility.ToJson(data);

        // Define the file path to save JSON file
        string filePath = Application.persistentDataPath + "/" + data.characterName + ".json";

        // Write JSON data to file
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

        // Reset input field
        characterCreationMenu.characterNameInput.text = "Enter Name...";
    }
    }

