using UnityEngine;

public class BuildingObject : MonoBehaviour, IBuildingObject
{
    public void Delete()
    {
        Destroy(gameObject);
    }
}
