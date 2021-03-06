﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float disposeTime = 10f;

    private void Awake()
    {
        StartCoroutine(Dispose());
    }

    private IEnumerator Dispose()
    {
        yield return new WaitForSeconds(disposeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
