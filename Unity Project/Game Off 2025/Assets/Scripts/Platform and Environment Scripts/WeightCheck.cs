using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightCheck : MonoBehaviour
{
    public int blockCount;
    public bool playerIsWeighingDown;
    public bool blockcollisionDetected;
    public int numberOfBlocksDetected;

    private void Update()
    {
        if (numberOfBlocksDetected < 1)
        {
            blockcollisionDetected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsWeighingDown = true;
        }
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            numberOfBlocksDetected++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            blockcollisionDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsWeighingDown = false;
        }
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            numberOfBlocksDetected--;
        }
    }
}
