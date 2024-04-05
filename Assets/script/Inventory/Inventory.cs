using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // List of items in the inventory
    public int maxInventorySize = 1000; // Maximum number of items that can be held in the inventory

    // Event delegate for inventory update
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    private const string PlayerPrefsKey = "InventoryItems"; // Key to store item data in PlayerPrefs

    private void Start()
    {
        LoadInventory();
    }

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
                items.Add(item);
            }

            // Trigger inventory update event
            onInventoryChangedCallback?.Invoke();

            // Save inventory data
            SaveInventory();
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

        // Save inventory data
        SaveInventory();
    }

    // Method to save inventory data to PlayerPrefs
    // Method to save inventory data to PlayerPrefs
    private void SaveInventory()
    {
        // Serialize the list of items
        string json = JsonUtility.ToJson(new SerializableInventory(items));
     //   Debug.Log(json);
        // Save the JSON data to PlayerPrefs
        PlayerPrefs.SetString(PlayerPrefsKey, json);
        PlayerPrefs.Save(); // Make sure to call Save() to ensure data is written immediately
    }

    // Method to load inventory data from PlayerPrefs
    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            // Retrieve JSON data from PlayerPrefs
            string json = PlayerPrefs.GetString(PlayerPrefsKey);

            // Deserialize the JSON data into a serializable format
            SerializableInventory serializedInventory = JsonUtility.FromJson<SerializableInventory>(json);

            // Clear existing items
            items.Clear();

            // Iterate over the deserialized items
            foreach (SerializableItem serializableItem in serializedInventory.items)
            {
                // Convert the serializable item back to an Item object
                Item item = new Item();
                item.itemName = serializableItem.itemName;
                item.icon = serializableItem.icon;
                item.quantity = serializableItem.quantity;
                item.description = serializableItem.description;
                item.weight = serializableItem.weight;
                item.showInInventory = serializableItem.showInInventory;

                // Add the item to the inventory
                items.Add(item);
            }
        }
    }

    // Serializable class to properly serialize list of items
    [System.Serializable]
    private class SerializableInventory
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

    // Serializable class for Item objects
    [System.Serializable]
    private class SerializableItem
    {
        public string itemName;
        public Sprite icon;
        public int quantity;
        public string description;
        public int weight;
        public bool showInInventory;

        public SerializableItem(Item item)
        {
            itemName = item.itemName;
            icon = item.icon;
            quantity = item.quantity;
            description = item.description;
            weight = item.weight;
            showInInventory = item.showInInventory;
        }
    }


}
