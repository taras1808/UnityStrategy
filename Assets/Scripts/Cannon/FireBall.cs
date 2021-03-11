using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10f;

    public Transform Target;

    private void Update()
    {
        if (Target)
        {
            Vector3 move = (Target.position - transform.position).normalized;
            transform.position += move * Speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Enemy)
        {
            Destroy(gameObject);
        }
    }
}
