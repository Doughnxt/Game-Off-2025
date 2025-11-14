using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().currentHealth--;
            if (collision.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                collision.GetComponent<PlayerPositionReset>().ResetPosition();
            }
        }
        if (collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            collision.gameObject.GetComponent<PushableBlock>().RespawnBlock();
        }
    }
}
