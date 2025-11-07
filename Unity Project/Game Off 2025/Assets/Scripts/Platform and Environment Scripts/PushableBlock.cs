using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement player;
    private bool canBePushed;
    private BlockCheck blockCheck;
    private BoxCollider2D coll;
    private bool playerIsTouching;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        blockCheck = FindObjectOfType<BlockCheck>();

    }

    private void Update()
    {
        if (!player.canPush)
        {
            rb.mass = 9999;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = 10;
        }

        // Use boxcast to check for other block
    }
    public bool IsStacked()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0.1f, Vector2.up, .1f, Physics2D.AllLayers, 0.001f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsTouching = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsStacked() && !playerIsTouching)
        {
            if (collision.gameObject.GetComponent<PushableBlock>() != null)
            {
                rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

}