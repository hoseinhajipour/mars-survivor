using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenConsumptionRate = 1f; // Oxygen consumption rate per second

    private bool isDead = false;

    private void Start()
    {
        currentOxygen = maxOxygen;
    }

    private void Update()
    {
        if (!isDead)
        {
            // Decrease oxygen level over time
            currentOxygen -= oxygenConsumptionRate * Time.deltaTime;

            // Check if oxygen is depleted
            if (currentOxygen <= 0)
            {
                Die();
            }
        }
    }

    public void RefillOxygen(float amount)
    {
        currentOxygen = Mathf.Min(maxOxygen, currentOxygen + amount);
    }

    private void Die()
    {
        // Handle player death (e.g., play death animation, show game over screen)
        isDead = true;
        Debug.Log("Player died due to lack of oxygen!");
        this.GetComponent<PlayerController>().enabled=false;
        // You can add more here, such as respawning the player or showing a game over screen.
    }
}
