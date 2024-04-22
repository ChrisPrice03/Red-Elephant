using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainGeneration : MonoBehaviour
{
    public int worldSize = 100;
    public Sprite grassBlock;
    public Sprite dirtBlock;
    public Sprite chest;

    public Sprite DungTP;

    public Loot loot_1;
    public Loot loot_2;
    public Loot loot_3;
    public GameObject lootPre;

    
    public float noiseFreq = 0.08f;
    public float terrainFreq = .04f;
    public float heightMult = 25f;
    public int heightAdd = 35;
    public float seed;
    public Texture2D noiseTexture; 
    public List<Vector2> worldTiles = new List<Vector2>();
    public List<GameObject> worldTileObjects = new List<GameObject>();

    public Player player;
    public void Start() {
        LoadGame();
    }
    public void LoadGame() 
    {
        SaveData data = SaveSystem.loadPlayer();
        if (data == null) {
            seed = Random.Range(-10000, 100000);
            GenerateNoiseTexture();
            GenerateTerrain();
        }
        else {
            player.difficulty = data.level;
            player.level = data.level;
            player.totalExp = data.totalExp;
            player.xpToLevel = data.xpToLevel;
            player.xpSinceLevel = data.xpSinceLevel;
            player.levelXpMult = data.levelXpMult;
            player.maxHp = data.maxHp;
            player.curHp = data.curHp;
            player.attackDamage = data.attackDamage;
            player.attackRate = data.attackRate;
            player.gold = data.gold;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            player.transform.position = position;
            

            seed = data.seed;
            GenerateNoiseTexture();
            GenerateTerrain();
        }
        
    }

    public void SaveGame() {
        SaveSystem.SavePlayer(player, this);
    }

    
    public void GenerateTerrain() 
    {
        bool tpPlace = false; 
        for (int x = 0; x < worldSize; x++) 
        {
            float height = Mathf.PerlinNoise((x+seed) * terrainFreq, (seed) * terrainFreq) * heightMult + heightAdd;
            for (int y = 0; y < height; y++) 
            {
                Sprite tileSprite;
                if (y < height - 1) {
                    tileSprite = dirtBlock;
                } 
                else {
                    tileSprite = grassBlock;
                }
                if (noiseTexture.GetPixel(x,y).r > 0.2f) {
                    if (!worldTiles.Contains(new Vector2Int(x,y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize) {
                        int chestChance = Random.Range(0, 101);
                        int tPChance = Random.Range(0, 101);
                        if (y > height - 1) {
                            if (tPChance < 5) {
                                if (tpPlace == false) {
                                    tileSprite = DungTP;
                                    tpPlace = true;
                                }
                            }
                        }
                        if ((y + 1) > height) {
                            if (chestChance < 5) {
                                tileSprite = chest;
                            }
                        }
                        GameObject newTile = new GameObject();
                        newTile.transform.parent = this.transform;
                        newTile.AddComponent<SpriteRenderer>();
                        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
                        newTile.AddComponent<BoxCollider2D>();
                        newTile.GetComponent<BoxCollider2D>().size = Vector2.one;

                        newTile.name = tileSprite.name;

                        if (tileSprite == chest) {
                            newTile.AddComponent<LootBag>();
                            newTile.GetComponent<LootBag>().droppedItemPrefab = lootPre;
                            newTile.GetComponent<LootBag>().lootList.Add(loot_1);
                            newTile.GetComponent<LootBag>().lootList.Add(loot_2);
                            newTile.GetComponent<LootBag>().lootList.Add(loot_3);
                        }
                        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

                        worldTiles.Add(newTile.transform.position - (Vector3.one * .5f));
                        
                        worldTileObjects.Add(newTile);
                    }

                    
                }
                
            }
        }
    }

    public void OpenLootBox(int x, int y) {
        
        if (worldTiles.Contains(new Vector2Int(x,y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize) {
            Debug.Log(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].name);
            
            if(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].name == chest.name) {
                worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].GetComponent<LootBag>().InstantiateLoot(new Vector2(x,y));
                Destroy(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))]);
            }
            if(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].name == DungTP.name) {
                SaveGame();
                Vector3 position;
                position.x = -107;
                position.y = -101;
                position.z = 0;
                player.transform.position = position;
                
            }
            
        }

    }
    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);

        for (int x = 0; x < noiseTexture.width; x++) 
        {
            for (int y = 0; y < noiseTexture.height; y++) 
            {
                float v = Mathf.PerlinNoise((x+seed) * noiseFreq, (y+seed) * noiseFreq);
                noiseTexture.SetPixel(x,y, new Color(v,v,v));
            }
        }
        noiseTexture.Apply();
    }

}
