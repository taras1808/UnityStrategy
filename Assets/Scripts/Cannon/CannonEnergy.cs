using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CannonEnergy : MonoBehaviour, IEnergyStorage
{
    [SerializeField]
    private float MaxEnergy = 100f;
    [SerializeField]
    private float Energy = 100f;
    [SerializeField]
    private float EnergyConsume = 25f;

    private Slider SliderUI;

    public bool IsActive => Energy > 0;

    private CannonFire CannonFire; 

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("EnergySliderUI").GetComponent<Slider>();
        SliderUI.value = Energy / MaxEnergy;
        CannonFire = GetComponent<CannonFire>();
    }

    public void Consume()
    {
        Get(EnergyConsume);
    }

    public bool HasSpace()
    {
        return Energy < MaxEnergy;
    }

    public float Get(float amount)
    {
        if (Energy >= amount)
        {
            Energy -= amount;
            SliderUI.value = Energy / MaxEnergy;
            return amount;
        }
        else
        {
            Energy -= Energy;
            SliderUI.value = Energy / MaxEnergy;
            return Energy;
        }
    }

    public float Put(float amount)
    {
        if (Energy == 0)
        {
            StartCoroutine(CannonFire.Reload());
        }

        float freeSpace = MaxEnergy - Energy;
        if (freeSpace >= amount)
        {
            Energy += amount;
            SliderUI.value = Energy / MaxEnergy;
            return amount;
        }
        else
        {
            Energy += freeSpace;
            SliderUI.value = Energy / MaxEnergy;
            return freeSpace;
        }
    }
}
