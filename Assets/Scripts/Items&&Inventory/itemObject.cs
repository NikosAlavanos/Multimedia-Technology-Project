using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object - " + itemData.itemName;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroKnight>() != null)
        {
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
