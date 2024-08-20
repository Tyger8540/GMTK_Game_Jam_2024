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
    public CameraFollow camScript;
    public bool inBlueprintMode = false;
    public GameObject bridgeOutline;
    public GameObject stairsOutline;
    public GameObject ladderOutline;
    public GameObject springOutline;
    public LayerMask jumpableGround;
    public bool outlinesBlue = true;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        camScript = FindObjectOfType<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inBlueprintMode && !playerMovementScript.IsGrounded())
        {
            SetOutlinesRed();
        }
        else if (!inBlueprintMode && Input.GetKeyDown(KeyCode.Space))
        {
            inBlueprintMode = true;
            ShowCurrentOutline();
        }
        else if (inBlueprintMode)
        {
            if (inventory.container.Count > 0)
            {
                Blueprint blueprint = inventory.container[0].blueprint;
                if (!CurrentOutlineActive())
                {
                    ShowCurrentOutline();
                }
                if (Physics2D.Raycast(new Vector2(GetCurrentOutline().GetComponent<Collider2D>().bounds.center.x - (playerMovementScript.playerDirection * GetCurrentOutline().GetComponent<Collider2D>().bounds.size.x / 2), transform.position.y), Vector2.down, 1f, jumpableGround))
                {
                    if (GetCurrentOutline() == bridgeOutline)
                    {
                        SetOutlinesRed();
                    }
                    else
                    {
                        SetOutlinesBlue();
                    }
                }
                else
                {
                    if (GetCurrentOutline() == bridgeOutline)
                    {
                        SetOutlinesBlue();
                    }
                    else
                    {
                        SetOutlinesRed();
                    }
                }
                //Debug.Log(new Vector2(GetCurrentOutline().GetComponent<Collider2D>().bounds.center.x - (playerMovementScript.playerDirection * GetCurrentOutline().GetComponent<Collider2D>().bounds.size.x / 2), transform.position.y));

                /*if (Physics2D.BoxCast(new Vector2(GetCurrentOutline().GetComponent<Collider2D>().bounds.center.x - (playerMovementScript.playerDirection * GetCurrentOutline().GetComponent<Collider2D>().bounds.size.x / 2), GetCurrentOutline().transform.position.y), new Vector2(.1f, .1f), 0f, Vector2.down, .1f, jumpableGround))
                {
                    Debug.Log(new Vector2(GetCurrentOutline().GetComponent<Collider2D>().bounds.center.x - (playerMovementScript.playerDirection * GetCurrentOutline().GetComponent<Collider2D>().bounds.size.x / 2), GetCurrentOutline().transform.position.y));
                    SetOutlinesBlue();
                }
                else
                {
                    Debug.Log(new Vector2(GetCurrentOutline().GetComponent<Collider2D>().bounds.center.x - (playerMovementScript.playerDirection * GetCurrentOutline().GetComponent<Collider2D>().bounds.size.x / 2), GetCurrentOutline().transform.position.y));
                    SetOutlinesRed();
                }*/
                if (Input.GetKeyDown(KeyCode.Space) && outlinesBlue)
                {
                    HideOutlines();
                    if (GetCurrentOutline() == bridgeOutline)
                    {
                        camScript.PlaySound(camScript.bridge);
                    }
                    else if (GetCurrentOutline() == stairsOutline)
                    {
                        camScript.PlaySound(camScript.stairs);
                    }
                    else if (GetCurrentOutline() == ladderOutline)
                    {
                        camScript.PlaySound(camScript.ladder);
                    }
                    else if (GetCurrentOutline() == springOutline)
                    {
                        camScript.PlaySound(camScript.launchpad);
                    }
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
                        inBlueprintMode = false;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space) && !outlinesBlue)
                {
                    camScript.PlaySound(camScript.invalid);
                }
            }
        }
    }

    public void HideOutlines()
    {
        bridgeOutline.SetActive(false);
        stairsOutline.SetActive(false);
        ladderOutline.SetActive(false);
        springOutline.SetActive(false);
    }

    public void ShowCurrentOutline()
    {
        string blueprintName = inventory.container[0].blueprint.name;
        if (blueprintName == "Bridge")
        {
            bridgeOutline.SetActive(true);
        }
        else if (blueprintName == "Stairs")
        {
            stairsOutline.SetActive(true);
        }
        else if (blueprintName == "Ladder")
        {
            ladderOutline.SetActive(true);
        }
        else if (blueprintName == "Launchpad")
        {
            springOutline.SetActive(true);
        }
    }

    public GameObject GetCurrentOutline()
    {
        string blueprintName = inventory.container[0].blueprint.name;
        if (blueprintName == "Bridge")
        {
            return bridgeOutline;
        }
        else if (blueprintName == "Stairs")
        {
            return stairsOutline;
        }
        else if (blueprintName == "Ladder")
        {
            return ladderOutline;
        }
        else if (blueprintName == "Launchpad")
        {
            return springOutline;
        }
        else
        {
            return null;
        }
    }

    public bool CurrentOutlineActive()
    {
        string blueprintName = inventory.container[0].blueprint.name;
        if (blueprintName == "Bridge")
        {
            return bridgeOutline.activeSelf;
        }
        else if (blueprintName == "Stairs")
        {
            return stairsOutline.activeSelf;
        }
        else if (blueprintName == "Ladder")
        {
            return ladderOutline.activeSelf;
        }
        else if (blueprintName == "Launchpad")
        {
            return springOutline.activeSelf;
        }
        else
        {
            return false;
        }
    }

    public void SetOutlinesRed()
    {
        bridgeOutline.GetComponent<OutlineController>().SetRed();
        stairsOutline.GetComponent<OutlineController>().SetRed();
        ladderOutline.GetComponent<OutlineController>().SetRed();
        springOutline.GetComponent<OutlineController>().SetRed();
        outlinesBlue = false;
    }

    public void SetOutlinesBlue()
    {
        bridgeOutline.GetComponent<OutlineController>().SetBlue();
        stairsOutline.GetComponent<OutlineController>().SetBlue();
        ladderOutline.GetComponent<OutlineController>().SetBlue();
        springOutline.GetComponent<OutlineController>().SetBlue();
        outlinesBlue = true;
    }
}
