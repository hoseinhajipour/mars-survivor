using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class QuestData
{
    public string name;
    public QuestStatus status;
}

public enum QuestStatus
{
    Pending,
    Active,
    Complete,
    Fail
}

public enum QuestType
{
    Task,
    Incremental
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public Sprite icon;
    public string questName;
    public string description;
    public QuestStatus status;
    public QuestType type;
    public UnityEvent onComplete;
    public UnityEvent onFail;

    public Quest NextQuest;

    public void UpdateStatus(QuestStatus newStatus)
    {
        status = newStatus;

        // Save the updated status
        SaveQuestData();

        // You can invoke additional events or perform other actions based on the new status here
        switch (status)
        {
            case QuestStatus.Complete:
                Complete();
                break;
            case QuestStatus.Fail:
                Fail();
                break;
            default:
                break;
        }
    }

    void Complete()
    {
        if (onComplete != null)
            onComplete.Invoke();
    }

    void Fail()
    {
        if (onFail != null)
            onFail.Invoke();
    }

    void SaveQuestData()
    {
        // Save the quest data using PlayerPrefs
        QuestData questData = new QuestData();
        questData.name = questName;
        questData.status = status;
        string json = JsonUtility.ToJson(questData);
        PlayerPrefs.SetString("Quest_" + questName, json);
        PlayerPrefs.Save();
    }

    public void LoadQuestData()
    {
        // Load the quest data from PlayerPrefs
        string json = PlayerPrefs.GetString("Quest_" + questName);

        if (!string.IsNullOrEmpty(json))
        {
            QuestData questData = JsonUtility.FromJson<QuestData>(json);
            status = questData.status;

        }
    }
}
