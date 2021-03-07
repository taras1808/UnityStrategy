using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
