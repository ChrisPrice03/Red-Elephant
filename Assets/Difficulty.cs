using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Difficulty : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diffText;
    [SerializeField] DifficultySO diff;
    [SerializeField] GameObject[] answerButtons;
    int selectedIndex;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite selectedSprite;

    public bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        diffText.text = diff.GetDescription();

        for(int i = 0; i < answerButtons.Length - 1; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = diff.GetSelectedDiff(i);
        }
        // ApplySelection();
        // NextPage();

    }

    public void OnAnswerSelected(int index) {

        Image buttonImage;

        if (index == 0) {
            diffText.text = "Easy mode:\n Player's HP: 150%\n Enemy's HP: 100%\n Player's Initial Damage: 150%\n ENemy's Initial Damage: 100%";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = selectedSprite;
        }
        else if (index == 1) {
            diffText.text = "Normal mode:\n Player's HP: 100%\n Enemy's HP: 100%\n Player's Initial Damage: 100%\n ENemy's Initial Damage: 100%";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = selectedSprite;
        }
        else if (index == 2){
            diffText.text = "Hard mode:\n Player's HP: 75%\n Enemy's HP: 100%\n Player's Initial Damage: 75%\n ENemy's Initial Damage: 100%";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = selectedSprite;
        }
        else {
            isComplete = true;
        }

        

        // if(index == diff.GetDifficultyIndex()) {
        //     diffText.text = "Chosen";
        //     buttonImage = answerButtons[index].GetComponent<Image>();
        //     buttonImage.sprite = selectedSprite;
        // }
        // else {
        //     selectedIndex = diff.GetDifficultyIndex();
        //     string selection = diff.GetSelectedDiff(selectedIndex);
        //     diffText.text = "ss" + selection;
        //     buttonImage = answerButtons[selectedIndex].GetComponent<Image>();
        //     buttonImage.sprite = selectedSprite;
        // }
        // SetButtonState(false);
    }

    void NextPage() {
        SetButtonState(true);
        SetDefaultButtonSprite();
        ApplySelection();
    }

    void ApplySelection() {
        diffText.text = diff.GetDescription();

        for(int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = diff.GetSelectedDiff(i);
        }
    }

    void SetButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite() {
        for (int i = 0; i < answerButtons.Length; i++) {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
        }
    }
}
