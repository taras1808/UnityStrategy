using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField]
    private float RaycastDistance = 3;

    [SerializeField]
    private LayerMask RaycastLayer = ~0;

    public GameObject showEnergyCannon;
    public GameObject showEnergyGenerator;

    void Update()
    {
        showEnergyCannon.SetActive(false);
        showEnergyGenerator.SetActive(false);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RaycastDistance, RaycastLayer, QueryTriggerInteraction.Ignore))
        {
            Transform t = hit.transform;

            while (t.parent != null)
            {
                t = t.parent;
            }
            
            EnergyGenerator generator = t.GetComponent<EnergyGenerator>();
            CannonEnergy energy = t.GetComponent<CannonEnergy>();

            if (generator)
            {
                showEnergyGenerator.SetActive(true);
            }
            else if (energy)
            {
                showEnergyCannon.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (generator)
                {
                    generator.GetEnergy(25);
                }
                else if (energy)
                {
                    energy.Charge(25);
                }
            }
    
        }
    }
}
