using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public bool finishedExpandingCam = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0f, player.transform.position.y + 3f, -10f);
    }

    public void Expand()
    {
        StartCoroutine(ExpandCamera());
    }

    IEnumerator ExpandCamera()
    {
        for (int i = 0; i < 50; i++)
        {
            GetComponent<Camera>().orthographicSize += .1f;
            yield return new WaitForSeconds(.05f);
        }

        finishedExpandingCam = true;
        yield return null;
    }
}
