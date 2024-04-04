using UnityEngine;
using UnityEngine.Events;

public enum QuestStatus
{
    Active,
    Complete,
    Fail
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public Quest[] childQuests; // List of child quests
    public QuestStatus status; // Status of the quest

    public UnityEvent onComplete;
    public UnityEvent onFail;

    public void Complete()
    {
        // Invoke the onComplete event
        if (onComplete != null)
            onComplete.Invoke();
    }

    public void Fail()
    {
        // Invoke the onFail event
        if (onFail != null)
            onFail.Invoke();
    }
}
