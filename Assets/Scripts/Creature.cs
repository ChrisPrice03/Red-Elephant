using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

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

    void Die() {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
