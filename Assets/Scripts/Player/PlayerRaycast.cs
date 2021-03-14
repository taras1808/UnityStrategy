using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField]
    private float RaycastDistance = 3;

    [SerializeField]
    private LayerMask RaycastLayer = ~0;

    public GameObject showEnergyCannon;
    public GameObject showEnergyGenerator;

    public GameObject GetOreText;
    public GameObject PutOreText;

    [SerializeField]
    private PlayerEnergyStorage PlayerEnergyStorage;
    [SerializeField]
    private PlayerOreStorage PlayerOreStorage;

    void Update()
    {
        showEnergyCannon.SetActive(false);
        showEnergyGenerator.SetActive(false);
        GetOreText.SetActive(false);
        PutOreText.SetActive(false);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RaycastDistance, RaycastLayer, QueryTriggerInteraction.Ignore))
        {
            Transform t = hit.transform;

            while (t.parent != null)
            {
                t = t.parent;
            }

            IEnergyTransfer energyTransfer = t.GetComponent<IEnergyTransfer>();
            IOreTransfer oreTransfer = t.GetComponent<IOreTransfer>();

            if (energyTransfer != null) {
                showEnergyGenerator.SetActive(true);
                showEnergyCannon.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerEnergyStorage.GetEnergyFrom(energyTransfer);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PlayerEnergyStorage.PutEnergyTo(energyTransfer);
                }
            }
            else if (oreTransfer != null)
            {
                GetOreText.SetActive(true);
                PutOreText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerOreStorage.GetOreFrom(oreTransfer);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PlayerOreStorage.PutOreTo(oreTransfer);
                }
            }
        }
    }
}
