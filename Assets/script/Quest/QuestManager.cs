using UnityEngine;
using TMPro; // Import TextMeshPro

public class QuestManager : MonoBehaviour
{
    public Quest questToManage;
    public TextMeshProUGUI lastActiveQuestText;
    public TextMeshProUGUI questStatusText;
    public Transform childQuestsParent;
    public GameObject childQuestPrefab;

    private void Start()
    {
        // Subscribe to quest events
        if (questToManage != null)
        {
            questToManage.onComplete.AddListener(OnQuestComplete);
            questToManage.onFail.AddListener(OnQuestFail);
        }

        UpdateUI();
    }

    private void OnQuestComplete()
    {
        questToManage.status = QuestStatus.Complete;
        UpdateUI();
        Debug.Log("Quest Completed: " + questToManage.questName);
        // Add your logic for what happens when the quest is completed
    }

    private void OnQuestFail()
    {
        questToManage.status = QuestStatus.Fail;
        UpdateUI();
        Debug.Log("Quest Failed: " + questToManage.questName);
        // Add your logic for what happens when the quest fails
    }

    private void UpdateUI()
    {
        if (lastActiveQuestText != null)
            lastActiveQuestText.text = questToManage.questName;

        if (questStatusText != null)
            questStatusText.text = questToManage.status.ToString();

        UpdateChildQuestsUI();
    }

    private void UpdateChildQuestsUI()
    {
        foreach (Transform child in childQuestsParent)
        {
            Destroy(child.gameObject);
        }

        if (questToManage.childQuests != null)
        {
            foreach (var child in questToManage.childQuests)
            {
                GameObject childQuestUI = Instantiate(childQuestPrefab, childQuestsParent);
                TextMeshProUGUI childQuestNameText = childQuestUI.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI childQuestStatusText = childQuestUI.transform.Find("Status").GetComponent<TextMeshProUGUI>();

                childQuestNameText.text = child.questName;
                childQuestStatusText.text = child.status.ToString();
            }
        }
    }
}
