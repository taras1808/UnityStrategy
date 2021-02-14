using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CameraMovement : MonoBehaviour
{

#if UNITY_STANDALONE_OSX

    public enum _CGEventType
    {
        kCGEventLeftMouseDown = 1,
        kCGEventLeftMouseUp = 2,
        kCGEventRightMouseDown = 3,
        kCGEventRightMouseUp = 4,
        kCGEventMouseMoved = 5,
        kCGEventLeftMouseDragged = 6,
        kCGEventRightMouseDragged = 7,
        kCGEventKeyDown = 10,
        kCGEventKeyUp = 11,
        kCGEventFlagsChanged = 12,
        kCGEventScrollWheel = 22,
        kCGEventTabletPointer = 23,
        kCGEventTabletProximity = 24,
        kCGEventOtherMouseDown = 25,
        kCGEventOtherMouseUp = 26,
        kCGEventOtherMouseDragged = 27
        //        kCGEventTapDisabledByTimeout = 0xFFFFFFFE,
        //        kCGEventTapDisabledByUserInput = 0xFFFFFFFF
    };

    public enum _CGMouseButton
    {
        kCGMouseButtonLeft = 0,
        kCGMouseButtonRight = 1,
        kCGMouseButtonCenter = 2
    };

    public struct CGPoint
    {
        float x;
        float y;

        public CGPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return string.Format("[" + x + "," + y + "]");
        }
    }

    [DllImport("/System/Library/Frameworks/Quartz.framework/Versions/Current/Quartz")]
    private static extern uint CGEventCreateMouseEvent(int? source, _CGEventType mouseType, CGPoint mouseCursorPosition, _CGMouseButton mouseButton);

    public static void MouseEvent(_CGEventType eventType, CGPoint position)
    {
        CGEventCreateMouseEvent((int?)null, eventType, position, _CGMouseButton.kCGMouseButtonLeft);
    }

#endif

    public float mouseSensitivity = 10f;
    public float movementSpeed = 10f;

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;

    public Vector3 currentRotation;

    float yaw;
    float pitch;

    bool tabToggle = false;

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);


    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (currentRotation != null)
        {
            yaw = currentRotation.y;
            pitch = currentRotation.x;
        }
    }

    void Update()
    {
        tabToggle = Input.GetKeyDown(KeyCode.Tab) ? !tabToggle : tabToggle;
        if (tabToggle)
        {
            Cursor.visible = false;
        }
        else
        {
            if (!Cursor.visible)
            {
                //#if UNITY_STANDALONE_WIN
                //             SetCursorPos(Mathf.FloorToInt(Screen.width / 2), Mathf.FloorToInt(Screen.height / 2));
                //#endif
                //#if UNITY_STANDALONE_OSX
                //                MouseEvent(_CGEventType.kCGEventMouseMoved, new CGPoint(Mathf.Floor(Screen.width / 2), Mathf.Floor(Screen.height / 2)));
                //#endif

                StartCoroutine("Test");
            }
        }

        if (!Cursor.visible)
        {
            //if(Mathf.Abs(Input.GetAxis("Mouse X")) > 1 ||
            //    Mathf.Abs(Input.GetAxis("Mouse Y")) > 1)
            //{
            //    return;
            //}
           
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -90, 90);
            

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            transform.Translate(move * Time.deltaTime * movementSpeed * (Input.GetKey(KeyCode.LeftShift) ? 2 : 1), Space.World);
        }

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    IEnumerator Test()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForFixedUpdate();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
