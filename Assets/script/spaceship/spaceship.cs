using UnityEngine;

public class spaceship : MonoBehaviour
{
    // Reference to the top part of the GameObject
    public GameObject topPart;

    // Flag to keep track if the player is inside the trigger zone
    private bool playerInside;

    private void Start()
    {
        // Ensure top part is initially visible
        topPart.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered trigger zone
            playerInside = true;
            // Hide the top part
            topPart.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited trigger zone
            playerInside = false;
            // Show the top part
            topPart.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If player is still inside the trigger zone, don't need to do anything
        // This is just for demonstration purposes, you can add additional logic here if needed
    }
}
