using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyStorage : MonoBehaviour, IStorage
{
    [SerializeField]
    private float MaxEnergy = 1000;
    [SerializeField]
    private float Energy = 0;

    private int EnergyStorageIndex = 0;
    private List<IStorage> EnergyStorages = new List<IStorage>();

    private Slider SliderUI;

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("EnergySliderUI").GetComponent<Slider>();
        SliderUI.value = Energy / MaxEnergy;
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
                Energy -= storage.Put(Energy);
                SliderUI.value = Energy / MaxEnergy;
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
        Transform tStorage = SearchSystem.FindUpByTag(other.transform, Tags.Cannon);
        if (tStorage)
        {
            IStorage storage = tStorage.GetComponent<IStorage>();
            if (!EnergyStorages.Contains(storage))
            {
                EnergyStorages.Add(storage);
            }
        }
    }
}
