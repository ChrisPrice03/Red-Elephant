using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //adding features attached to a player
    private int level = 0;
    private int totalExp = 0;
    private int xpToLevel = 20;
    private int xpSinceLevel = 0;
    private double levelXpMult = 2;
    private int maxHp = 100;
    private int curHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            }
            case 1: {
                levelXpMult = 2;
            }
            case 2: {
                levelXpMult = 5;
            }
            case 3: {
                levelXpMult = 8;
            }
            default: {
                levelXpMult = 2;
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
        level++;
        xpSinceLevel -= xpToLevel;
        xpToLevel *= levelXpMult;
    }

    //gives xp tp a player
    void addXp(int count) {
        totalExp += addXp;
        xpSinceLevel += addXp;
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
        curHp += maxHp;
    }

    //allows player to take damage
    void loseHp(int lose) {
        curHp -= loss;
        if (this.checkDeath()) {
            this.showDeath();
        }
    }

    //allows player to change maxHp
    void modifyMaxHp(int change) {
        maxHp += change;
    }
}
