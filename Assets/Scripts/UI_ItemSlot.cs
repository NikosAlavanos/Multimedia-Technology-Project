using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;


    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        if (itemImage != null)
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
}
