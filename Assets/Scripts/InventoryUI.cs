using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public bool showingInventory = false;
    public Inventory inventroy;

    private void Start()
    {
        inventroy.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.I)) return;
        
        showingInventory = !showingInventory;
        
        Cursor.lockState = showingInventory ? CursorLockMode.None : CursorLockMode.Locked;
        inventroy.gameObject.SetActive(showingInventory);
    }
}
