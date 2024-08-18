using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintDisplay : MonoBehaviour
{
    public Blueprint blueprint;

    public TextMeshProUGUI nameText;

    public Image icon;

    public TextMeshProUGUI amountText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = blueprint.name;
        icon.sprite = blueprint.icon;
        //amountText.text = "x" + blueprint.amount.ToString();
    }
}
