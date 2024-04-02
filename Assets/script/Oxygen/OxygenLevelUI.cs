using UnityEngine;
using UnityEngine.UI;

public class OxygenLevelUI : MonoBehaviour
{
    public PlayerOxygen playerOxygen;
    public Image oxygenFillImage;

    private void Update()
    {
        // Update oxygen level UI
        oxygenFillImage.fillAmount = playerOxygen.currentOxygen / playerOxygen.maxOxygen;
    }
}
