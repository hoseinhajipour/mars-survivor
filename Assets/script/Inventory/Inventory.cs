using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // List of items in the inventory

    public List<ItemQuantityPair> ItemQuantityPairs = new List<ItemQuantityPair>();

    // Event delegate for inventory update
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    private const string PlayerPrefsKey = "InventoryItems"; // Key to store item data in PlayerPrefs

    private void Start()
    {
        LoadInventory();
    }

    // Method to add an item to the inventory
    public void AddItem(Item _item)
    {

        int existingItemIndex00 = ItemQuantityPairs.FindIndex(i => i.item.name == _item.name);

        int _quantity_final = 1;
        if (existingItemIndex00 != -1)
        {
            ItemQuantityPairs[existingItemIndex00].quantity += _item.quantity;
            ItemQuantityPairs[existingItemIndex00].item.quantity = _item.quantity;
            _quantity_final = ItemQuantityPairs[existingItemIndex00].quantity;
        }
        else
        {
            ItemQuantityPair newItemQuantityPair = new ItemQuantityPair();
            newItemQuantityPair.item = _item;
            newItemQuantityPair.item.quantity = _item.quantity;
            newItemQuantityPair.quantity = _item.quantity;
            ItemQuantityPairs.Add(newItemQuantityPair);
            _quantity_final = _item.quantity;
        }

        int existingItemIndex = items.FindIndex(i => i.name == _item.name);
        if (existingItemIndex != -1)
        {
            items[existingItemIndex] = ItemQuantityPairs[existingItemIndex00].item;
            items[existingItemIndex].quantity = ItemQuantityPairs[existingItemIndex00].quantity;
        }
        else
        {
            items.Add(_item);

        }
        SaveInventory();
        // Trigger inventory update event
        onInventoryChangedCallback?.Invoke();
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
                item.name = serializableItem.name;
                item.itemName = serializableItem.name;
                item.icon = serializableItem.icon;
                item.quantity = serializableItem.quantity;
                item.description = serializableItem.description;
                // Add the item to the inventory
                items.Add(item);

                ItemQuantityPair newItemQuantityPair = new ItemQuantityPair();
                newItemQuantityPair.item = item;
                newItemQuantityPair.item.quantity = item.quantity;
                newItemQuantityPair.quantity = item.quantity;
                ItemQuantityPairs.Add(newItemQuantityPair);
            }
        }
    }
}
