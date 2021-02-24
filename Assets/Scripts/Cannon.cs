using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private List<Transform> enemies = new List<Transform>();

    public GameObject fireBall;

    public float speed = 0.1f;
    public Transform rotate;

    public Transform cannon;

    public Vector3 currentRotate;

    private Vector3 velocity = Vector3.zero;

    private bool isFire = false;

    public float fireRate = 5f;

    public float fireSpeed = 25f;

    private void Start()
    {
        currentRotate = transform.forward;
    }

    private void Update()
    {
        if (enemies.Count == 0) { return; }
        Vector3 position = enemies.OrderBy(e => (e.position - transform.position).magnitude).First().position;
        Vector3 v = (position - cannon.position).normalized;
        currentRotate = Vector3.SmoothDamp(currentRotate, v, ref velocity, speed);
        rotate.rotation = Quaternion.LookRotation(new Vector3(currentRotate.x, 0, currentRotate.z));
        cannon.rotation = Quaternion.LookRotation(currentRotate, rotate.right);
        if (!isFire)
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        isFire = true;
        Rigidbody rb = Instantiate(fireBall, cannon.position, cannon.rotation).GetComponent<Rigidbody>();
        rb.AddForce(cannon.forward * fireSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(fireRate);
        isFire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.transform);
        }
    }
}
