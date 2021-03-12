using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField]
    private float MaxEnergyStorage = 250;
    [SerializeField]
    private float EnergyStorage = 0;
    [SerializeField]
    private float EnergyPerSecond = 25;

    private Slider SliderUI;

    private WaitForSeconds WaitForSeconds = new WaitForSeconds(1f);

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("EnergySliderUI").GetComponent<Slider>();
        StartCoroutine(GenerateEnergy());
    }

    private IEnumerator GenerateEnergy()
    {
        while (true)
        {
            yield return WaitForSeconds;
            if (MaxEnergyStorage > EnergyStorage)
            {
                EnergyStorage += EnergyPerSecond;
                if (MaxEnergyStorage < EnergyStorage)
                {
                    EnergyStorage = MaxEnergyStorage;
                    break;
                }
                SliderUI.value = EnergyStorage / MaxEnergyStorage;
                if (MaxEnergyStorage == EnergyStorage)
                {
                    break;
                }
            } else
            {
                break;
            }
        }
    }

    public bool GetEnergy(float count)
    {
        bool startGanerating = EnergyStorage == MaxEnergyStorage;
        if (EnergyStorage >= count)
        {
            EnergyStorage -= count;
            SliderUI.value = EnergyStorage / MaxEnergyStorage;
            if (startGanerating)
            {
                StartCoroutine(GenerateEnergy());
            }
            return true;
        }
        return false;
    }
}
