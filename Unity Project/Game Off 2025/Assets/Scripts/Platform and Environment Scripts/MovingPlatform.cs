using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    private bool endReached;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float speed = 5;
    [SerializeField] private bool vertical = false;

    // Update is called once per frame
    void Update()
    {
        if (vertical)
        {
            if (endReached)
            {
                if (transform.position.y != endPos.y)
                {
                    transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                }
                if (transform.position.y == endPos.y)
                {
                    endReached = false;
                }
            }
            else
            {
                if (transform.position.y != startPos.y)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                }
                if (transform.position.y == startPos.y)
                {
                    endReached = true;
                }

            }
        }
        else
        {
            if (endReached)
            {
                if (transform.position.x != endPos.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                }
                if (transform.position.x == endPos.x)
                {
                    endReached = false;
                }
            }
            else
            {
                if (transform.position.x != startPos.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                }
                if (transform.position.x == startPos.x)
                {
                    endReached = true;
                }

            }

        }
    }
}