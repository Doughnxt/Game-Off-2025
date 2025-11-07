using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private KeyManager keyManager;

    private void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            keyManager.keyCount++;
            // Play animation or vfx and a sound
            this.gameObject.SetActive(false);
        }
    }
}
