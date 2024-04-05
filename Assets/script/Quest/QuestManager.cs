using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Image questIconImage; // Reference to the UI Image component for the quest icon
    public TextMeshProUGUI questNameText; // Reference to the TextMeshProUGUI component for the quest name

    public Quest activeQuest; // Reference to the currently active quest

    void Start()
    {
        // Find the current active quest (You can implement your own logic here)
        activeQuest = LoadCurrentQuest();

        // Update the UI to display the active quest
        if (activeQuest != null)
        {
            // Set the quest icon
            if (questIconImage != null && activeQuest.icon != null)
            {
                questIconImage.sprite = activeQuest.icon;
                questIconImage.gameObject.SetActive(true);
            }

            // Set the quest name
            if (questNameText != null)
            {
                questNameText.text = activeQuest.questName;
            }
        }
    }

    // Method to find the current active quest (You can implement your own logic here)
    private Quest LoadCurrentQuest()
    {

       string currentQuestName= PlayerPrefs.GetString("currentQuest");
        // For simplicity, let's say the active quest is the first one in the list
        // You can replace this with your own logic to find the active quest
        Quest[] quests = Resources.FindObjectsOfTypeAll<Quest>();
        foreach (Quest quest in quests)
        {
            if (quest.questName == currentQuestName)
            {
                return quest;
            }
        }


        return quests[0]; 
    }

    // Method to change the status of the active quest
    public void ChangeQuestStatus(QuestStatus newStatus)
    {
        if (activeQuest != null)
        {
            activeQuest.UpdateStatus(newStatus);

            // You may want to trigger some events or UI updates based on the new quest status

            // For example, if the quest is complete, activate the next quest
            if (newStatus == QuestStatus.Complete)
            {
                ActivateNextQuest(activeQuest.NextQuest);
            }
        }
    }

    // Method to activate the next quest
    public void ActivateNextQuest(Quest newQuest)
    {

        // Update the active quest reference
        activeQuest = newQuest;
        activeQuest.UpdateStatus(QuestStatus.Active);
PlayerPrefs.SetString("currentQuest",activeQuest.questName);
        // Update the UI to display the new active quest
        if (questIconImage != null && activeQuest.icon != null)
        {
            questIconImage.sprite = activeQuest.icon;
        }

        if (questNameText != null)
        {
            questNameText.text = activeQuest.questName;
        }
    }
}
