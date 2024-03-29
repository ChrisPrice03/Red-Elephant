using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();
    
    Loot GetDroppedItem() {
        int roll = Random.Range(1, 101);

        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in lootList) {
            if (roll <= item.dropChance) {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0) {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }

    public void InstantiateLoot(Vector2 spawnPos) {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null) {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPos, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
            lootGameObject.AddComponent<BoxCollider2D>();
            lootGameObject.GetComponent<BoxCollider2D>().size = Vector2.one * 0.5f;
            lootGameObject.GetComponent<Items>().sprite = droppedItem.lootSprite;
            //float dropForce = 300f;

            //Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));

            //lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
    
}
