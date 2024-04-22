using UnityEngine;
using System.IO;
using TMPro;

public class CharacterLoader : MonoBehaviour
{
    private int currentIndex = 0; // Current index of the file being processed
    public OutfitChanger[] outfitChangers; // Array of OutfitChanger components
    public TMP_Text CharacterNameText;

    // Method to display and apply customizations
    public void DisplayAndApplyCustomizations(int direction)
    {
        TextAsset[] files = Resources.LoadAll<TextAsset>("SavedCharacters");

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

    public void NextCharacter()
    {
        DisplayAndApplyCustomizations(1);
    }

    public void PreviousCharacter()
    {
        DisplayAndApplyCustomizations(-1);
    }

    // Method to apply saved character customization from JSON file
    private void ApplySavedCharacterCustomization(TextAsset file)
    {
        try
        {
            // Deserialize JSON data to CharacterData object
            SaveCharacter.CharacterData savedData = JsonUtility.FromJson<SaveCharacter.CharacterData>(file.text);

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
}
