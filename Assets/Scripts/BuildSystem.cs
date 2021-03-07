using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{

    public Camera cam;
    public LayerMask layer;
    private GameObject previewGameObject = null;
    private Preview previewScript = null;

    public float distance = 25f;

    public float stickTolerance = 1f;

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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, ~layer))
        {
            previewGameObject = Instantiate(go, hit.point, Quaternion.identity);
            previewScript = previewGameObject.GetComponent<Preview>();
            previewScript.ChangeColor(true);
        }
        else
        {
            previewGameObject = Instantiate(go, cam.transform.position + cam.transform.forward * 5, Quaternion.identity);
            previewScript = previewGameObject.GetComponent<Preview>();
            previewScript.ChangeColor(false);
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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, ~layer))
        {
            previewGameObject.transform.position = hit.point;
            previewScript.ChangeColor(true);
        }
        else
        {
            previewGameObject.transform.position = cam.transform.position + cam.transform.forward * 5;
            previewScript.ChangeColor(false);
        }
    }
}
