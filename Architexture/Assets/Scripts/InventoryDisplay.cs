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
    public Vector3 leftSlot;
    public Vector3 centerSlot;
    public Vector3 rightSlot;
    public bool rotatedInventory = false;
    public GameObject selection;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        selection.SetActive(false);
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void RotateDisplay(bool isCW)  // if CW then true should be passed in, otherwise false (Pressing E would be CW and pressing Q would be CCW)
    {
        if (isCW)
        {
            InventorySlot tempSlot = inventory.container[0];  // temporarily store the first inventory item, which will move to the end
            for (int i = 0; i < inventory.container.Count; i++)
            {
                if (i < inventory.container.Count - 1)
                {
                    inventory.container[i] = inventory.container[i + 1];
                }
                else
                {
                    inventory.container[i] = tempSlot;
                }

            }
        }
        else
        {
            InventorySlot tempSlot = inventory.container[inventory.container.Count - 1];  // temporarily store the last inventory item, which will move to the beginning
            for (int i = inventory.container.Count - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    inventory.container[i] = inventory.container[i - 1];
                }
                else
                {
                    inventory.container[i] = tempSlot;
                }
            }
        }
        rotatedInventory = true;
    }

    public void UpdateDisplay()
    {
        if (inventory.container.Count > 0 && !selection.activeSelf)
        {
            selection.SetActive(true);
        }
        else if (inventory.container.Count == 0)
        {
            selection.SetActive(false);
        }

        if (rotatedInventory)
        {
            itemsDisplayed.Clear();
            if (FindObjectOfType<UseBlueprint>().inBlueprintMode)
            {
                FindObjectOfType<UseBlueprint>().HideOutlines();
                FindObjectOfType<UseBlueprint>().ShowCurrentOutline();
            }
            rotatedInventory = false;
            // Get rid of prefabs since they get re-instantiated
            BlueprintDisplay[] bp = FindObjectsOfType<BlueprintDisplay>();
            foreach (BlueprintDisplay b in bp)
            {
                Destroy(b.gameObject);
            }
            Debug.Log("ItemsDisplayed count: " + itemsDisplayed.Count);
        }
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                //Debug.Log(itemsDisplayed[inventory.container[i]].gameObject.name);
                if (itemsDisplayed[inventory.container[i]] != null) {  // had annoying missing reference exception when not used
                    TextMeshProUGUI[] children = itemsDisplayed[inventory.container[i]].GetComponentsInChildren<TextMeshProUGUI>();
                    for (int j = 0; j < children.Length; j++)
                    {
                        if (children[j].name == "Amount")
                        {
                            children[j].text = "x" + inventory.container[i].amount.ToString();
                        }
                    }
                }
            }
            else if (itemsDisplayed.Count <= 3 && (i == 0 || i == 1 || i == inventory.container.Count - 1))  // only displays an item if it is one of the first 3 to be displayed
            {
                if (itemsDisplayed.Count == 3)  // ^ gross else if statement above if u wanna try to fix it, but does work
                {
                    BlueprintDisplay[] bp = FindObjectsOfType<BlueprintDisplay>();
                    foreach (BlueprintDisplay b in bp)
                    {
                        Debug.Log("b.name: " + b.nameText.text);
                        Debug.Log("blueprint.name: " + inventory.container[i - 1].blueprint.name);
                        if (b.nameText.text == inventory.container[i - 1].blueprint.name)
                        {
                            Destroy(b.gameObject);
                        }
                    }
                }
                var obj = Instantiate(inventory.container[i].blueprint.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                TextMeshProUGUI[] children = obj.GetComponentsInChildren<TextMeshProUGUI>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j] != null)
                    {
                        if (children[j].name == "Amount")
                        {
                            children[j].text = "x" + inventory.container[i].amount.ToString();
                        }
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
            Debug.Log("Test 1");
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
        Debug.Log("i: " + i);
        if (i == 0)  // NEED TO MAKE CASE WHERE U ONLY HAVE 2 BLUEPRINTS!!!
        {
            return centerSlot;
        }
        else if (i == 1)
        {
            return rightSlot;
        }
        else if (i == inventory.container.Count - 1)
        {
            return leftSlot;
        }
        else
        {
            return new Vector3(600f, 0f, 0f);
        }
        //return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUM_COLS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUM_COLS)), 0f);
    }
}
