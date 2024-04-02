using UnityEngine;

public class MiningZone : MonoBehaviour
{
    public int healthLevel = 3; // Health level of the mining zone
    public float miningStepDuration = 3f; // Duration of each mining step
    public GameObject[] objectsToShow; // Array of objects to show based on health level
    public Inventory inventory; // Reference to the player's inventory
    public Item[] rewardItems; // Array of items to reward after mining completion

    private bool isMining = false; // Flag to track if player is mining
    private float miningTimer = 0f; // Timer for mining progress

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartMining();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopMining();
        }
    }

    private void StartMining()
    {
        isMining = true;
        miningTimer = 0f; // Reset the timer
        Debug.Log("Mining started...");
    }

    private void StopMining()
    {
        isMining = false;
        Debug.Log("Mining stopped...");
    }

    private void Update()
    {
        if (isMining)
        {
            // Update mining progress
            miningTimer += Time.deltaTime;
            if (miningTimer >= miningStepDuration)
            {
                Mine();
                miningTimer = 0f; // Reset the timer for the next step
            }
        }
    }

    private void Mine()
    {
        healthLevel--; // Decrease the health level
        if (healthLevel <= 0)
        {
            MiningComplete();
        }
        else
        {
            UpdateMiningState();
        }
    }

    private void MiningComplete()
    {
        Debug.Log("Mining complete!");
        AddRewardToInventory();
        Destroy(gameObject); // Destroy the mining zone
    }

    private void AddRewardToInventory()
    {
        if (inventory != null)
        {
            // Add reward items to inventory based on health level or any other condition you want
            for (int i = 0; i < rewardItems.Length; i++)
            {
                if (rewardItems[i] != null)
                {
                    inventory.AddItem(rewardItems[i]);
                    Debug.Log("Item added to inventory: " + rewardItems[i].name);
                }
            }
        }
        else
        {
            Debug.LogWarning("Inventory reference not set!");
        }
    }

    private void UpdateMiningState()
    {
        // Ensure all objects are inactive
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // Activate the object at the current health level index
        int objectIndex = Mathf.Clamp(healthLevel, 0, objectsToShow.Length - 1);
        if (objectsToShow[objectIndex] != null)
        {
            objectsToShow[objectIndex].SetActive(true);
        }
    }


}
