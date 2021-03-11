using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float Distance = 10f;


    private Transform Player;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        if ((transform.position - Player.position).magnitude > Distance)
        {
            Vector3 move = (Player.position - transform.position).normalized;
            transform.position = transform.position + move * Time.fixedDeltaTime * Speed;
        }
    }
}
