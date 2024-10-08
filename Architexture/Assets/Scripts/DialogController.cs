using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBlueprint(string blueprintType)
    {
        StartCoroutine(AcquireBlueprint(blueprintType));
    }

    IEnumerator AcquireBlueprint(string blueprintType)
    {
        interactText.gameObject.SetActive(true);
        interactText.text = "Acquired <color=blue><b>" + blueprintType + "</color></b> blueprint";
        yield return new WaitForSeconds(3f);
        interactText.gameObject.SetActive(false);
        // ADD BLUEPRINT TO INVENTORY
        yield return null;
    }
}
