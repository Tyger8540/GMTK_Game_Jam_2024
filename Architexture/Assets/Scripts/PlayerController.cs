using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InventoryManager inventory;
    public CameraFollow camScript;

    public void AddItemToInventory(Item item)
    {
        inventory.AddBlueprint(item.blueprint, 1);
        camScript.PlaySound(camScript.checkpoint);
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            FindObjectOfType<InventoryDisplay>().RotateDisplay(true);  // rotate CW
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressed Q");
            FindObjectOfType<InventoryDisplay>().RotateDisplay(false);  // rotate CCW
        }
    }

    private void Start()
    {
        camScript = FindObjectOfType<CameraFollow>();
    }
}
