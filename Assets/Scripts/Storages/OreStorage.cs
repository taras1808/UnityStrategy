using UnityEngine;
using UnityEngine.UI;

public class OreStorage: MonoBehaviour, IOreStorage
{
    [SerializeField]
    private float MaxCapacity = 1000;
    [SerializeField]
    private float Quantity = 0;

    private Slider SliderUI;

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("OreSliderUI").GetComponent<Slider>();
        SliderUI.value = Quantity / MaxCapacity;
    }

    public bool HasSpace()
    {
        return Quantity < MaxCapacity;
    }

    public float Get(float amount)
    {
        if (Quantity >= amount)
        {
            Quantity -= amount;
            SliderUI.value = Quantity / MaxCapacity;
            return amount;
        }
        else
        {
            Quantity -= Quantity;
            SliderUI.value = Quantity / MaxCapacity;
            return Quantity;
        }
    }

    public float Put(float amount)
    {
        float freeSpace = MaxCapacity - Quantity;
        if (freeSpace >= amount)
        {
            Quantity += amount;
            SliderUI.value = Quantity / MaxCapacity;
            return amount;
        }
        else
        {
            Quantity += freeSpace;
            SliderUI.value = Quantity / MaxCapacity;
            return freeSpace;
        }
    }
}
