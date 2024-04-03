using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel;

    // This function will be called when the button is clicked
    public void TogglePanel()
    {
        // Toggle the active state of the panel
        panel.SetActive(!panel.activeSelf);
    }
}
