using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftingStation : MonoBehaviour
{
    public List<ItemQuantityPair> requiredItemQuantities = new List<ItemQuantityPair>();
    public GameObject craftingPanel; // Reference to the crafting panel UI
    public string errorMessage = "You don't have the required items to craft!"; // Error message to display if items are missing
    public GameObject prefabToCreate; // Prefab to create after crafting process ends
    public List<ItemQuantityPair> rewardItems = new List<ItemQuantityPair>(); // List of items and their quantities as rewards
    public float craftingDuration = 3f; // Duration of the crafting process

    private bool isCraftingInProgress = false; // Flag to track if crafting is in progress
    private Inventory playerInventory; // Reference to the player's inventory

    public Image craftingProgressImage; // Reference to the image component displaying crafting progress
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro component displaying the timer
    private float craftingTimer; // Timer for the crafting process

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the inventory component from the player
            playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                // Check if the player has all the required items
                bool hasRequiredItems = CheckRequiredItems(playerInventory);

                if (hasRequiredItems && !isCraftingInProgress)
                {
                    // Subtract required items from inventory
                    SubtractRequiredItemsFromInventory(playerInventory);
                    // Start crafting
                    StartCrafting();
                }
                else if (!hasRequiredItems)
                {
                    // Show error message
                    Debug.LogError(errorMessage);
                    // You can also display the error message on the UI or handle it in any other way
                }
            }
        }
    }

    // Method to check if the player has all the required items for crafting
    private bool CheckRequiredItems(Inventory inventory)
    {
        // Iterate through each required item
        foreach (ItemQuantityPair pair in requiredItemQuantities)
        {
            Item requiredItem = pair.item;
            int requiredQuantity = pair.quantity;

            // Check if the player has the required item in the inventory
            bool foundItem = false;
            foreach (Item item in inventory.items)
            {
                if (item.name == requiredItem.name && item.quantity >= requiredQuantity)
                {
                    foundItem = true;
                    break;
                }
            }

            // If the required item is not found or the quantity is insufficient, return false
            if (!foundItem)
            {
                return false;
            }

        }

        // If all checks pass, return true
        return true;
    }


    // Method to subtract required items from the player's inventory
    private void SubtractRequiredItemsFromInventory(Inventory inventory)
    {
        // Iterate through each required item
        foreach (ItemQuantityPair pair in requiredItemQuantities)
        {
            Item requiredItem = pair.item;
            int requiredQuantity = pair.quantity;

            // Iterate through the player's inventory
            for (int j = 0; j < inventory.items.Count; j++)
            {
                Item inventoryItem = inventory.items[j];
                // Check if the inventory contains the required item
                if (inventoryItem.name == requiredItem.name)
                {
                    // Subtract the required quantity from the inventory
                    inventoryItem.quantity -= requiredQuantity;

                    // Remove the item from inventory if its quantity becomes zero or less
                    if (inventoryItem.quantity <= 0)
                    {
                        inventory.items.RemoveAt(j);
                    }
                    break;
                }
            }
        }
    }


    // Method to start crafting
    private void StartCrafting()
    {
        isCraftingInProgress = true;
        craftingTimer = 0f; // Reset crafting timer

        // Implement crafting logic here
        // For example, show the crafting panel UI
        if (craftingPanel != null)
        {
            craftingPanel.SetActive(true);
        }

        // Wait for crafting duration and then create the prefab
        StartCoroutine(CreatePrefabAfterDelay());
    }

    // Coroutine to create the prefab after crafting duration
    private System.Collections.IEnumerator CreatePrefabAfterDelay()
    {
        yield return new WaitForSeconds(craftingDuration);

        // Create the prefab
        if (prefabToCreate != null)
        {
            Instantiate(prefabToCreate, transform.position, Quaternion.identity);
        }

        // Reward items to the player's inventory
        RewardItems();

        // Reset crafting in progress flag
        isCraftingInProgress = false;

        // Hide the crafting panel
        if (craftingPanel != null)
        {
            craftingPanel.SetActive(false);
        }
    }

    // Method to reward items to the player's inventory
    private void RewardItems()
    {
        foreach (ItemQuantityPair rewardItemPair in rewardItems)
        {
            rewardItemPair.item.quantity = rewardItemPair.quantity;
            playerInventory.AddItem(rewardItemPair.item);
        }
    }

    private void UpdateCraftingProgress()
    {
        if (craftingProgressImage != null)
        {
            // Calculate fill amount based on crafting timer progress
            float fillAmount = Mathf.Clamp01(craftingTimer / craftingDuration);
            craftingProgressImage.fillAmount = fillAmount;
        }

        if (timerText != null)
        {
            // Display remaining crafting time
            float remainingTime = craftingDuration - craftingTimer;
            timerText.text = remainingTime.ToString("0.0");
        }
    }

    private void Update()
    {
        if (isCraftingInProgress)
        {
            // Update crafting timer
            craftingTimer += Time.deltaTime;

            // Update crafting progress UI
            UpdateCraftingProgress();
        }
    }
}

