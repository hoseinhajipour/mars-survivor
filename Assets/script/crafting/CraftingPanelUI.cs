using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CraftingPanelUI : MonoBehaviour
{
    public GameObject itemPrefab; // Reference to the prefab for displaying items
    public Transform itemListParent; // Parent transform where items will be instantiated

    private CraftingStation craftingStation; // Reference to the CraftingStation script

    private void Start()
    {
        // Find the CraftingStation script in the scene
        craftingStation = FindObjectOfType<CraftingStation>();

        // Update the UI panel with required items information
        UpdateRequiredItemsUI();
    }

    private void UpdateRequiredItemsUI()
    {
        if (craftingStation != null && itemListParent != null && itemPrefab != null)
        {
            // Clear previous items
            foreach (Transform child in itemListParent)
            {
                Destroy(child.gameObject);
            }

            // Iterate through each required item quantity pair
            foreach (ItemQuantityPair pair in craftingStation.requiredItemQuantities)
            {
                // Instantiate the item prefab
                GameObject itemGO = Instantiate(itemPrefab, itemListParent);

                // Get the ItemUI component of the instantiated item
                ItemUI itemUI = itemGO.GetComponent<ItemUI>();

                // Set the icon of the item
                if (itemUI != null && pair.item.icon != null)
                {
                    itemUI.SetItemIcon(pair.item.icon);
    
                    itemUI.SetItemQuantity(pair.quantity);
                }
            }
        }
    }
}
