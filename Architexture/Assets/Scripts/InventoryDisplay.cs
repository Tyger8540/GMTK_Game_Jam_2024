using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryManager inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUM_COLS;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                TextMeshProUGUI[] children = itemsDisplayed[inventory.container[i]].GetComponentsInChildren<TextMeshProUGUI>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j].name == "Amount")
                    {
                        children[j].text = "x" + inventory.container[i].amount.ToString();
                    }
                }
            }
            else
            {
                var obj = Instantiate(inventory.container[i].blueprint.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                TextMeshProUGUI[] children = obj.GetComponentsInChildren<TextMeshProUGUI>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j].name == "Amount")
                    {
                        children[j].text = "x" + inventory.container[i].amount.ToString();
                    }
                }
                itemsDisplayed.Add(inventory.container[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].blueprint.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            TextMeshProUGUI[] children = obj.GetComponentsInChildren<TextMeshProUGUI>();
            for (int j = 0; j < children.Length; j++)
            {
                if (children[j].name == "Amount")
                {
                    children[j].text = "x" + inventory.container[i].amount.ToString();
                }
            }
            itemsDisplayed.Add(inventory.container[i], obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUM_COLS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUM_COLS)), 0f);
    }
}
