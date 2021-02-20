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
    private bool pauseBuilding = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (previewGameObject) previewGameObject.transform.Rotate(0, 90f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuild();
        }

        if (Input.GetMouseButtonDown(0) && isBuilding)
        {
            if (previewScript.isSnapped)
            {
                StopBuild();
            }
        }

        if (isBuilding)
        {
            if (pauseBuilding)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >= stickTolerance)
                {
                    pauseBuilding = false;
                }
            }
            else
            {
                DoBuildRay();
            }
        }
    }

    public void NewBuild(GameObject go)
    {
        CancelBuild();
        previewGameObject = Instantiate(go, Vector3.zero, Quaternion.identity);
        previewScript = previewGameObject.GetComponent<Preview>();
        isBuilding = true;
        pauseBuilding = false;
    }

    private void CancelBuild()
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        pauseBuilding = false;
    }

    private void StopBuild()
    {
        previewScript.Place();
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
        pauseBuilding = false;
    }

    public void PauseBuild(bool value)
    {
        pauseBuilding = value;
    }

    private void DoBuildRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, ~layer))
        {
            previewGameObject.SetActive(true);
            float y = hit.point.y + (previewGameObject.transform.localScale.y * 0.5f);
            Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
            previewGameObject.transform.position = pos;
        }
        else
        {
            previewGameObject.SetActive(false);
        }
    }
}
