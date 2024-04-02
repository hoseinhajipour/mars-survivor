using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;
    public void SetItemIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }
    public void SetItemQuantity(int quantity)
    {
        quantityText.text = quantity.ToString();
    }
}
