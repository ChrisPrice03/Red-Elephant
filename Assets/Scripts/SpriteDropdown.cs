using UnityEngine;
using System.Collections.Generic;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject currentCharacter;
    public GameObject[] characterPrefabs;
    private List<GameObject> characterGameObjects = new List<GameObject>();
    private int currentOption = 0;

    void Start()
    {
        // Load character prefabs from the specified folder
        LoadCharacterPrefabs();
        
        // Set initial character
        ShowCharacter(currentOption);
    }

    // Function to load character prefabs from the specified folder
    void LoadCharacterPrefabs()
    {
        characterPrefabs = Resources.LoadAll<GameObject>("CharacterCustomizations");

        // Create empty GameObjects for each character prefab
        foreach (GameObject prefab in characterPrefabs)
        {
            GameObject characterGameObject = new GameObject(prefab.name);
            characterGameObject.SetActive(false); // Start with all characters hidden
            characterGameObjects.Add(characterGameObject);

            // Assign the prefab to the GameObject
            GameObject instantiatedPrefab = Instantiate(prefab, characterGameObject.transform);
            instantiatedPrefab.transform.localPosition = Vector3.zero;
            instantiatedPrefab.transform.localRotation = Quaternion.identity;
            instantiatedPrefab.transform.localScale = Vector3.one;
        }
    }

    // Function to cycle to the next character option
    public void NextOption()
    {
        currentOption++;
        if (currentOption >= characterGameObjects.Count)
        {
            currentOption = 0;
        }

        ShowCharacter(currentOption);
    }

    // Function to show the specified character
    void ShowCharacter(int indexToShow)
    {

        currentCharacter.SetActive(false);
        
        for (int i = 0; i < characterGameObjects.Count; i++)
        {
            if (i == indexToShow)
            {
                characterGameObjects[i].SetActive(true);
            }
            else
            {
                characterGameObjects[i].SetActive(false);
            }
        }
    }
}
