using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField]
    private float RaycastDistance = 3;

    [SerializeField]
    private LayerMask RaycastLayer = ~0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RaycastDistance, RaycastLayer, QueryTriggerInteraction.Ignore))
        {
            Transform t = hit.transform;

            bool isMain = false;
            while (t.parent != null)
            {
                t = t.parent;
                if (t.parent == null)
                {
                    isMain = true;
                    break;
                }
            }
            if (isMain)
            {
                EnergyGenerator generator = t.GetComponent<EnergyGenerator>();
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (generator)
                    {
                        generator.GetEnergy(25);
                    }
                }
            }
        }
    }
}
