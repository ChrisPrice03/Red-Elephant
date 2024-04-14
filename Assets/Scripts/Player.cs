using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //adding attack point
    public Transform attackPoint;
    public float attackRange = 0.75f;
    public LayerMask creatureLayers;
    public LayerMask interactLayer;
    public float interactRange = 1f;

    //adding features attached to a player
    public int difficulty = 2; //0 peaceful, 1 is easy, 2 is normal, 3 is hard
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

    //adding base player points
    int skillPoints = 10;
    public int baseHealth = 0;
    public int baseAttack = 0;
    public int baseDefense = 0;
    public int baseSpeed = 0;

    //adding GUI
    public HealthBar healthBar;
    public ExpBar expBar;
    public CharInfoText charInfoText;
    public CharInfoText baseSkillsText;
    public Button healthButton;
    public Button attackButton;
    public Button defenseButton;
    public Button speedButton;
    public Button intelligenceButton;
    public Button baseHealthButton;
    public Button baseAttackButton;
    public Button baseDefenseButton;
    public Button baseSpeedButton;
    public Image levelNotif;
    public respawnScreen respawnScreen;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setMaxHealth(maxHp);
        healthBar.setHealth(curHp);
        expBar.setMaxXP(xpToLevel);
        expBar.setXP(xpSinceLevel);
        charInfoText.updateText(getPlayerInfo());
        baseSkillsText.updateText(getBasePlayerInfo());
        healthButton.onClick.AddListener(increaseHealthStat);
        attackButton.onClick.AddListener(increaseAttackStat);
        defenseButton.onClick.AddListener(increaseDefenseStat);
        speedButton.onClick.AddListener(increaseSpeedStat);
        intelligenceButton.onClick.AddListener(increaseIntelligenceStat);
        baseHealthButton.onClick.AddListener(increaseBaseHealthStat);
        baseAttackButton.onClick.AddListener(increaseBaseAttackStat);
        baseDefenseButton.onClick.AddListener(increaseBaseDefenseStat);
        baseSpeedButton.onClick.AddListener(increaseBaseSpeedStat);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.DownArrow)) {
        //    loseHp(20);
        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow)) {
        //    gainHp(20);
        //}
        if (Input.GetKeyDown(KeyCode.R)) {
            addXp(20);
        }
        if (Time.time >= nextAttackTime) {
            if (Input.GetMouseButtonDown(0)) {
                attackNearby();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            interactNearby();
        }
        charInfoText.updateText(getPlayerInfo());
        baseSkillsText.updateText(getBasePlayerInfo());
    }

    //returning a string of player info to be displayed
    string getPlayerInfo() {
        return "Health: " + curHp + "/" + maxHp +
                "\nAttack Damage: " + attackDamage +
                "\nLevel: " + level +
                "\nLevel Progress: " + xpSinceLevel + "/" + xpToLevel +
                "\nTotal Xp: " + totalExp +
                "\n\nAllocated Skill Points: " + spendablePoints + " Spendable" +
                "\nHealth - " + health + " (+" + (int) (health * 20 * (1 + (baseHealth * 0.2))) + "%)" +
                "\nAttack - " + attack + " (+" + (int) (attack * 20 * (1 + (baseAttack * 0.2))) + "%)" +
                "\nDefense - " + defense + " (-" + (int) (defense * (1 + (baseDefense * 0.2))) + "damage)" +
                "\nSpeed - " + speed + " (+" + (int) (speed * 20 * (1 + (baseSpeed * 0.2))) + "%)" +
                "\nIntelligence - " + intelligence;
    }

    //returning a string of base player info to be displayed
    string getBasePlayerInfo() {
        return "Spendable Skill Points: " + skillPoints +
                "\n" +
                "\nSkills:" +
                "\nHealth:" + baseHealth + "/5" + " (+ " + baseHealth * 20 + "% effect)" +
                "\nAttack:" + baseAttack + "/5" + " (+ " + baseAttack * 20 + "% effect)" +
                "\nDefense:" + baseDefense + "/5" + " (+ " + baseDefense * 20 + "% effect)" +
                "\nSpeed:" + baseSpeed + "/5" + " (+ " + baseSpeed * 20 + "% effect)" +
                "\n" +
                "\nThese points increase the effectiveness of other skill points on your player";
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
    public void addXp(int count) {
        totalExp += count;
        xpSinceLevel += count;
        expBar.setXP(xpSinceLevel);
        this.checkLevelStatus();
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

    //allows player to take damage (affected by difficulty)
    public void loseHp(int lose) {
        lose *= difficulty;
        lose -= (int) (defense * (1 + (baseDefense * 0.2)));
        if (curHp - lose <= 0) {
            curHp = 0;
            healthBar.setHealth(0);
        }
        else {
            curHp -= lose;
            healthBar.setHealth(curHp);
        }
        if (this.checkDeath()) {
            //this.showDeath();
        }
    }

    //function called when a player dies
    void kill() {
        respawnScreen.showDeath();
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
            modifyMaxHp((int) (0.2 * maxHp * (1 + (baseHealth * 0.2))));
        }
    }

    //increases attack stat
    public void increaseAttackStat() {
        if (spendablePoints > 0) {
            spendablePoints--;
            attack++;
            modifyAttackDamage((int) ((0.2 * attackDamage) * (1 + (baseAttack * 0.2))));
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

    //base stat functions
    //stat functions

    //increases health stat
    public void increaseBaseHealthStat() {
        if (skillPoints > 0 && baseHealth < 5) {
            skillPoints--;
            baseHealth++;
            //modifyMaxHp((int) (0.2 * maxHp));
        }
    }

    //increases attack stat
    public void increaseBaseAttackStat() {
        if (skillPoints > 0 && baseAttack < 5) {
            skillPoints--;
            baseAttack++;
            //modifyAttackDamage((int) (0.2 * attackDamage));
        }
    }

    //increases defense stat
    public void increaseBaseDefenseStat() {
        if (skillPoints > 0 && baseDefense < 5) {
            skillPoints--;
            baseDefense++;
        }
    }

    //increases speed stat
    public void increaseBaseSpeedStat() {
        if (skillPoints > 0 && baseSpeed < 5) {
            skillPoints--;
            baseSpeed++;
        }
    }

    //this command is run when the player chooses to attack
    void attackNearby() {
        //detecting hit enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, creatureLayers);

        //hitting the enemies
        foreach(Collider2D enemy in hitEnemies) {
            //enemy.GetComponent<Creature>().TakeDamage(attackDamage);
            //Debug.Log("We hit " + enemy.name);

            Creature creature = enemy.GetComponent<Creature>();
                if (creature != null) {
                    creature.TakeDamage(attackDamage);
                } else {
                    TalkingPanda tp = enemy.GetComponent<TalkingPanda>();
                    if (tp != null) {
                        tp.TakeDamage(attackDamage);
                    } else {
                        HostilePanda hp = enemy.GetComponent<HostilePanda>();
                             if (hp != null) {
                                 hp.TakeDamage(attackDamage);
                             } else {
                                // Handle the case where the enemy is neither a Creature nor an Interactable
                             }
                    }
                }
        }
    }

    //this command is run when the player chooses to interact
        void interactNearby() {
            //detecting hit enemies
            Collider2D[] interactables = Physics2D.OverlapCircleAll(attackPoint.position, interactRange, interactLayer);

            //interacting with the enemies
            foreach(Collider2D creature in interactables) {
                creature.GetComponent<TalkingPanda>().interact();
                //Debug.Log("We hit " + enemy.name);
            }
        }
}
