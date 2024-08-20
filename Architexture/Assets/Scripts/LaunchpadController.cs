using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadController : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation()
    {
        StartCoroutine(ExtendSpring());
    }

    IEnumerator ExtendSpring()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        yield return null;
    }
}
