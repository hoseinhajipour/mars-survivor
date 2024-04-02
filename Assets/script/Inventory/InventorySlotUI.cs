using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage; // Reference to the image component displaying the item icon
    public TextMeshProUGUI quantityText; // Reference to the TextMeshPro component displaying the item quantity

    // Method to set the item icon and quantity in the inventory slot UI
    public void SetItem(Item item)
    {
        if (item != null)
        {
            iconImage.sprite = item.icon;
            iconImage.gameObject.SetActive(true);
            quantityText.text = item.quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }
        else
        {
            iconImage.gameObject.SetActive(false);
            quantityText.gameObject.SetActive(false);
        }
    }
}
