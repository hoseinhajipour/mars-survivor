using UnityEngine;
using TMPro;

public class FloatMessageManager : MonoBehaviour
{
    public static void ShowFloatMessage(string msg, Vector3 pos, float timeout)
    {
        // Load prefab from folder
        GameObject prefab = Resources.Load<GameObject>("util/float_message");

        if (prefab != null)
        {
            // Instantiate prefab
            GameObject floatMessageGO = Instantiate(prefab, pos, Quaternion.identity);

            // Set message text
            TextMeshProUGUI textMesh = floatMessageGO.GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh != null)
                textMesh.text = msg;

            // Destroy after timeout
            Destroy(floatMessageGO, timeout);
        }
        else
        {
            Debug.LogError("Float message prefab not found!");
        }
    }
}
