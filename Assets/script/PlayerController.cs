using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f; // Rotation speed
    public float gravity = 9.81f; // Gravity value
    public Animator animator; // Reference to the Animator component
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

        // Rotate the player towards the movement direction
        RotatePlayer(movement);

        // Move the player
        controller.Move(movement * moveSpeed * Time.deltaTime);

        // Update Animator
        UpdateAnimator(movement.magnitude);

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotatePlayer(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimator(float moveMagnitude)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator reference not set.");
            return;
        }

        // Check if player is moving
        if (moveMagnitude > 0)
        {
            // Set "isWalking" parameter in the animator to true
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Set "isWalking" parameter in the animator to false
            animator.SetBool("isWalking", false);
        }
    }
}
