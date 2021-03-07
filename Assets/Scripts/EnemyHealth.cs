using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;


    public float damage = 5;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            health -= damage;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }
}
