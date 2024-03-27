using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainGeneration : MonoBehaviour
{
    public int worldSize = 100;
    public Sprite grassBlock;
    public Sprite dirtBlock;
    public float noiseFreq = 0.08f;
    public float terrainFreq = .04f;
    public float heightMult = 25f;
    public int heightAdd = 35;
    public float seed;
    public Texture2D noiseTexture; 

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
                    GameObject newTile = new GameObject();
                    newTile.transform.parent = this.transform;
                    newTile.AddComponent<SpriteRenderer>();
                    newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
                    newTile.name = tileSprite.name;
                    newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
                }
                
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
