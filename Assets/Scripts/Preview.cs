using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{

    public GameObject prefab;

    private MeshRenderer renderer;
    public Material goodMat;
    public Material badMat;

    private BuildSystem buildSystem;

    public bool isSnapped = false;
    public bool isFoundation = false;

    public List<string> tagsSnapTo = new List<string>();

    private void Start()
    {
        buildSystem = FindObjectOfType<BuildSystem>();
        renderer = GetComponent<MeshRenderer>();
        ChangeColor();
    }

    public void Place()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        if (isSnapped)
        {
            renderer.material = goodMat;
        }
        else
        {
            renderer.material = badMat;
        }

        if (isFoundation)
        {
            renderer.material = goodMat;
            isSnapped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagsSnapTo.Count; i++)
        {
            string currentTag = tagsSnapTo[i];
            if (other.tag == currentTag)
            {
                buildSystem.PauseBuild(true);
                transform.position = other.transform.position;
                isSnapped = true;
                ChangeColor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tagsSnapTo.Count; i++)
        {
            string currentTag = tagsSnapTo[i];
            if (other.tag == currentTag)
            {
                isSnapped = false;
                ChangeColor();
            }
        }
    }
}
