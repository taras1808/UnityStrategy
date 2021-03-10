using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public GameObject prefab;

    private MeshRenderer[] renderer;
    public Material goodMat;
    public Material badMat;

    public bool IsGood = false;

    private int collisions = 0;

    private void Awake()
    {
        renderer = GetComponentsInChildren<MeshRenderer>();
    }

    public void Place()
    {
        if (IsGood && collisions == 0)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            Material[] materials = new Material[renderer[i].materials.Length];
            for (int j = 0; j < materials.Length; j++)
            {
                materials[j] = IsGood && collisions == 0 ? goodMat : badMat;
            }
            renderer[i].materials = materials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.Cannon)
        {
            collisions++;
            ChangeColor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.Cannon)
        {
            collisions--;
            if (collisions == 0)
            {
                ChangeColor();
            }
        }
    }
}
