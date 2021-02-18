using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        if (Input.GetKeyDown("e"))
        {
            Collect.Money += 5;
            Destroy(gameObject);
        }
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

}
