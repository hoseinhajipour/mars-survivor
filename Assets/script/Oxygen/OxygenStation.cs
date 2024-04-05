using UnityEngine;

public class OxygenStation : MonoBehaviour
{
    public float refillAmount = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();
            if (playerOxygen != null)
            {
                // Refill player's oxygen
                playerOxygen.RefillOxygen(refillAmount);
              //  Debug.Log("Player's oxygen refilled!");
                // Disable or destroy the oxygen station after use if needed
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();
            if (playerOxygen != null)
            {
                // Refill player's oxygen
                playerOxygen.RefillOxygen(refillAmount);
                Debug.Log("Player's oxygen refilled!");
                // Disable or destroy the oxygen station after use if needed
            }
        }
    }

}
