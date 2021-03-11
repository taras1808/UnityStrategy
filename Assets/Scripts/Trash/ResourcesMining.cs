using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMining : MonoBehaviour
{
    public LayerMask playerMask;

    public float Distance;
    public Selectable CurrentSelectable;
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Distance, ~playerMask))
        {
            Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>();
            
            if (selectable)
            {
                if(CurrentSelectable && CurrentSelectable != selectable)
                {
                    CurrentSelectable.Deselect();
                }
                CurrentSelectable = selectable;
                selectable.Select();
            } else
            {
                if (CurrentSelectable)
                {
                    CurrentSelectable.Deselect();
                    CurrentSelectable = null;
                }
            }
        } else
        {
            if (CurrentSelectable)
            {
                CurrentSelectable.Deselect();
                CurrentSelectable = null;
            }
        }
    }
}
