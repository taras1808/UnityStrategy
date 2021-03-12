using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CannonEnergy : MonoBehaviour
{
    [SerializeField]
    private float MaxEnergy = 100f;
    [SerializeField]
    private float Energy = 100f;
    [SerializeField]
    private float EnergyConsume = 25f;

    private Slider SliderUI;

    public bool IsActive => Energy > 0;

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("EnergySliderUI").GetComponent<Slider>();
    }

    public void Consume()
    {
        Energy -= EnergyConsume;
        SliderUI.value = Energy / MaxEnergy;
    }

    public void Charge(int count)
    {
        if (Energy < MaxEnergy)
        {
            Energy += count;
            SliderUI.value = Energy / MaxEnergy;
        }
    }
}
