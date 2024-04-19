using System;
using System.IO;
using UnityEngine;
using TMPro;

public class CharacterLoader : MonoBehaviour
{
    private int currentIndex = 0; // Current index of the file being processed
    public OutfitChanger[] outfitChangers; // Array of OutfitChanger components

    public TMP_Text CharacterNameText;

    // Method to display and apply customizations
    public void DisplayAndApplyCustomizations(int direction)
    {
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedCharacters");

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            if (files.Length > 0)
            {
                Debug.Log("Available Customizations:");

                // Update currentIndex based on direction
                currentIndex += direction;

                // Ensure currentIndex stays within bounds
                if (currentIndex < 0)
                {
                    currentIndex = files.Length - 1;
                }
                else if (currentIndex >= files.Length)
                {
                    currentIndex = 0;
                }

                // Apply saved customization for the current character
                ApplySavedCharacterCustomization(files[currentIndex]);
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

    public void NextCharacter()
{
    DisplayAndApplyCustomizations(1);
}

public void PreviousCharacter()
{
    DisplayAndApplyCustomizations(-1);
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

            // Set character name text
            if (CharacterNameText != null)
            {
                CharacterNameText.text = savedData.characterName;
            }
            else
            {
                Debug.LogWarning("CharacterNameText TMP_Text component not assigned.");
            }

            // Apply saved indices to outfitChangers
            if (outfitChangers != null && outfitChangers.Length > 0)
            {
                for (int i = 0; i < savedData.indices.Length && i < outfitChangers.Length; i++)
                {
                    outfitChangers[i].SetCurrentOption(savedData.indices[i]);
                }

                Debug.Log("Character customization applied successfully for: " + savedData.characterName);
            }
            else
            {
                Debug.LogError("OutfitChangers array not assigned or empty.");
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
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedCharacters");

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
