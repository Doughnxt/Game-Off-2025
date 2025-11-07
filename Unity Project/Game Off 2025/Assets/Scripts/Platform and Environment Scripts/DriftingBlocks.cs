using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftingBlocks : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public bool OnTop()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0.1f, Vector2.up, .1f, playerLayer);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (OnTop() && collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
