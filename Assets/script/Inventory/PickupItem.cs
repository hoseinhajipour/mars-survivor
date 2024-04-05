using UnityEngine;

public class PickupItem : MonoBehaviour
{

    public Item _item;
    // Add other properties like description, quantity, etc.

    // Reference to the SaveAble component
    private SaveAble saveAble;


    void Start()
    {
        // Try to get the SaveAble component attached to this GameObject
        saveAble = GetComponent<SaveAble>();
        if (saveAble == null)
        {
            Debug.LogError("SaveAble component not found on the GameObject!");
        }

        // Check if the item name exists in PlayerPrefs using the SaveAble's itemName
        if (saveAble != null && !string.IsNullOrEmpty(saveAble.name) && PlayerPrefs.HasKey(saveAble.name))
        {
            // If the item name exists, destroy the GameObject
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger collider


        if (other.CompareTag("Player"))
        {
            // Get the inventory component from the player
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                // Create a new Item instance based on the PickupItem's properties
                Item newItem = new Item
                {
                    name = _item.name,
                    icon = _item.icon
                    // Add other properties as needed
                };

                // Add the new item to the inventory
                inventory.AddItem(newItem);


                // Save the item's name to PlayerPrefs using SaveAble's itemName
                if (saveAble != null)
                {
                    PlayerPrefs.SetString(saveAble.name, "Collected");
                    PlayerPrefs.Save();
                }


                // Disable or hide the item object in the scene
                Destroy(this.gameObject);
            }
        }
    }
}
