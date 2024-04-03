using UnityEngine;
using UnityEngine.UI;

public class MiningZone : MonoBehaviour
{
    public int healthLevel = 3;
    public float miningStepDuration = 3f;
    public GameObject[] objectsToShow;
    public Inventory inventory;
    public Item[] rewardItems;

    public GameObject progressPanel; // Reference to the progress panel
    public Image progressBar; // Reference to the progress bar image component

    private bool isMining = false;
    private float miningTimer = 0f;
    private Animator playerAnimator;
    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
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

        // Hide the progress panel initially
        if (progressPanel != null)
        {
            progressPanel.SetActive(false);
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
        miningTimer = 0f;
        Debug.Log("Mining started...");

        // Show the progress panel
        if (progressPanel != null)
        {
            progressPanel.SetActive(true);
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isMining", true);
        }
    }

    private void StopMining()
    {
        isMining = false;
        Debug.Log("Mining stopped...");

        // Hide the progress panel
        if (progressPanel != null)
        {
            progressPanel.SetActive(false);
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isMining", false);
        }
    }

    private void Update()
    {
        if (isMining)
        {
            miningTimer += Time.deltaTime;
            if (miningTimer >= miningStepDuration)
            {
                Mine();
                miningTimer = 0f;
            }

            // Calculate total mining progress
            float totalMiningProgress = 1f - (float)healthLevel / 3f; // Assuming healthLevel starts from 3

            // Invert the fill amount
            totalMiningProgress = 1f - totalMiningProgress;

            // Update progress bar fill amount
            if (progressBar != null)
            {
                progressBar.fillAmount = totalMiningProgress;
            }
        }

        if (isMining && playerObject != null)
        {
            Vector3 direction = (transform.position - playerObject.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            float rotationSpeed = 5f;
            playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }


    private void Mine()
    {
        healthLevel--;
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

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isMining", false);
        }

        Destroy(gameObject);
    }

    private void AddRewardToInventory()
    {
        if (inventory != null)
        {
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
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        int objectIndex = Mathf.Clamp(healthLevel, 0, objectsToShow.Length - 1);
        if (objectsToShow[objectIndex] != null)
        {
            objectsToShow[objectIndex].SetActive(true);
        }
    }
}
