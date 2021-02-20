using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject foundation;

    public GameObject wall;

    public GameObject ceiling;


    public BuildSystem buildSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            buildSystem.NewBuild(foundation);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            buildSystem.NewBuild(wall);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            buildSystem.NewBuild(ceiling);
        }
    }
}
