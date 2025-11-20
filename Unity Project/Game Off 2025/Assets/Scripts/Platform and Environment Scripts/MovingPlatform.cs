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
    [SerializeField] private float waitTimeBeforeTurnAround = 0;
    private bool canGoToEnd;
    private bool canGoToStart;
    private bool resetStart;
    private bool resetEnd;

    private void Start()
    {
        transform.position = endPos;
        canGoToStart = true;
        endReached = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (vertical)
        {
            if (endReached && canGoToStart)
            {
                if (transform.position.y != endPos.y)
                {
                    transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                }
                if (transform.position.y == endPos.y)
                {
                    if (!resetEnd)
                    {
                        StartCoroutine(CanGoToEnd());
                    }

                }
            }
            else if (!endReached && canGoToEnd)
            {
                if (transform.position.y != startPos.y)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                }
                if (transform.position.y == startPos.y)
                {
                    if (!resetStart)
                    {
                        StartCoroutine(CanGoToStart());
                    }
                }

            }
        }
        else
        {
            if (endReached && canGoToStart)
            {
                if (transform.position.x != endPos.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                }
                if (transform.position.x == endPos.x)
                {
                    if (!resetEnd)
                    {
                        StartCoroutine(CanGoToEnd());
                    }
                }
            }
            else if (!endReached && canGoToEnd)
            {
                if (transform.position.x != startPos.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                }
                if (transform.position.x == startPos.x)
                {
                    if (!resetStart)
                    {
                        StartCoroutine(CanGoToStart());
                    }
                }

            }

        }
    }
    public void ResetPlatform()
    {
        transform.position = endPos;
    }
    private IEnumerator CanGoToEnd()
    {
        resetEnd = true;
        canGoToStart = false;
        endReached = false;
        yield return new WaitForSeconds(waitTimeBeforeTurnAround);
        canGoToEnd = true;
        resetEnd = false;
    }
    private IEnumerator CanGoToStart()
    {
        resetStart = true;
        canGoToEnd = false;
        endReached = true;
        yield return new WaitForSeconds(waitTimeBeforeTurnAround);
        canGoToStart = true;
        resetStart = false;
    }
}