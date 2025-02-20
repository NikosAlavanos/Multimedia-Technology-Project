using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;


    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.icon; //assigning sprite of the image to the icon of the item

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString(); //converting numbers to text
            }
            else
            {
                itemText.text = "";
            }

        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData) //its called whenever you use your pointer down on this object that has this Interface
    {
        Debug.Log($"OnPointerDown called. Item: {item}, Item Data: {item?.data}");
        if (item == null || item.data == null) // Prevent null reference errors
            return;

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }

        if (item.data.itemType == ItemType.Equipment)
            Inventory.instance.EquipItem(item.data);
    }
}
