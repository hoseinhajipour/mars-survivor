using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableInventory
{
    public List<SerializableItem> items;

    public SerializableInventory(List<Item> items)
    {
        this.items = new List<SerializableItem>();

        // Convert each Item object to its serializable form
        foreach (Item item in items)
        {
            this.items.Add(new SerializableItem(item));
        }
    }
}