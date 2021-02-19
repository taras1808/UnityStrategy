using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    void Start()
    {
        transform.localScale = Random.Range(0.5f, 4) * Vector3.one;
    }

    private int Count = 5;
    public Material selectedMaterial;
    public Material defaultMaterial;
    public void Select()
    {
        GetComponent<Renderer>().material = selectedMaterial;
        if (Input.GetKeyDown("e"))
        {
            Collect.Money += Count * transform.localScale.x;

            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(WaitBeforeShow());
        }
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = defaultMaterial;
    }

    private IEnumerator WaitBeforeShow()
    {
        yield return new WaitForSeconds(5);

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

}
