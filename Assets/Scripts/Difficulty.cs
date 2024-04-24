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

    public int selectedDifficulty;

    private Player _player;

    private HostilePanda _hostilePanda;
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

        selectedDifficulty = index;

        if (index == 0) {
            diffText.text = "Easy mode:\n Player's HP: 150%\n Enemy's HP: 75%\n";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = selectedSprite;
        }
        else if (index == 1) {
            diffText.text = "Normal mode:\n Player's HP: 100%\n Enemy's HP: 100%\n ";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = selectedSprite;
        }
        else if (index == 2){
            diffText.text = "Hard mode:\n Player's HP: 75%\n Enemy's HP: 150%\n";
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

    public void Change() {
        if (selectedDifficulty == 0) {
            _player.maxHp += 50;
            _player.curHp += 50;
            _hostilePanda.maxHealth -= 25;
            _hostilePanda.currentHealth -= 25;
            _player.healthBar.setMaxHealth(_player.maxHp);
            _player.healthBar.setHealth(_player.curHp);
        }
        else if (selectedDifficulty == 1) {
            _player.curHp += 0;
            _player.healthBar.setMaxHealth(_player.maxHp);
            _player.healthBar.setHealth(_player.curHp);
        }
        else if (selectedDifficulty == 2){
            _player.maxHp -= 25;
            _player.curHp -= 25;
            _hostilePanda.maxHealth += 50;
            _hostilePanda.currentHealth += 50;
            _player.healthBar.setMaxHealth(_player.maxHp);
            _player.healthBar.setHealth(_player.curHp);
        }
        else {
            isComplete = true;
        }
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
