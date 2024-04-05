using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{


    // Call this function to clear all PlayerPrefs
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs have been cleared.");
    }
}
