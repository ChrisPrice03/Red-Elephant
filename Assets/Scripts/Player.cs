using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //adding attack point
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask creatureLayers;

    //adding features attached to a player
    public int level = 0;
    public int totalExp = 0;
    public int xpToLevel = 20;
    public int xpSinceLevel = 0;
    public double levelXpMult = 2;
    public int maxHp = 100;
    public int curHp = 100;
    public int attackDamage = 10;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    //adding individual stat values and spendable points
    public int health = 0;
    public int attack = 0;
    public int defense = 0;
    public int speed = 0;
    public int intelligence = 0;
    int spendablePoints = 0;

    //adding GUI
    public HealthBar healthBar;
    public ExpBar expBar;
    public CharInfoText charInfoText;
    public Button healthButton;
    public Button attackButton;
    public Button defenseButton;
    public Button speedButton;
    public Button intelligenceButton;
    public Image levelNotif;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setMaxHealth(maxHp);
        healthBar.setHealth(curHp);
        expBar.setMaxXP(xpToLevel);
        expBar.setXP(xpSinceLevel);
        charInfoText.updateText(getPlayerInfo());
        healthButton.onClick.AddListener(increaseHealthStat);
        attackButton.onClick.AddListener(increaseAttackStat);
        defenseButton.onClick.AddListener(increaseDefenseStat);
        speedButton.onClick.AddListener(increaseSpeedStat);
        intelligenceButton.onClick.AddListener(increaseIntelligenceStat);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            loseHp(20);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            gainHp(20);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            addXp(20);
        }
        if (Time.time >= nextAttackTime) {
            if (Input.GetMouseButtonDown(0)) {
                attackNearby();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        charInfoText.updateText(getPlayerInfo());
    }

    //returning a string of player info to be displayed
    string getPlayerInfo() {
        return "Health: " + curHp + "/" + maxHp +
                "\nLevel: " + level +
                "\nLevel Progress: " + xpSinceLevel + "/" + xpToLevel +
                "\nTotal Xp: " + totalExp +
                "\n\nAllocated Skill Points: " + spendablePoints + " Spendable" +
                "\nHealth - " + health +
                "\nAttack - " + attack +
                "\nDefense - " + defense +
                "\nSpeed - " + speed +
                "\nIntelligence - " + intelligence;
    }

    //updates levelUp speed based on difficulty
    //0 is easy
    //1 is medium
    //2 is hard
    //3 is extreme
    void updateLevelMult(int difficulty) {
        switch (difficulty) {
            case 0: {
                levelXpMult = 1.5;
                break;
            }
            case 1: {
                levelXpMult = 2;
                break;
            }
            case 2: {
                levelXpMult = 5;
                break;
            }
            case 3: {
                levelXpMult = 8;
                break;
            }
            default: {
                levelXpMult = 2;
                break;
            }
        }
    }

    //checking level status
    //returns true if level up
    bool checkLevelStatus() {
        if (xpSinceLevel >= xpToLevel) {
            this.levelUp();
            this.checkLevelStatus();
            return true;
        }
        return false;
    }

    //levels up a player
    void levelUp() {
        levelNotif.gameObject.SetActive(true);
        spendablePoints++;
        level++;
        xpSinceLevel -= xpToLevel;
        expBar.setXP(xpSinceLevel);
        xpToLevel = (int) (xpToLevel * levelXpMult);
        expBar.setMaxXP(xpToLevel);
    }

    //gives xp tp a player
    void addXp(int count) {
        totalExp += count;
        xpSinceLevel += count;
        expBar.setXP(xpSinceLevel);
        if (this.checkLevelStatus()) {
            this.displayLevelUp();
        }
    }

    //function to show a player when they leveled up
    void displayLevelUp() {
        //incomplete
    }

    //function called when a player dies
    void kill() {
        //incomplete
    }

    //function displayed when player dies
    void showDeath() {
        //incomplete
    }

    bool checkDeath() {
        if (curHp <= 0) {
            this.kill();
            return true;
        }
        return false;
    }

    //allows player to gain Hp
    void gainHp(int gain) {
        if (curHp + gain <= maxHp) {
            curHp += gain;
        }
        else {
            curHp = maxHp;
        }
        healthBar.setHealth(curHp);
    }

    //allows player to take damage
    void loseHp(int lose) {
        if (curHp - lose <= 0) {
            curHp = 0;
            healthBar.setHealth(0);
        }
        else {
            curHp -= lose;
            healthBar.setHealth(curHp);
        }
        if (this.checkDeath()) {
            this.showDeath();
        }
    }

    //allows player to change maxHp
    void modifyMaxHp(int change) {
        maxHp += change;
        healthBar.setMaxHealth(maxHp);
    }

    //allows player to change attackDamage
        void modifyAttackDamage(int change) {
            attackDamage += change;
        }

    //stat functions

    //increases health stat
    public void increaseHealthStat() {
        if (spendablePoints > 0) {
            spendablePoints--;
            health++;
            modifyMaxHp((int) (0.2 * maxHp));
        }
    }

    //increases attack stat
    public void increaseAttackStat() {
        if (spendablePoints > 0) {
            spendablePoints--;
            attack++;
            modifyAttackDamage((int) (0.2 * maxHp));
        }
    }

    //increases defense stat
    public void increaseDefenseStat() {
        if (spendablePoints > 0) {
            spendablePoints--;
            defense++;
        }
    }

    //increases speed stat
    public void increaseSpeedStat() {
        if (spendablePoints > 0) {
            spendablePoints--;
            speed++;
        }
    }

    //increases intelligence stat
    public void increaseIntelligenceStat() {
        if (spendablePoints > 0) {
            spendablePoints--;
            intelligence++;
        }
    }

    //this command is run when the player chooses to attack
    void attackNearby() {
        //detecting hit enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, creatureLayers);

        //hitting the enemies
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Creature>().TakeDamage(attackDamage);
            //Debug.Log("We hit " + enemy.name);
        }
    }
}
