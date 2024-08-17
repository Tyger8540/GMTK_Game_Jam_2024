using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintController : MonoBehaviour
{
    public Vector3 startingPosition;
    public float hoverSpeed;
    public float offset;
    public bool movingUp;
    public bool movingDown;

    // Start is called before the first frame update
    void Start()
    {
        movingUp = true;
        movingDown = false;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
