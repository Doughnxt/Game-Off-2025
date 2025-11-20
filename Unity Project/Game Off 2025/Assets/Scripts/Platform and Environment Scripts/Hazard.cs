using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private float respawnTime = 0.2f;
    private PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().currentHealth--;
            if (collision.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.GetComponent<PlayerPositionReset>().ResetPosition();
                Invoke(nameof(EnablePlayerCollisionAgain),respawnTime);
            }
        }
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            if (this.gameObject.GetComponent<Waves>() == null)
            {
                collision.gameObject.GetComponent<PushableBlock>().RespawnBlock();
            }

        }
    }
    private void EnablePlayerCollisionAgain()
    {
        player.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
