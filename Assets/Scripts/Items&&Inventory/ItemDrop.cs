using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            {
                dropList.Add(possibleDrop[i]);
                Debug.Log("Added item to drop list: " + possibleDrop[i].itemName);
            }
        }

        if (dropList.Count == 0)
        {
            Debug.LogWarning("No items were added to the drop list.");
            return;
        }

        int itemsToDrop = Mathf.Min(possibleItemDrop, dropList.Count);

        for (int i = 0; i < itemsToDrop; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count)];
            dropList.Remove(randomItem);
            Debug.Log("Dropping item: " + randomItem.itemName);
            DropItem(randomItem);
        }
    }



    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-3, 3), Random.Range(15, 20));


        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
