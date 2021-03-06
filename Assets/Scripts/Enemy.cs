using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public float speed = 5f;

    public float distance = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if ((transform.position - player.position).magnitude > distance)
        {
            Vector3 move = (player.position - transform.position).normalized;
            rb.MovePosition(transform.position + move * Time.fixedDeltaTime * speed);
        }
    }
}
