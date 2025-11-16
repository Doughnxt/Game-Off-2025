using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftingBlocks : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 startPos;
    [SerializeField] private GameObject blockers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.bodyType = RigidbodyType2D.Static;
        blockers.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            blockers.SetActive(true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            blockers.SetActive(false);
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void RespawnBlock()
    {
        transform.position = startPos;
    }
}
