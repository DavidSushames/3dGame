using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController2 : MonoBehaviour
{
    public Transform player;

    public float mouseSensitivity = 100f;

    public float distanceFromPlayer = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;

    public float zoomSpeed = 10f;
    public float smoothSpeed = 10f;

    public float heightOffset = 2f;

    public float minYAngle = -40f;
    public float maxYAngle = 80f;

    public InputAction mouseLook;
    public InputAction scroll;

    float xRotation = 0f;
    float yRotation = 0f;

    float targetDistance;

    void OnEnable()
    {
        mouseLook.Enable();
        scroll.Enable();
    }

    void OnDisable()
    {
        mouseLook.Disable();
        scroll.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        targetDistance = distanceFromPlayer;
    }

    void LateUpdate()
    {
        // Mouse input
        Vector2 mouseInput = mouseLook.ReadValue<Vector2>();

        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Scroll zoom
        float scrollInput = scroll.ReadValue<Vector2>().y;

        targetDistance -= scrollInput * zoomSpeed * Time.deltaTime;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        // Smooth zoom
        distanceFromPlayer = Mathf.Lerp(distanceFromPlayer, targetDistance, smoothSpeed * Time.deltaTime);

        // Camera direction
        Vector3 direction = transform.rotation * new Vector3(0f, 0f, -distanceFromPlayer);

        Vector3 origin = player.position + Vector3.up * heightOffset;

        RaycastHit hit;

        // Wall collision
        if (Physics.Raycast(origin, direction.normalized, out hit, distanceFromPlayer))
        {
            transform.position = origin + direction.normalized * hit.distance;
        }
        else
        {
            transform.position = origin + direction;
        }
    }
}