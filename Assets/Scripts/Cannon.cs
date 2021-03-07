using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public float fireRate = 3f;

    public float fireSpeed = 100f;

    public float energy = 100f;
    public float energyConsume = 25f;
    public float maxEnergy = 100f;

    public GameObject energyBarUI;
    public Slider slider;

    private void Start()
    {
        currentRotate = transform.forward;
        energy = maxEnergy;
        slider.value = CalculateEnergy();
    }

    private void Update()
    {


        if (energy > 0)
        {
            enemies = enemies.Where(e => e != null).ToList();
            if (enemies.Count == 0) { return; }
            Transform enemy = enemies.OrderBy(e => (e.position - transform.position).magnitude).First();
            Vector3 position = enemy.position;
            Vector3 v = (position - cannon.position).normalized;
            currentRotate = Vector3.SmoothDamp(currentRotate, v, ref velocity, speed);
            rotate.rotation = Quaternion.LookRotation(new Vector3(currentRotate.x, 0, currentRotate.z));
            cannon.rotation = Quaternion.LookRotation(currentRotate);
            if (!isFire && Mathf.Abs((v - currentRotate).magnitude) < .1f)
            {
                StartCoroutine(Fire(enemy));
            }
        }
    }

    private IEnumerator Fire(Transform enemy)
    {

        //if (energy < maxEnergy)
        //{
        //    energyBarUI.SetActive(true);
        //}

        isFire = true;
        energy -= energyConsume;
        slider.value = CalculateEnergy();
        Boom boom = Instantiate(fireBall, cannon.position, cannon.rotation).GetComponent<Boom>();
        boom.enemy = enemy;
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

    float CalculateEnergy()
    {
        return energy / maxEnergy;
    }
}
