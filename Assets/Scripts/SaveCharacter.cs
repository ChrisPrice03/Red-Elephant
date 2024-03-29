using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveCharacter : MonoBehaviour
{
    public GameObject character;
    public TMP_InputField inputField;

    public TMP_Text notificationField;

    public GameObject notificationPanel;
    private int maxCharacterCount = 10;

    public void Save()
    {
        if (GetCharacterCount() >= maxCharacterCount)
            {
                notificationField.text = "Character List is Full. Delete a Character to save a new one!";
                notificationPanel.SetActive(true);
                inputField.text = "Enter Name...";

                StartCoroutine(HideSuccessPanel());
                return;
            }
        string characterName = inputField.text; 
        string prefabPath = "Assets/CharacterCustomizations/" + characterName + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(character, prefabPath);
        inputField.text = "Enter Name...";

        notificationField.text = "Character successfully added to list!";
        notificationPanel.SetActive(true);

        // Start the coroutine to hide the panel after a few seconds
        StartCoroutine(HideSuccessPanel());

    }
    private int GetCharacterCount()
    {
        string[] prefabFiles = Directory.GetFiles("Assets/CharacterCustomizations/", "*.prefab");
        return prefabFiles.Length;
    }
    IEnumerator HideSuccessPanel()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2f);

        // Hide the success panel after 3 seconds
        notificationPanel.SetActive(false);
    }
}
