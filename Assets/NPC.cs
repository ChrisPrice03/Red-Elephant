using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject ending1;
    public GameObject ending2;
    public string[] dialogue;
    private int index;

    public GameObject contButton;

    public float wordSpeed;
    public bool playerIsClose;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && playerIsClose) {
            if (GameObject.FindGameObjectsWithTag("BadPanda").Length == 0) {
                // Show 'Cleared' UI menu
                ending1.SetActive(true);
                StartCoroutine(Typing());
            } else if (GameObject.FindGameObjectsWithTag("FriendlyPanda").Length == 0) {
                ending2.SetActive(true);
                StartCoroutine(Typing());
            } else {
                if (dialoguePanel.activeInHierarchy) {
                    zeroText();
                } else {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
                if(dialogueText.text == dialogue[index]) {
                    contButton.SetActive(true);
                }
            }
        }
        
        
    }

    public void zeroText() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() {
        foreach(char letter in dialogue[index].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void Exit() {
        SceneManager.LoadScene("CharMoveTest");
    }
    public void NexLine() {
        contButton.SetActive(false);
        if (index < dialogue.Length - 1) {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        } else {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            playerIsClose = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            playerIsClose = false;
            zeroText();
        }

    }
}
