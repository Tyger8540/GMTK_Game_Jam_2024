using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class UseBlueprint : MonoBehaviour
{
    public InventoryManager inventory;
    public float playerReach;
    public PlayerMovement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerMovementScript.IsGrounded())
        {
            if (inventory.container.Count > 0)
            {
                Blueprint blueprint = inventory.container[0].blueprint;
                var obj = Instantiate(blueprint.placedObjectPrefab, Vector3.zero, Quaternion.identity);
                obj.transform.position = new Vector3(transform.position.x + playerMovementScript.playerDirection * blueprint.placeDistance.x, transform.position.y + blueprint.placeDistance.y, 0f);
                if (obj.GetComponent<PlaceableController>().orientation != PlaceableController.Orientation.Up && obj.GetComponent<PlaceableController>().orientation != PlaceableController.Orientation.Down)
                {
                    if (playerMovementScript.playerDirection < 0)
                    {
                        obj.transform.Rotate(new Vector3(0f, 180f, 0f));
                        obj.GetComponent<PlaceableController>().orientation = PlaceableController.Orientation.Left;
                    }
                    else
                    {
                        obj.GetComponent<PlaceableController>().orientation = PlaceableController.Orientation.Right;
                    }
                }
                if (blueprint != null)
                {
                    inventory.RemoveBlueprint(blueprint);
                }
            }
        }
    }
}
