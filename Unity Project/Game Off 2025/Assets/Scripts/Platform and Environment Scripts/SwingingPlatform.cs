using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{

    [SerializeField] private GameObject platform1;
    [SerializeField] private GameObject platform2;
    [SerializeField] private float halfwayPointPosition;
    [SerializeField] private float buffer = 0.05f;
    [SerializeField] private bool switched;

    // WHEN SETTING UP, MAKE SURE PARENT OBJECT 1 IS HALF THE RADIUS OF ROTATION ABOVE PARENT OBJECT 2

    private void Update()
    {
        if (platform1.transform.position.y >= halfwayPointPosition && switched)
        {
            switched = !switched;
            platform1.GetComponent<BoxCollider2D>().enabled = false;
            platform1.GetComponent<SpriteRenderer>().enabled = false;
            platform2.GetComponent<BoxCollider2D>().enabled = true;
            platform2.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (platform1.transform.position.y <= halfwayPointPosition && !switched)
        {
            switched = !switched;
            platform1.GetComponent<BoxCollider2D>().enabled = true;
            platform1.GetComponent<SpriteRenderer>().enabled = true;
            platform2.GetComponent<BoxCollider2D>().enabled = false;
            platform2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
