using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CannonEnergy))]
[RequireComponent(typeof(CannonTargets))]
[RequireComponent(typeof(CannonSight))]
public class CannonFire : MonoBehaviour
{
    [SerializeField]
    private GameObject FireBall;

    [SerializeField]
    private float FireRate = 3f;

    private Transform SpawnPosition;

    private CannonEnergy CannonEnergy;
    private CannonTargets CannonTargets;
    private CannonSight CannonSight;

    private bool IsFire = false;

    private void Start()
    {
        CannonEnergy = GetComponent<CannonEnergy>();
        CannonTargets = GetComponent<CannonTargets>();
        CannonSight = GetComponent<CannonSight>();
        SpawnPosition = transform.Find("body").Find("cannon");
    }

    private void Update()
    {
        if (!IsFire && CannonEnergy.IsActive && CannonSight.IsReady)
        {
            Transform target = CannonTargets.GetTarget();
            if (!target)
            {
                return;
            }            
            StartCoroutine(Fire(target));
        }
    }

    private IEnumerator Fire(Transform target)
    {
        CannonEnergy.Consume();
        IsFire = true;
        FireBall fireBall = Instantiate(FireBall, SpawnPosition.position, SpawnPosition.rotation).GetComponent<FireBall>();
        fireBall.Target = target;
        yield return new WaitForSeconds(FireRate);
        IsFire = false;
    }

    public IEnumerator Reload()
    {
        IsFire = true;
        yield return new WaitForSeconds(FireRate);
        IsFire = false;
    }
}
