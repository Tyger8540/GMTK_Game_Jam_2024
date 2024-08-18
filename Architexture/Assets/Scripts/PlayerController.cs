using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InventoryManager inventory;

    public void AddItemToInventory(Item item)
    {
        inventory.AddBlueprint(item.blueprint, 1);
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}
