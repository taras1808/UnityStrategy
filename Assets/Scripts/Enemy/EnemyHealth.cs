using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth = 100;
    [SerializeField]
    private float Health = 100;
    [SerializeField]
    private float Damage = 25;

    private Slider SliderUI;

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("HealthSliderUI").GetComponent<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Ammunition)
        {
            Health -= Damage;
            SliderUI.value = Health / MaxHealth;

            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
