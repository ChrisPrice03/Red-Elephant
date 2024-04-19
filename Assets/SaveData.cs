using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public int difficulty; //0 peaceful, 1 is easy, 2 is normal, 3 is hard
    public int level;
    public int totalExp;
    public int xpToLevel;
    public int xpSinceLevel;
    public double levelXpMult;
    public int maxHp;
    public int curHp;
    public int attackDamage;
    public float attackRate;
    
    public int gold;

    public float seed;

    public float[] position;

    

    public SaveData (Player player, terrainGeneration terrain) 
    {
        difficulty = player.level;
        level = player.level;
        totalExp = player.totalExp;
        xpToLevel = player.xpToLevel;
        xpSinceLevel = player.xpSinceLevel;
        levelXpMult = player.levelXpMult;
        maxHp = player.maxHp;
        curHp = player.curHp;
        attackDamage = player.attackDamage;
        attackRate = player.attackRate;
        gold = player.gold;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        seed = terrain.seed;
    }
}
