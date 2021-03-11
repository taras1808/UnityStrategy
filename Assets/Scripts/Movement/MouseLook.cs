using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float MouseSensitivity = 500f;

    public Transform PlayerBody;

    float XRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
