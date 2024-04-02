using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager

    // Add other game-related variables and properties here

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // Keep GameManager persistent between scenes
    }

    private void Start()
    {
        // Initialize game state
        // Load saved game data
    }

    // Method to start the game
    public void StartGame()
    {
        // Add any initialization logic for starting the game
        // For example, loading the first level or setting up player controls
        SceneManager.LoadScene("Level1"); // Load the first level scene
    }

    // Method to restart the game
    public void RestartGame()
    {
        // Reset game state
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    // Method to end the game
    public void EndGame()
    {
        // Add any cleanup logic for ending the game
        // For example, displaying game over UI, saving game data
        // SceneManager.LoadScene("MainMenu"); // Load the main menu scene
        Application.Quit(); // Quit the application (for standalone builds)
    }

    // Method to handle game over event
    public void GameOver()
    {
        // Handle game over logic
        // For example, displaying game over UI, saving game data
        // You may also want to implement a delay before restarting or returning to the main menu
        RestartGame(); // Restart the game
    }

    // Method to handle player victory event
    public void PlayerVictory()
    {
        // Handle player victory logic
        // For example, displaying victory UI, saving game data
        // You may also want to implement a delay before transitioning to the next level or returning to the main menu
        // SceneManager.LoadScene("NextLevel"); // Load the next level scene
        EndGame(); // End the game (for demonstration purposes)
    }
}
