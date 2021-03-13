using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CannonEnergy : MonoBehaviour, IStorage
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
        SliderUI.value = Energy / MaxEnergy;
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
