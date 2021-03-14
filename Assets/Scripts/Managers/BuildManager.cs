using UnityEngine;

[RequireComponent(typeof(BuildSystem))]
public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CannonPreview;

    private BuildSystem BuildSystem;

    private void Start()
    {
        BuildSystem = GetComponent<BuildSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildSystem.NewBuild(CannonPreview);
        }
    }

    public void StartBuilding()
    {
        BuildSystem.NewBuild(CannonPreview);
    }
}
