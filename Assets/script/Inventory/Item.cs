using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    public string name="item name"; // Name of the item
    public Sprite icon; // Icon representing the item
    public int quantity; // Quantity of the item in the inventory
    public string description; // Description of the item
    public int weight; // Weight of the item
    public bool showInInventory = true;
}
