using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Speed of movement

    [Header("Mouse Look Settings")]
    public float lookSensitivity = 2f; // Speed of mouse look

    private float yaw = 0f;   // Horizontal rotation
    private float pitch = 0f; // Vertical rotation

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Mouse Look ---
        // Get mouse input. These axes come from the old Input Manager.
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        // Clamp the pitch so the camera doesn’t flip over
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply the rotation to the camera.
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        // --- WASD Movement ---
        // Get movement input. Horizontal for A/D and Vertical for W/S.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Build movement directions relative to the current orientation.
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Optionally, zero-out any vertical components if you want to limit movement to the horizontal plane.
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Combine the directions with input
        Vector3 moveDirection = (forward * moveVertical + right * moveHorizontal);

        // Move the camera
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
