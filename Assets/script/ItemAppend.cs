using UnityEngine;

public class ItemAppend : MonoBehaviour
{
    private QuestManager questManager; // Reference to the QuestManager
    public Quest _quest;

    public string itemName = null; // Unique name for the item

    void Start()
    {
        // Find the QuestManager in the scene
        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in the scene!");
        }

        // Check if the item name exists in PlayerPrefs
        if (!string.IsNullOrEmpty(itemName) && PlayerPrefs.HasKey(itemName))
        {
            // If the item name exists, destroy the GameObject
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the item's name to PlayerPrefs
            PlayerPrefs.SetString(itemName, "Collected");
            PlayerPrefs.Save();

            // Trigger the onComplete event in the QuestManager to mark the active quest as complete
            if (questManager != null)
            {
                _quest.UpdateStatus(QuestStatus.Complete);
                questManager.ActivateNextQuest(_quest.NextQuest);
            }

            // Destroy the item object
            Destroy(gameObject);
        }
    }

    // Generate a unique name for the item
    private string GenerateUniqueName()
    {
        // Use a fixed prefix followed by a randomly generated suffix
        string prefix = gameObject.name + "_";
        string suffix = Random.Range(1000, 10000).ToString(); // Generate a random number as suffix
        return prefix + suffix;
    }

    // Reset method is called when the script is added to a GameObject or reset from the Inspector window
    void Reset()
    {
        itemName = GenerateUniqueName();
    }
}
