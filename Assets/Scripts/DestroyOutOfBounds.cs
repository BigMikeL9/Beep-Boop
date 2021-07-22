using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class/script controls the boundaries of the enemies, and destroys them if they reach the boundaries. 
public class DestroyOutOfBounds : MonoBehaviour
{
    private float xRangeRight = 15;
    private float xRangeLeft = -15;
    private float zRangeTop = 10;
    private float zRangeBottom = -10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutOfBound();
    }


    void DestroyOutOfBound ()
    {
        if (transform.position.x > xRangeRight)
        {
            Destroy(gameObject);
        }

        else if (transform.position.x < xRangeLeft)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > zRangeTop)
        {
            Destroy(gameObject);
        }

        else if (transform.position.z < zRangeBottom)
        {
            Destroy(gameObject);
        }
    }
}
