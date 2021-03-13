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

            ITransfer transfer = t.GetComponent<ITransfer>();

            if (transfer != null) {
                showEnergyGenerator.SetActive(true);
                showEnergyCannon.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    transfer.Get(25);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    transfer.Put(25);
                }
            }
        }
    }
}
