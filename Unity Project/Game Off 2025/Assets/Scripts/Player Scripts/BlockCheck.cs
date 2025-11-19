using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public bool isTouchingBlock = false;
    public bool isTouchingDashPushBlock = false;
    [SerializeField] private GameObject pushText;

    private void Start()
    {
        pushText.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable Block"))
        {
            isTouchingBlock = true;
            pushText.SetActive(true);
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
            pushText.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Dash Push Block"))
        {
            isTouchingDashPushBlock = false;
        }
    }
}