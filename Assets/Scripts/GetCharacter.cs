using System.IO;
using UnityEngine;
using TMPro;
using System;

public class GetCharacter : MonoBehaviour
{
    public OutfitChanger[] outfitChangers; // Array of OutfitChanger components
    public TMP_Text CharacterNameText;

    void Start()
    {
        // Call the method to display and apply customizations
        DisplayAndApplyCustomizations();
    }

    // Method to display and apply customizations
    public void DisplayAndApplyCustomizations()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "SavedCharacters", "Character.json");
        
        ApplySavedCharacterCustomization(filePath); // Assuming only one file for simplicity
            
    }

    // Method to apply saved character customization from JSON file
    private void ApplySavedCharacterCustomization(string filePath)
    {
        try
        {
            // Read JSON data from file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize JSON data to CharacterData object
            CharacterChosen.CharactersData savedData = JsonUtility.FromJson<CharacterChosen.CharactersData>(jsonData);

            // Set character name text
            CharacterNameText.text = savedData.userName;
           
            // Apply saved indices to outfitChangers
            if (outfitChangers != null && outfitChangers.Length > 0)
            {
                for (int i = 0; i < savedData.indices.Length && i < outfitChangers.Length; i++)
                {
                    outfitChangers[i].SetCurrentOption(savedData.indices[i]);
                }

                Debug.Log("Character customization applied successfully for: " + savedData.userName);
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
