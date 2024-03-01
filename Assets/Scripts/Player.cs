using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //adding features attached to a player
    public int level = 0;
    public int totalExp = 0;
    public int xpToLevel = 20;
    public int xpSinceLevel = 0;
    public double levelXpMult = 2;
    public int maxHp = 100;
    public int curHp = 100;

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

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setMaxHealth(maxHp);
        healthBar.setHealth(curHp);
        expBar.setMaxXP(xpToLevel);
        expBar.setXP(xpSinceLevel);
        charInfoText.updateText(getPlayerInfo());
        healthButton.onClick.AddListener(increaseHealthStat);
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

    //stat functions

    //increases health stat
    public void increaseHealthStat() {
        health++;
    }

    //increases attack stat
    public void increaseAttackStat() {
        attack++;
    }

    //increases defense stat
    public void increaseDefenseStat() {
        defense++;
    }

    //increases speed stat
    public void increaseSpeedStat() {
        speed++;
    }

    //increases intelligence stat
    public void increaseIngelligencestat() {
        intelligence++;
    }
}
