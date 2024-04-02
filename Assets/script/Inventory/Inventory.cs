using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // List of items in the inventory
    public int maxInventorySize = 10; // Maximum number of items that can be held in the inventory

    // Add event delegates for inventory update
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    // Method to add an item to the inventory
    public void AddItem(Item item)
    {
        // Check if the inventory is not full
        if (items.Count < maxInventorySize)
        {
            // Check if the item is already in the inventory
            Item existingItem = items.Find(i => i.name == item.name);
            if (existingItem != null)
            {
                // If the item already exists, increment its quantity
                existingItem.quantity++;
            }
            else
            {
                // Otherwise, add the item to the inventory with a quantity of 1
                items.Add(new Item
                {
                    name = item.name,
                    icon = item.icon,
                    quantity = 1
                });
            }
            
            // Trigger inventory update event
            onInventoryChangedCallback?.Invoke();
        }
        else
        {
            Debug.LogWarning("Inventory is full!");
            // Handle full inventory case
        }
    }

    // Method to remove an item from the inventory
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        // Trigger inventory update event
        onInventoryChangedCallback?.Invoke();
    }

    // Method to check if an item is in the inventory
    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    // Additional methods for inventory management can be added here
}
