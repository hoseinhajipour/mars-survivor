using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public float smoothSpeed = 0.125f; // How quickly the camera moves to its target position
    public Vector3 offset; // The initial offset between the camera and the target

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position for the camera to move towards
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
