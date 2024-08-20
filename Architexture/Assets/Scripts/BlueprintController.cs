using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueprintController : MonoBehaviour
{
    public Vector3 startingPosition;
    public float hoverSpeed;
    public float offset;
    public bool movingUp;
    public bool movingDown;

    public TextMeshProUGUI interactText;

    public bool inPlayerRange;
    public bool isPickedUp;
    public Blueprint blueprint;

    // Start is called before the first frame update
    void Start()
    {
        movingUp = true;
        movingDown = false;
        startingPosition = transform.position;
        inPlayerRange = false;
        isPickedUp = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.text = "Press <color=green><b>F</color></b> to pick up <color=blue><b>" + blueprint.name + "</color></b> blueprint";
            interactText.gameObject.SetActive(true);
            inPlayerRange = true;
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(0);
            if (collision.CompareTag("Player"))
            {
                Debug.Log(1);
                StartCoroutine(AcquireBlueprint());
                Destroy(gameObject);
            }
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isPickedUp)
        {
            interactText.gameObject.SetActive(false);
        }
        inPlayerRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPlayerRange)
        {
            isPickedUp = true;
            FindObjectOfType<DialogController>().AddBlueprint(blueprint.name);

            var item = GetComponent<Item>();
            if (item)  // if the item component exists for this blueprint (should always be true)
            {
                FindObjectOfType<PlayerController>().AddItemToInventory(item);
                Destroy(gameObject);
            }
        }

        if (movingUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startingPosition.y + 2*offset), Time.deltaTime * hoverSpeed);
            if (transform.position.y >= startingPosition.y + offset)
            {
                movingUp = false;
                movingDown = true;
            }
        }
        else if (movingDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startingPosition.y - 2*offset), Time.deltaTime * hoverSpeed);
            if (transform.position.y <= startingPosition.y - offset)
            {
                movingDown = false;
                movingUp = true;
            }
        }
    }
}
