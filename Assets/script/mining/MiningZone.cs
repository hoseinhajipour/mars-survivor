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
    private Animator playerAnimator;
    public GameObject playerObject;
    private void Start()
    {
        // Find the player object by tag
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            // Get the child object with the Animator component
            playerAnimator = playerObject.GetComponentInChildren<Animator>();
            if (playerAnimator == null)
            {
                Debug.LogError("Animator component not found on child object of the player!");
            }
        }
        else
        {
            Debug.LogError("Player object not found with the tag 'Player'!");
        }
    }

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

        // Play the "mining" animation on the player
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isMining", true);
        }
    }

    private void StopMining()
    {
        isMining = false;
        Debug.Log("Mining stopped...");

        // Stop the "mining" animation on the player
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isMining", false);
        }
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

        if (isMining && playerObject != null)
        {
            // Calculate the direction to the mining zone
            Vector3 direction = (transform.position - playerObject.transform.position).normalized;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Smoothly rotate towards the target rotation
            float rotationSpeed = 5f; // Adjust the rotation speed as needed
            playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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

        // Stop the "mining" animation on the player
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isMining", false);
        }

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
