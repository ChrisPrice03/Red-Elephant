using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainGeneration : MonoBehaviour
{
    public int worldSize = 100;
    public Sprite grassBlock;
    public Sprite dirtBlock;
    public Sprite chest;
    public Sprite portalSprite;

    public Loot loot_1;
    public Loot loot_2;
    public Loot loot_3;
    public GameObject lootPre;
    public GameObject fixedPortalObject;

    public float noiseFreq = 0.08f;
    public float terrainFreq = .04f;
    public float heightMult = 25f;
    public int heightAdd = 35;
    public float seed;
    
    public Texture2D noiseTexture; 
    public List<Vector2> worldTiles = new List<Vector2>();
    public List<GameObject> worldTileObjects = new List<GameObject>();

    

    public void Start() 
    {
        seed = Random.Range(-10000, 100000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    public void GenerateTerrain() 
    {
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
                        int portalChance = Random.Range(0, 101);

                        if (y > height - 1) {
                            if (chestChance < 5) {
                                tileSprite = chest;
                            }
                            
                            if (portalChance < 3) { // Adjust the chance as needed
                                SpawnPortal(x, y);
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

    private void SpawnPortal(int x, int y)
    {
        GameObject portalObject = new GameObject("Portal");
        portalObject.tag = "enter shop portal";
        portalObject.transform.position = new Vector3(x + 5f, y + 5f, 0); // Adjust the Z position if needed
        portalObject.transform.parent = transform;

        // Add SpriteRenderer component and assign portal sprite
        SpriteRenderer portalRenderer = portalObject.AddComponent<SpriteRenderer>();
        portalRenderer.sprite = portalSprite; // Use the assigned portal sprite

        // Add BoxCollider2D for interaction
        BoxCollider2D portalCollider = portalObject.AddComponent<BoxCollider2D>();
        portalCollider.isTrigger = true;

        // Add functionality directly to the portal GameObject
        // portalObject.AddComponent<PortalLogic>();


        PortalLogic portalLogic = portalObject.AddComponent<PortalLogic>();
    // Assign the destination portal

        portalLogic.destinationPortal = fixedPortalObject;
    }

    public void OpenLootBox(int x, int y) {
        
        if (worldTiles.Contains(new Vector2Int(x,y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize) {
            Debug.Log(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].name);
            if(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].name == chest.name) {
                worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))].GetComponent<LootBag>().InstantiateLoot(new Vector2(x,y));
            }
            
            Destroy(worldTileObjects[worldTiles.IndexOf(new Vector2(x,y))]);
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
