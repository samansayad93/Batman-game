using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 200f;
    private float minX = -30f;
    private float maxX = 30f;
    private float minY = -30f;
    private float maxY = 30f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Vector3 rot = transform.localRotation.eulerAngles;

        yRotation = rot.y;
        xRotation = rot.x;
    }

    // Update is called once per frame
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
