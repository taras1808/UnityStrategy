using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGenerator : MonoBehaviour, ITransfer
{
    [SerializeField]
    private float MaxEnergy = 250;
    [SerializeField]
    private float Energy = 0;
    [SerializeField]
    private float EnergyPerSecond = 25;

    private List<IStorage> EnergyStorages = new List<IStorage>();

    private Slider SliderUI;

    private WaitForSeconds WaitForSeconds = new WaitForSeconds(1f);

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("EnergySliderUI").GetComponent<Slider>();
        SliderUI.value = Energy / MaxEnergy;
        StartCoroutine(GenerateEnergy());
    }

    private IEnumerator GenerateEnergy()
    {
        while (true)
        {
            yield return WaitForSeconds;
            if (MaxEnergy > Energy)
            {
                Energy += EnergyPerSecond;
                if (MaxEnergy < Energy)
                {
                    Energy = MaxEnergy;
                    break;
                }
                SliderUI.value = Energy / MaxEnergy;
                if (MaxEnergy == Energy)
                {
                    break;
                }
            } else
            {
                break;
            }
        }
    }

    public bool HasSpace()
    {
        return Energy < MaxEnergy;
    }

    public float Get(float amount)
    {
        if (Energy >= amount)
        {
            bool startGanerating = Energy == MaxEnergy;
            Energy -= amount;
            SliderUI.value = Energy / MaxEnergy;
            if (startGanerating)
            {
                StartCoroutine(GenerateEnergy());
            }
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

    private void Update()
    {
        if (Energy == 0)
        {
            return;
        }

        foreach (IStorage storage in EnergyStorages)
        {
            if (Energy == 0)
            {
                return;
            }

            if (storage.HasSpace())
            {
                bool startGanerating = Energy == MaxEnergy;
                Energy -= storage.Put(Energy);
                SliderUI.value = Energy / MaxEnergy;
                if (startGanerating)
                {
                    StartCoroutine(GenerateEnergy());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        bool isStorage = t.tag == Tags.Storage;
        while (t.parent != null)
        {
            t = t.parent;
            if (t.tag == Tags.Storage)
            {
                isStorage = true;
                break;
            }
        }
        if (isStorage)
        {
            EnergyStorages.Add(t.GetComponent<IStorage>());
        }
    }
}
