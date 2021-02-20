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

    public float stickTolerance = 0.5f;

    public bool isBuilding = false;
    private bool pauseBuilding = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (previewGameObject) previewGameObject.transform.Rotate(0, 90f * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuild();
        }

        if (Input.GetMouseButtonDown(0) && isBuilding)
        {
            if (previewScript.isSnapped && previewScript.isGood)
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
        previewGameObject = Instantiate(go, cam.transform.position + cam.transform.forward * 5, Quaternion.identity);
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
            if (previewScript.tagsSnapTo.Contains(hit.transform.gameObject.tag))
            {
                //PauseBuild(true);
                previewGameObject.transform.position = hit.transform.gameObject.transform.position;
                previewGameObject.transform.rotation = hit.transform.rotation;
                previewScript.isSnapped = true;
            }
            else
            {
                if (previewScript.isFoundation)
                {
                    float y = hit.point.y + (previewGameObject.transform.localScale.y * 0.5f);
                    Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
                    previewGameObject.transform.position = pos;
                    previewScript.isSnapped = true;
                }
                else
                {
                    previewGameObject.transform.position = cam.transform.position + cam.transform.forward * 5;
                    previewScript.isSnapped = false;
                }
            }
        }
        else
        {
            previewGameObject.transform.position = cam.transform.position + cam.transform.forward * 5;
            previewScript.isSnapped = false;
        }

        previewScript.ChangeColor();
    }
}
