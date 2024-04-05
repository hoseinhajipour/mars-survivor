using UnityEngine;

public class ItemAppend : MonoBehaviour
{
    private QuestManager questManager; // Reference to the QuestManager
    public Quest _quest;

    // Reference to the SaveAble component
    private SaveAble saveAble;

    void Start()
    {
        // Find the QuestManager in the scene
        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in the scene!");
        }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the item's name to PlayerPrefs using SaveAble's itemName
            if (saveAble != null)
            {
                PlayerPrefs.SetString(saveAble.name, "Collected");
                PlayerPrefs.Save();
            }

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
}
