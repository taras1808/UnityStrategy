using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreMine : MonoBehaviour, ITransfer
{
    [SerializeField]
    private float MaxCapacity = 250;
    [SerializeField]
    private float Quantity = 0;
    [SerializeField]
    private float OrePerSecond = 25;

    private int OreStorageIndex = 0;
    private List<IOreStorage> OreStorages = new List<IOreStorage>();

    private Slider SliderUI;

    private WaitForSeconds WaitForSeconds = new WaitForSeconds(1f);

    private void Start()
    {
        SliderUI = transform.Find("Canvas").Find("OreSliderUI").GetComponent<Slider>();
        SliderUI.value = Quantity / MaxCapacity;
        StartCoroutine(GenerateOre());
    }

    private IEnumerator GenerateOre()
    {
        while (true)
        {
            yield return WaitForSeconds;
            if (MaxCapacity > Quantity)
            {
                Quantity += OrePerSecond;
                if (MaxCapacity < Quantity)
                {
                    Quantity = MaxCapacity;
                    break;
                }
                SliderUI.value = Quantity / MaxCapacity;
                if (MaxCapacity == Quantity)
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
        return Quantity < MaxCapacity;
    }

    public float Get(float amount)
    {
        if (Quantity >= amount)
        {
            bool startGanerating = Quantity == MaxCapacity;
            Quantity -= amount;
            SliderUI.value = Quantity / MaxCapacity;
            if (startGanerating)
            {
                StartCoroutine(GenerateOre());
            }
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

    private void Update()
    {
        if (Quantity == 0)
        {
            return;
        }

        if (OreStorages.Count > 0)
        {
            IStorage storage = OreStorages[OreStorageIndex];
            if (storage.HasSpace())
            {
                bool startGanerating = Quantity == MaxCapacity;
                Quantity -= storage.Put(Quantity);
                SliderUI.value = Quantity / MaxCapacity;
                if (startGanerating)
                {
                    StartCoroutine(GenerateOre());
                }
            }
            if (++OreStorageIndex >= OreStorages.Count)
            {
                OreStorageIndex = 0;
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
            new List<string>() { Tags.Storage }
        );
        if (tStorage)
        {
            IOreStorage storage = tStorage.GetComponent<IOreStorage>();
            if (storage != null)
            {
                if (!OreStorages.Contains(storage))
                {
                    OreStorages.Add(storage);
                }
            }
        }
    }
}
