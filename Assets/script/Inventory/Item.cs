using UnityEngine;


[CreateAssetMenu(fileName = "new item", menuName = "Inventory/item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon; // Icon representing the item
    public int quantity = 1; // Quantity of the item in the inventory
    public string description; // Description of the item

}
