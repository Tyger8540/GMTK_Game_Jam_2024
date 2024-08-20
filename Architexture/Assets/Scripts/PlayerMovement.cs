using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;

    public LayerMask jumpableGround;
    public LayerMask ladderMask;

    public float dirX = 0f;

    public float defaultMoveSpeed = 5f;
    public float moveSpeed;
    public float jumpForce;
    public float sprintSpeed;
    public float lowerBound;
    public Vector3 startingPosition;

    public int playerDirection = 0;

    public bool inLadder = false;
    public float climbSpeed;
    public float springForce;
    //public float bruh;


    //private bool movingRight;
    //private bool movingLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Placeable"))
        {
            if (collision.gameObject.layer == 9)  // 9 being ladder
            {
                Debug.Log("in ladder");
                inLadder = true;
            }

            if (collision.gameObject.layer == 10)  // 10 being a spring
            {
                collision.GetComponent<LaunchpadController>().PlayAnimation();
                rb.velocity = new Vector2(rb.velocity.x, springForce);
            }
            else if (playerDirection > 0 && collision.GetComponent<PlaceableController>().orientation != PlaceableController.Orientation.Right)
            {
                Debug.Log(coll.gameObject.name);
                Debug.Log(collision.gameObject.name);
                Physics2D.IgnoreCollision(coll, collision.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(coll, collision.GetComponentInChildren<Collider2D>(), true);
            }
            else if (playerDirection < 0 && collision.GetComponent<PlaceableController>().orientation != PlaceableController.Orientation.Left)
            {
                Physics2D.IgnoreCollision(coll, collision.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(coll, collision.GetComponentInChildren<Collider2D>(), true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)  // 9 being ladder
        {
            inLadder = false;
        }
        if (collision.CompareTag("Placeable"))
        {                          
            Debug.Log("Exiting " + collision.gameObject.name + " collider");
            Physics2D.IgnoreCollision(coll, collision.GetComponent<Collider2D>(), false);
            Physics2D.IgnoreCollision(coll, collision.GetComponentInChildren<Collider2D>(), false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        moveSpeed = defaultMoveSpeed;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (rb.velocity.x > 0f)
        {
            movingRight = true;
            movingLeft = false;
        }
        else if (rb.velocity.x < 0f)
        {
            movingLeft = true;
            movingRight = false;
        }*/
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (dirX < 0f)
        {
            if (playerDirection >= 0)
            {
                Debug.Log("Here");
                OutlineController[] outlines = FindObjectsOfType<OutlineController>();
                Debug.Log(outlines.Length);
                foreach (OutlineController outline in outlines)
                {
                    outline.ChangeDirection(outline.leftPosition);
                }
            }
            playerDirection = -1;
        }
        else if (dirX > 0f)
        {
            if (playerDirection < 0)
            {
                OutlineController[] outlines = FindObjectsOfType<OutlineController>();
                foreach (OutlineController outline in outlines)
                {
                    outline.ChangeDirection(outline.rightPosition);
                }
            }
            playerDirection = 1;
        }

        if (Input.GetKey(KeyCode.W) && inLadder)
        {
            Debug.Log("holding W in ladder");
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.S) && IsOnLadderTop() && IsGrounded())  // doing all these check to make sure you are on top of the ladder
        {
            Debug.Log("holding S in ladder");
            rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);
            inLadder = true;
            transform.position -= new Vector3(0f, 0.6f, 0f);
            //transform.position = new Vector3(transform.position.x, transform.position.y - 4f, 0f);
        }
        else if (Input.GetKey(KeyCode.S) && inLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);
        }
        //Debug.Log(IsOnLadderTop());
        /*if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
        {
            moveSpeed = sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = defaultMoveSpeed;
        }*/

        if (transform.position.y <= lowerBound)
        {
            Respawn(startingPosition);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public bool IsOnLadderTop()
    {
        //return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 10f, ladderMask);
        return (!inLadder && (Physics2D.Raycast(coll.bounds.center, Vector2.down, 1f, ladderMask) || Physics2D.Raycast(coll.bounds.center + coll.bounds.extents, Vector2.down, 1f, ladderMask) || Physics2D.Raycast(coll.bounds.center - coll.bounds.extents, Vector2.down, 1f, ladderMask)));
    }

    private void Respawn(Vector3 spawnpoint)
    {
        transform.position = spawnpoint;
        // probably will do more like decrease lives and stuff
    }
}
