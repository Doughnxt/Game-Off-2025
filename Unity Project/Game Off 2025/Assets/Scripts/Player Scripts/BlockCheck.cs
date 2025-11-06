using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public bool isTouchingBlock = false;
    public bool isTouchingDashPushBlock = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable Block"))
        {
            isTouchingBlock = true;
        }
        if (collision.gameObject.CompareTag("Dash Push Block"))
        {
            isTouchingDashPushBlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable Block"))
        {
            isTouchingBlock = false;
        }
        if (collision.gameObject.CompareTag("Dash Push Block"))
        {
            isTouchingDashPushBlock = false;
        }
    }
}