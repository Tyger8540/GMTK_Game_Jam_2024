using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableController : MonoBehaviour
{
    public enum Orientation
    {
        Left,
        Right,
        Up,
        Down
    };

    public Orientation orientation;
    public Collider2D childColl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
