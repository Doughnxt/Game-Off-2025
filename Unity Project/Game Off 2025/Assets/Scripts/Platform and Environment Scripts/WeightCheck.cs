using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightCheck : MonoBehaviour
{
    public int blockCount;
    public bool playerIsWeighingDown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsWeighingDown = true;
        }
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            blockCount++;
            blockCount += collision.gameObject.GetComponent<PushableBlock>().blocksStacked;
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
            blockCount--;
            blockCount -= collision.gameObject.GetComponent<PushableBlock>().blocksStacked;
        }
    }
}
