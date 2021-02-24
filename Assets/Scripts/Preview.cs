using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public GameObject prefab;

    private MeshRenderer[] renderer;
    public Material goodMat;
    public Material badMat;

    private bool IsGood = false;

    private void Awake()
    {
        renderer = GetComponentsInChildren<MeshRenderer>();
        ChangeColor(true);
    }

    public void Place()
    {
        if (IsGood)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void ChangeColor(bool isGood)
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            Material[] materials = new Material[renderer[i].materials.Length];
            for (int j = 0; j < materials.Length; j++)
            {
                materials[j] = isGood ? goodMat : badMat;
            }
            renderer[i].materials = materials;
        }
        IsGood = isGood;
    }
}
