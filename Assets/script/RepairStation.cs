using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public float repairRate = 1f; // The rate at which the repair is performed
    public string repairTag = "Player"; // The tag of the GameObject that can repair at this station
    public GameObject repairEffectPrefab; // Particle effect to show repair activity

    private bool isRepairing; // Flag to check if repair is in progress
    private float repairProgress; // Progress of repair operation
    private GameObject player; // Reference to the player GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(repairTag))
        {
            player = other.gameObject;
            StartRepair();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(repairTag))
        {
            StopRepair();
        }
    }

    public void StartRepair()
    {
        isRepairing = true;
        repairProgress = 0f;
    }

    private void StopRepair()
    {
        isRepairing = false;
        repairProgress = 0f;
        player = null;
    }

    private void Update()
    {
        if (isRepairing)
        {
            repairProgress += repairRate * Time.deltaTime;

            // Perform repair action
            // You can implement your repair logic here
            // For example, increase the health of the spaceship, etc.

            // Instantiate repair effect
            if (repairEffectPrefab != null)
            {
                Instantiate(repairEffectPrefab, transform.position, Quaternion.identity);
            }

            if (repairProgress >= 100f)
            {
                StopRepair();
            }
        }
    }
}
