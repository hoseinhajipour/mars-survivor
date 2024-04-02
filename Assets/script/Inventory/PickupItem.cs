using UnityEngine;

public class PickupItem : MonoBehaviour
{

    public Item _item;
    // Add other properties like description, quantity, etc.

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

                // Disable or hide the item object in the scene
                Destroy(this.gameObject);
            }
        }
    }
}
