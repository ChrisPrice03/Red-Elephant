using System.IO;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    private int currentIndex = 0; // Current index of the file being processed

    // Method to display and apply customizations
    public void DisplayAndApplyCustomizations()
    {
        string folderPath = Application.persistentDataPath + "/CustomizedCharacters";

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            if (files.Length > 0)
            {
                Debug.Log("Available Customizations:");

                // Loop through the files starting from the currentIndex
                for (int i = currentIndex; i < files.Length + currentIndex; i++)
                {
                    int index = i % files.Length; // Wrap around the index when reaching the end

                    // Extract character name from file path
                    string characterName = Path.GetFileNameWithoutExtension(files[index]);

                    Debug.Log("Character: " + characterName);

                    // Apply saved customization for each character
                    ApplySavedCharacterCustomization(files[index]);
                }
            }
            else
            {
                Debug.Log("No saved customizations found.");
            }
        }
        else
        {
            Debug.Log("Customization directory does not exist.");
        }
    }

    // Method to apply saved character customization from JSON file
    private void ApplySavedCharacterCustomization(string filePath)
    {
        try
        {
            // Read JSON data from file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize JSON data to CharacterData object
            SaveCharacter.CharacterData savedData = JsonUtility.FromJson<SaveCharacter.CharacterData>(jsonData);

            // Apply saved indices to outfitChangers
            SpriteDropdown characterCreation = FindObjectOfType<SpriteDropdown>();
            if (characterCreation != null)
            {
                for (int i = 0; i < savedData.indices.Length && i < characterCreation.outfitChangers.Count; i++)
                {
                    characterCreation.outfitChangers[i].SetCurrentOption(savedData.indices[i]);
                }

                Debug.Log("Character customization applied successfully for: " + savedData.characterName);
            }
            else
            {
                Debug.LogError("CharacterCustomizationMenu not found.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error applying character customization: " + e.Message);
        }
    }

    // Method to delete the last saved character customization JSON file
    public void DeleteLastCharacterCustomization()
    {
        string folderPath = Application.persistentDataPath + "/CustomizedCharacters";

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            if (files.Length > 0)
            {
                string lastSavedFile = files[files.Length - 1];
                string characterName = Path.GetFileNameWithoutExtension(lastSavedFile);

                try
                {
                    File.Delete(lastSavedFile);
                    Debug.Log("Last saved character customization deleted successfully for: " + characterName);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error deleting last saved character customization: " + e.Message);
                }
            }
            else
            {
                Debug.LogWarning("No saved character customizations found.");
            }
        }
        else
        {
            Debug.LogWarning("Customization directory does not exist.");
        }
    }
}
