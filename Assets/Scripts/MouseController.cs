using UnityEngine;

/// <summary>
/// Handles mouse input to rotate an object with sensitivity and angle limits
/// </summary>
public class MouseController : MonoBehaviour
{
    /// <summary>
    /// Mouse movement sensitivity
    /// </summary>
    [SerializeField]
    private float sensitivity = 250f;

    /// <summary>
    /// Minimum and maximum rotation limits on the X axis (vertical)
    /// </summary>
    private float minX = -30f;
    private float maxX = 30f;

    /// <summary>
    /// Minimum and maximum rotation limits on the Y axis (horizontal)
    /// </summary>
    private float minY = -30f;
    private float maxY = 30f;

    /// <summary>
    /// Current rotation values
    /// </summary>
    float xRotation = 0f;
    float yRotation = 0f;

    /// <summary>
    /// Locks and hides the cursor, and initializes rotation values
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = transform.localRotation.eulerAngles;
        yRotation = rot.y;
        xRotation = rot.x;
    }

    /// <summary>
    /// Reads mouse input, applies rotation with sensitivity and clamps angles
    /// </summary>
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, minX, maxX);
        yRotation = Mathf.Clamp(yRotation, minY, maxY);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
