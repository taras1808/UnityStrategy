using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyStorage : MonoBehaviour, IEnergyStorage
{
    [SerializeField]
    private float MaxEnergy = 1000;
    [SerializeField]
    private float Energy = 0;

    private int EnergyStorageIndex = 0;
    private List<IEnergyStorage> EnergyStorages = new List<IEnergyStorage>();

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
