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

    public void GetEnergyFrom(IEnergyTransfer energyGenerator)
    {
        if (Energy < MaxEnergy)
        {
            Energy += energyGenerator.Get(EnergyConsume);
            EnergyCount.text = Energy.ToString();
        }
    }

    public void PutEnergyTo(IEnergyTransfer cannonEnergy)
    {
        if (Energy > 0)
        {
            if (Energy >= EnergyConsume)
            {
                Energy -= cannonEnergy.Put(EnergyConsume);
                EnergyCount.text = Energy.ToString();
            }
            else
            {
                Energy -= cannonEnergy.Put(Energy);
                EnergyCount.text = Energy.ToString();
            }
        }
    }
}
