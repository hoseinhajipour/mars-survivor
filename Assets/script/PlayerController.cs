using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 9.81f; // Gravity value
    public Transform gunTip;
    public GameObject bulletPrefab;
    public Inventory inventory;

    private CharacterController controller;
    private Vector3 velocity; // Player velocity for gravity

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        controller.Move(movement * moveSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

      
    }


}
