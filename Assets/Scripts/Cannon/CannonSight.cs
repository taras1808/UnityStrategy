using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CannonEnergy))]
[RequireComponent(typeof(CannonTargets))]
public class CannonSight : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 0.1f;

    private Vector3 CurrentVector = Vector3.forward;
    private Vector3 TargetVector;

    private CannonEnergy CannonEnergy;
    private CannonTargets CannonTargets;

    private Transform CannonBase;
    private Transform Cannon;

    public bool IsReady => TargetVector != null && Mathf.Abs((TargetVector - CurrentVector).magnitude) < .1f;

    private void Start()
    {
        CannonEnergy = GetComponent<CannonEnergy>();
        CannonTargets = GetComponent<CannonTargets>();
        CannonBase = transform.Find("body");
        Cannon = CannonBase.Find("cannon");
    }

    private void Update()
    {
        if (CannonEnergy.IsActive)
        {
            Transform target = CannonTargets.GetTarget();
            if (!target) return;
            Vector3 targetPosition = target.position;
            TargetVector = (targetPosition - Cannon.position).normalized;
            Vector3 velocity = Vector3.zero;
            CurrentVector = Vector3.SmoothDamp(CurrentVector, TargetVector, ref velocity, MoveSpeed);
            CannonBase.rotation = Quaternion.LookRotation(new Vector3(CurrentVector.x, 0, CurrentVector.z));
            Cannon.rotation = Quaternion.LookRotation(CurrentVector);
        }
    }
}
