using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    private void Awake()
    {
        transform.forward = Camera.main.transform.forward;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
