using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool ShowingInventory = false;
    private InventoryUI InventoryUI;

    private void Start()
    {
        InventoryUI = GameObject.Find("Canvas").transform.Find("InventoryUI").GetComponent<InventoryUI>();
        InventoryUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.I)) return;

        ShowingInventory = !ShowingInventory;
        
        Cursor.lockState = ShowingInventory ? CursorLockMode.None : CursorLockMode.Locked;
        InventoryUI.gameObject.SetActive(ShowingInventory);
    }
}
