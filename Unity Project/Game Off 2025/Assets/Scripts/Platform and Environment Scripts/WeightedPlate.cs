using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlate : MonoBehaviour
{
    public bool isWeighedDown;
    private int numberOfCollidingObjects;

    private void Update()
    {
        isWeighedDown = numberOfCollidingObjects > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            numberOfCollidingObjects++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            numberOfCollidingObjects--;
        }
    }
}
