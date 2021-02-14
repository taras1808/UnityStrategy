using System.Collections;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{

    private Vector3? pos;

    public GameObject obj;

    public LayerMask layer;

    private bool spawning = true;

    bool tabToggle = false;

    // Update is called once per frame
    void Update()
    {
        tabToggle = Input.GetKeyDown(KeyCode.Tab) ? !tabToggle : tabToggle;
        if (!tabToggle)
        {

            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, int.MaxValue, layer))
            {
                pos = hit.point;

                if (spawning)
                {
                    if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
                    {
                        Collider[] colliders = Physics.OverlapSphere(hit.point + Vector3.up * 0.7f, .5f, ~layer);
                        if (colliders.Length > 0)
                        {
                            spawning = false;
                            foreach (Collider collider in colliders)
                            {
                                Destroy(collider.gameObject.transform.parent.gameObject);
                            }
                            StartCoroutine("Spawn");
                        }
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        Collider[] colliders = Physics.OverlapSphere(hit.point + Vector3.up * 0.7f, .5f);
                        if (colliders.Length == 0)
                        {
                            spawning = false;
                            Instantiate(obj, hit.point, Quaternion.identity);
                            StartCoroutine("Spawn");
                        }
                    }
                }
            }
            else
            {
                pos = null;
            }

        }
        else
        {
            pos = null;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(.05f);
        spawning = true;
    }

    void OnDrawGizmos()
    {
        if (pos.HasValue)
        {
            //Gizmos.color = Color.magenta;
            //Gizmos.DrawCube(pos.Value, new Vector3(.5f, .5f, .5f));
            //Gizmos.color = Color.red;
            //Gizmos.DrawSphere(pos.Value + Vector3.up * 0.7f, .5f);
        }
    }
}
