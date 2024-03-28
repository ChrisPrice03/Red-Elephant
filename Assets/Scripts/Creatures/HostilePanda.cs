using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostilePanda : MonoBehaviour
{
    //the determining exp
    public Transform center;
    public float xpRange = 2f;
    public LayerMask playerLayer;

    public int maxHealth = 100;
    public int currentHealth;
    public int xpVal = 10;
    public int attackDamage = 5;
    public float attackRate = 1f; //one attack per second
    float nextAttackTime = 0f;

    public HealthBar healthBar;

    //for creature wandering or player finding
    public float moveSpeed = 1f;
    public float wanderInterval = 2f;

    private bool isMoving = true;
    private float timer;
    private int direction;

    public float detectionRange = 5f;
    public Transform player;

    //animations
    public Animator animator;

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
            changeDirection();
            timer = wanderInterval;
        }

        //moving towards player
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange) {
            // Normalize the direction vector to maintain constant speed
            Vector3 moveDirection = directionToPlayer.normalized;

            // Move towards the player
            animator.SetTrigger("Walking");
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        else {
            // Move in the chosen direction
            if (isMoving) {
                animator.SetTrigger("Walking");
                transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
            }
        }
        if (Time.time >= nextAttackTime) {
             attackNearby();
             nextAttackTime = Time.time + 1f / attackRate;
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
    void changeDirection() {
        // Randomly choose left or right
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        isMoving = Random.Range(0, 2) == 0 ? false : true;
    }

    //this command is run to cause the creature to attack
    void attackNearby() {
        //detecting hit enemies
        Collider2D[] nearbyPlayers = Physics2D.OverlapCircleAll(center.position, xpRange, playerLayer);

        //attacking
        foreach(Collider2D player in nearbyPlayers) {
            player.GetComponent<Player>().loseHp(attackDamage);
        }
    }
}
