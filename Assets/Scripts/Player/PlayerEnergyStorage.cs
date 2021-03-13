using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyStorage : MonoBehaviour
{
    [SerializeField]
    private float MaxEnergy = 100f;
    [SerializeField]
    private float Energy = 0f;
    [SerializeField]
    private float EnergyConsume = 25f;

    [SerializeField]
    private Text EnergyCount;

    private void Start()
    {
        EnergyCount.text = Energy.ToString();
    }

    public void GetEnergyFrom(EnergyGenerator energyGenerator)
    {
        if (Energy < MaxEnergy && energyGenerator.GetEnergy(EnergyConsume))
        {
            Energy += EnergyConsume;
            EnergyCount.text = Energy.ToString();
        }
    }

    public void PutEnergyTo(CannonEnergy cannonEnergy)
    {
        if (Energy > 0)
        {
            if (cannonEnergy.Charge(EnergyConsume))
            {
                Energy -= EnergyConsume;
                EnergyCount.text = Energy.ToString();
            }
        }
    }
}
