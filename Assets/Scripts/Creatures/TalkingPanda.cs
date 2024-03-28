using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingPanda : MonoBehaviour
{
    //textbox
    public GameObject textbox;
    public CharInfoText speech;
    public string[] dialogueOptions = {"Hi there!",
                                                   "How are you?",
                                                   "Nice weather today!",
                                                   "What's up?",
                                                   "Have a good day!",
                                                   "Good morning!",
                                                   "How's it going?",
                                                   "Lovely to see you!",
                                                   "Take care!",
                                                   "See you later!"};

    //the determining exp
    public Transform center;
    public float xpRange = 1f;
    public LayerMask playerLayer;

    public int maxHealth = 100;
    public int currentHealth;
    public int xpVal = 10;

    public HealthBar healthBar;

    //for creature wandering
    public float moveSpeed = 1f;
    public float wanderInterval = 2f;

    private bool isMoving = true;
    private float timer;
    private int direction;

    //animations
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        speech.updateText(SelectRandomDialogue());
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(currentHealth);
        timer = wanderInterval;
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            ChangeDirection();
            timer = wanderInterval;
        }

        // Move in the chosen direction
        if (isMoving) {
            animator.SetTrigger("Walking");
            transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        }
    }

    //allows the creature to take damage
    public void TakeDamage(int damage) {
        if (currentHealth - damage <= 0) {
                    currentHealth = 0;
                    healthBar.setHealth(0);
                    Die();
        }
        else {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);
        }
    }

    //allows the creature to be interacted with
    public void interact() {
        //Debug.Log("Interacted with me!");
        speech.updateText(SelectRandomDialogue());
        textbox.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay() {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Hide the object
        textbox.SetActive(false);
    }

    //kills creature
    void Die() {
        //detecting nearby players
        Collider2D[] nearbyPlayers = Physics2D.OverlapCircleAll(center.position, xpRange, playerLayer);
        //adding xp
        foreach(Collider2D player in nearbyPlayers) {
                player.GetComponent<Player>().addXp(xpVal);
        }

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    //randomly sets wander direction
    void ChangeDirection() {
        // Randomly choose left or right
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        isMoving = Random.Range(0, 2) == 0 ? false : true;
    }

    public string SelectRandomDialogue() {
        // Select a random index from the dialogueOptions array
        int randomIndex = Random.Range(0, dialogueOptions.Length);

        // Retrieve the selected dialogue
        return dialogueOptions[randomIndex];
    }
}