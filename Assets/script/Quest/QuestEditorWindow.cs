using UnityEngine;
using UnityEditor;
using System.Linq;

public class QuestEditorWindow : EditorWindow
{
    private Quest quest;
    private Quest parentQuest;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditorWindow>("Quest Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Quest Editor", EditorStyles.boldLabel);

        quest = (Quest)EditorGUILayout.ObjectField("Quest", quest, typeof(Quest), false);

        if (quest != null)
        {
            EditorGUILayout.LabelField("Name", quest.questName);
            EditorGUILayout.LabelField("Description", quest.description);

            if (quest.childQuests != null && quest.childQuests.Length > 0)
            {
                EditorGUILayout.LabelField("Child Quests");
                foreach (var child in quest.childQuests)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(child, typeof(Quest), false);
                    if (GUILayout.Button("Remove", GUILayout.Width(80)))
                    {
                        RemoveChildQuest(child);
                        break;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add New Quest"))
            {
                AddNewQuest();
            }
        }
    }


    private void AddNewQuest()
    {
        Quest newQuest = CreateInstance<Quest>();
        newQuest.questName = "New Quest";
        newQuest.description = "Description";

        string uniqueName = quest.name + "_" + quest.childQuests.Length; // Generating a unique name using GUID


        AssetDatabase.CreateAsset(newQuest, "Assets/Quests/" + quest.name + "/" + uniqueName + ".asset");
        AssetDatabase.SaveAssets();
        if (quest != null)
        {
            // Add the new quest to the childQuests array of the parent quest
            if (quest.childQuests == null)
                quest.childQuests = new Quest[0];

            ArrayUtility.Add(ref quest.childQuests, newQuest);
            EditorUtility.SetDirty(quest);
        }


        AssetDatabase.Refresh();
    }




    private void RemoveChildQuest(Quest questToRemove)
    {
        if (parentQuest != null)
        {
            // Remove the quest from the parent's childQuests array
            if (parentQuest.childQuests != null)
            {
                parentQuest.childQuests = parentQuest.childQuests.Where(q => q != questToRemove).ToArray();
                EditorUtility.SetDirty(parentQuest);
            }
        }
        else
        {
            // Remove the quest from the childQuests array of the quest
            if (quest.childQuests != null)
            {
                quest.childQuests = quest.childQuests.Where(q => q != questToRemove).ToArray();
                EditorUtility.SetDirty(quest);
            }
            // Delete the asset if there's no parent quest
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(questToRemove));
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


}
