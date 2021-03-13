using UnityEngine;
using UnityEngine.UI;

public class PlayerOreStorage : MonoBehaviour
{
    [SerializeField]
    private float MaxCapacity = 100f;
    [SerializeField]
    private float Quantity = 0f;
    [SerializeField]
    private float OreConsume = 25f;

    [SerializeField]
    private Text OreCount;

    private void Start()
    {
        OreCount.text = Quantity.ToString();
    }

    public void GetOreFrom(IOreTransfer energyGenerator)
    {
        if (Quantity < MaxCapacity)
        {
            Quantity += energyGenerator.Get(OreConsume);
            OreCount.text = Quantity.ToString();
        }
    }

    public void PutOreTo(IOreTransfer cannonEnergy)
    {
        if (Quantity > 0)
        {
            if (Quantity >= OreConsume)
            {
                Quantity -= cannonEnergy.Put(OreConsume);
                OreCount.text = Quantity.ToString();
            }
            else
            {
                Quantity -= cannonEnergy.Put(Quantity);
                OreCount.text = Quantity.ToString();
            }
        }
    }
}
