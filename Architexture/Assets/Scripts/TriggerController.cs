using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerController : MonoBehaviour
{
    public GameObject cam;
    public bool isStartTrigger;
    public bool isEndTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isStartTrigger)
        {
            cam.GetComponent<CameraFollow>().Expand();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && isEndTrigger)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}
