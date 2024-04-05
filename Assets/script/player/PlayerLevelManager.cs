using UnityEngine;
using TMPro;

public class PlayerLevelManager : MonoBehaviour
{
    // Player level key for PlayerPrefs
    private const string playerLevelKey = "PlayerLevel";

    // Current player level
    public int currentPlayerLevel = 1;

    // Function to upgrade player level

    // Reference to the TMP Text element
    public TMP_Text playerLevelText;

    void Start()
    {
        LoadPlayerLevel();

    }
    public void UpgradePlayerLevel()
    {
        // Increase player level by 1
        currentPlayerLevel++;

        // Save the updated player level
        SavePlayerLevel();

        playerLevelText.text = currentPlayerLevel.ToString();
    }

    // Function to load player level
    public void LoadPlayerLevel()
    {
        // Check if the player level key exists in PlayerPrefs
        if (PlayerPrefs.HasKey(playerLevelKey))
        {
            // Load player level from PlayerPrefs
            currentPlayerLevel = PlayerPrefs.GetInt(playerLevelKey);
        }
        else
        {
            // If player level key doesn't exist, set default level to 1
            currentPlayerLevel = 1;
        }
        playerLevelText.text = currentPlayerLevel.ToString();
    }

    // Function to save player level
    private void SavePlayerLevel()
    {
        // Save current player level to PlayerPrefs
        PlayerPrefs.SetInt(playerLevelKey, currentPlayerLevel);
        PlayerPrefs.Save();
    }

    // Function to get current player level
    public int GetCurrentPlayerLevel()
    {
        return currentPlayerLevel;
    }

}
