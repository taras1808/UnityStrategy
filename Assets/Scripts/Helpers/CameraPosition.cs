using UnityEngine;

public static class CameraPosition
{
    public static Vector3 GetForward(int distance = 1)
    {
        return Camera.main.transform.position + Camera.main.transform.forward * distance;
    }
}
