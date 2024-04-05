using UnityEngine;

[System.Serializable]
public class SerializableItem
{
    public string name;
    public string itemName;
    public Sprite icon;
    public int quantity;
    public string description;

    public SerializableItem(Item item)
    {
        name = item.name;
        itemName = item.itemName;
        icon = item.icon;
        quantity = item.quantity;
        description = item.description;
    }
}