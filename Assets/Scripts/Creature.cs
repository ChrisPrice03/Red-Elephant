using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    //the player this creature is associated with
    public Player player;

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

    // Start is called before the first frame update
    void Start()
    {
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

    //kills creature
    void Die() {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        player.addXp(xpVal);
    }

    //randomly sets wander direction
    void ChangeDirection() {
        // Randomly choose left or right
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        isMoving = Random.Range(0, 2) == 0 ? false : true;
    }
}
