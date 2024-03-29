using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public GameObject characterPrefab; // Prefab representing the character
    public Transform characterParent; // Parent transform to instantiate characters under

    void Start()
    {
        // Load all saved characters
        for (int i = 0; i < PlayerPrefs.GetInt("CharacterCount", 0); i++)
        {
            // Instantiate character prefab
            GameObject newCharacter = Instantiate(characterPrefab, characterParent);

            // Set character name
            string characterName = PlayerPrefs.GetString("CharacterName_" + i);
            newCharacter.name = characterName;

            // Load and apply customization data
            int bodyPartCount = PlayerPrefs.GetInt(characterName + "_BodyPartCount", 0);
            for (int j = 0; j < bodyPartCount; j++)
            {
                // Retrieve color data for each body part
                string colorString = PlayerPrefs.GetString(characterName + "_BodyPart_" + j + "_Color");
                Color color;
                if (ColorUtility.TryParseHtmlString(colorString, out color))
                {
                    // Apply color to body part
                    Renderer bodyPartRenderer = newCharacter.transform.GetChild(j).GetComponent<Renderer>();
                    if (bodyPartRenderer != null)
                    {
                        bodyPartRenderer.material.color = color;
                    }
                }
            }
        }
    }
}
