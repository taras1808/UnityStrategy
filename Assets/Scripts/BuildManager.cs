using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject cannon;

    public BuildSystem buildSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildSystem.NewBuild(cannon);
        }
    }
}
