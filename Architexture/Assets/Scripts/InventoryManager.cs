using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static BlueprintController;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryManager : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public void AddBlueprint(Blueprint _blueprint, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].blueprint == _blueprint)
            {
                container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            container.Add(new InventorySlot(_blueprint, _amount));
        }
    }

    public void RemoveBlueprint(Blueprint _blueprint)
    {
        container[0].AddAmount(-1);
        if (container[0].amount == 0)
        {
            container.RemoveAt(0);
            FindObjectOfType<InventoryDisplay>().rotatedInventory = true;
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Blueprint blueprint;
    public int amount;
    public InventorySlot(Blueprint _blueprint, int _amount)
    {
        blueprint = _blueprint;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
