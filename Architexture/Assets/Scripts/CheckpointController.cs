using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public bool reachedCheckpoint = false;
    public CameraFollow camScript;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !reachedCheckpoint)
        {
            //camScript.PlaySound(camScript.checkpoint);
            reachedCheckpoint = true;
            //GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        camScript = FindObjectOfType<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
