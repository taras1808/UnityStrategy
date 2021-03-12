using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[DisallowMultipleComponent]
public class CannonTargets : MonoBehaviour
{
    private List<Transform> EnemiesTargets = new List<Transform>();

    public Transform GetTarget()
    {
        EnemiesTargets = EnemiesTargets.Where(e => e != null).ToList();
        if (EnemiesTargets.Count == 0) {
            return null;
        }
        Transform target = EnemiesTargets.OrderBy(e => (e.position - transform.position).magnitude).First();
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Enemy)
        {
            EnemiesTargets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.Enemy)
        {
            EnemiesTargets.Remove(other.transform);
        }
    }
}
