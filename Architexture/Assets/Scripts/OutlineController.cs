using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public Vector3 rightPosition;
    public Vector3 leftPosition;
    public bool inRightPosition = true;
    public Sprite[] sprites;

    public PlayerMovement playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        if (playerMovementScript.playerDirection < 0)
        {
            ChangeDirection(leftPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementScript.playerDirection > 0 && !inRightPosition)
        {
            ChangeDirection(rightPosition);
            inRightPosition = true;
        }
        else if (playerMovementScript.playerDirection < 0 && inRightPosition)
        {
            ChangeDirection(leftPosition);
            inRightPosition = false;
        }
    }

    public void ChangeDirection(Vector3 position)
    {
        transform.localPosition = position;
        transform.Rotate(new Vector3(0f, 180f, 0f));
        inRightPosition = !inRightPosition;
    }

    public void SetRed()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
    }

    public void SetBlue()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
