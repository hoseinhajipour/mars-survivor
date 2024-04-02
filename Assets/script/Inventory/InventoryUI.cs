using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // Prefab for the inventory slot UI
    public Transform inventoryPanel; // Parent object where inventory slots will be placed

    private List<GameObject> inventorySlots = new List<GameObject>(); // List to store references to inventory slot UI objects

    // Reference to the inventory component
    private Inventory inventory;

    private void Start()
    {
        // Find the Inventory component in the scene
        inventory = FindObjectOfType<Inventory>();

        // Subscribe to the inventory update event
        if (inventory != null)
        {
            inventory.onInventoryChangedCallback += UpdateInventoryUI;
        }

        // Initialize the inventory UI
        InitializeInventoryUI();
    }

    // Method to initialize the inventory UI
    private void InitializeInventoryUI()
    {
        // Clear existing inventory slots
        ClearInventoryUI();

        // Create UI elements for each item in the inventory
        foreach (Item item in inventory.items)
        {
            CreateInventorySlot(item);
        }
    }

    // Method to create an inventory slot UI element
    private void CreateInventorySlot(Item item)
    {
        GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
        inventorySlots.Add(slot);

        // Set the icon and quantity of the item in the inventory slot
        InventorySlotUI slotUI = slot.GetComponent<InventorySlotUI>();
        if (slotUI != null)
        {
            slotUI.SetItem(item);
        }
    }

    // Method to update the inventory UI when the inventory changes
    private void UpdateInventoryUI()
    {
        // Update UI elements to match the inventory state
        InitializeInventoryUI();
    }

    // Method to clear the inventory UI
    private void ClearInventoryUI()
    {
        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();
    }
}
