using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGenerator : MonoBehaviour, IEnergyTransfer
{
    [SerializeField]
    private float MaxEnergy = 250;
    [SerializeField]
    private float Energy = 0;
    [SerializeField]
    private float EnergyPerSecond = 25;

    private int EnergyStorageIndex = 0;
    private List<IEnergyStorage> EnergyStorages = new List<IEnergyStorage>();

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

        if (EnergyStorages.Count > 0)
        {
            IStorage storage = EnergyStorages[EnergyStorageIndex];
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
            if (++EnergyStorageIndex >= EnergyStorages.Count)
            {
                EnergyStorageIndex = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        Transform tStorage = SearchSystem.FindUpByTags(
            other.transform,
            new List<string>() { Tags.Storage, Tags.Cannon }
        );
        if (tStorage)
        {
            IEnergyStorage storage = tStorage.GetComponent<IEnergyStorage>();
            if (storage != null)
            {
                if (!EnergyStorages.Contains(storage))
                {
                    EnergyStorages.Add(storage);
                }
            }
        }
    }
}
