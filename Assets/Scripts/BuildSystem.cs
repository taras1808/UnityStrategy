using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public LayerMask layer;
    private GameObject previewGameObject = null;
    private Preview previewScript = null;

    public float distance = 25f;

    public bool isBuilding = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            if (previewGameObject) previewGameObject.transform.Rotate(0, 90f * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (previewGameObject) previewGameObject.transform.Rotate(0, -90f * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuild();
        }

        if (Input.GetMouseButtonDown(0) && isBuilding)
        {
            StopBuild();
        }

        if (isBuilding)
        {
            DoBuildRay();
        }
    }

    public void NewBuild(GameObject go)
    {
        CancelBuild();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layer, QueryTriggerInteraction.Ignore))
        {
            previewGameObject = Instantiate(go, hit.point, Quaternion.identity);
            previewScript = previewGameObject.GetComponent<Preview>();
            previewScript.IsGood = true;
            previewScript.ChangeColor();
        }
        else
        {
            previewGameObject = Instantiate(go, Camera.main.transform.position + Camera.main.transform.forward * 5, Quaternion.identity);
            previewScript = previewGameObject.GetComponent<Preview>();
            previewScript.IsGood = false;
            previewScript.ChangeColor();
        }
        isBuilding = true;
    }

    private void CancelBuild()
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }

    private void StopBuild()
    {
        previewScript.Place();
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }

    private void DoBuildRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layer, QueryTriggerInteraction.Ignore))
        {
            previewGameObject.transform.position = hit.point;
            previewScript.IsGood = true;
            previewScript.ChangeColor();
        }
        else
        {
            previewGameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
            previewScript.IsGood = false;
            previewScript.ChangeColor();
        }
    }
}
