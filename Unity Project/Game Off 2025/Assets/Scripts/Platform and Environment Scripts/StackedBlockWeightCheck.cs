using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedBlockWeightCheck : MonoBehaviour
{
    private WeightCheck weight;
    public int blocksInCollider;

    private void Start()
    {
        weight = GetComponentInParent<WeightCheck>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            blocksInCollider += 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            if (weight.blockcollisionDetected)
            {
                weight.blockCount = blocksInCollider;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            blocksInCollider -= 1;
            if (!weight.blockcollisionDetected)
            {
                weight.blockCount -= 1;
            }
        }
    }
}
