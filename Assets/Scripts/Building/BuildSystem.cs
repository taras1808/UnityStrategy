using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField]
    private float Distance = 25f;
    [SerializeField]
    private LayerMask BuildLayer;

    private GameObject PreviewGameObject = null;
    private PreviewScript PreviewScript = null;

    private bool IsBuilding = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            if (PreviewGameObject) PreviewGameObject.transform.Rotate(0, 90f * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (PreviewGameObject) PreviewGameObject.transform.Rotate(0, -90f * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuild();
        }

        if (Input.GetMouseButtonDown(0) && IsBuilding)
        {
            StopBuild();
        }

        if (IsBuilding)
        {
            DoBuildRay();
        }
    }

    public void NewBuild(GameObject CannonPreview)
    {
        CancelBuild();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Distance, BuildLayer, QueryTriggerInteraction.Ignore))
        {
            PreviewGameObject = Instantiate(CannonPreview, hit.point, Quaternion.identity);
            PreviewScript = PreviewGameObject.GetComponent<PreviewScript>();
            PreviewScript.IsGood = true;
            PreviewScript.ChangeColor();
        }
        else
        {
            PreviewGameObject = Instantiate(CannonPreview, CameraPosition.GetForward(5), Quaternion.identity);
            PreviewScript = PreviewGameObject.GetComponent<PreviewScript>();
            PreviewScript.IsGood = false;
            PreviewScript.ChangeColor();
        }
        IsBuilding = true;
    }

    private void CancelBuild()
    {
        Destroy(PreviewGameObject);
        PreviewGameObject = null;
        PreviewScript = null;
        IsBuilding = false;
    }

    private void StopBuild()
    {
        PreviewScript.Place();
        PreviewGameObject = null;
        PreviewScript = null;
        IsBuilding = false;
    }

    private void DoBuildRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Distance, BuildLayer, QueryTriggerInteraction.Ignore))
        {
            PreviewGameObject.transform.position = hit.point;
            PreviewScript.IsGood = true;
            PreviewScript.ChangeColor();
        }
        else
        {
            PreviewGameObject.transform.position = CameraPosition.GetForward(5);
            PreviewScript.IsGood = false;
            PreviewScript.ChangeColor();
        }
    }
}
