using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement player;
    public int blocksStacked;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!player.canPush)
        {
            rb.mass = 9999;
        }
        else
        {
            rb.mass = 10;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            blocksStacked++;
            gameObject.transform.SetParent(collision.transform);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            blocksStacked--;
            gameObject.transform.SetParent(null);
        }
    }
}