using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StringsOfDenial : MonoBehaviour
{
    [SerializeField] private Range range;
    private PlayerMovement player;
    [SerializeField] private Transform startPos;
    private bool moving;
    [SerializeField] private float bufferTime = 0.1f;
    [SerializeField] private float zoomSpeed = 25;
    [SerializeField] private float zoomDuration = 0.5f;


    // Disable movement
    // Set an animation state for the player
    // Have a synced animation that plays at the same time (all will be same length except last one)
    // Pull player forward by certain amount
    // Let player move again
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        moving = false;
    }


    void Update()
    {
        if (range.inRange)
        {
            // Show text showing what input needs to be pressed
            if (Input.GetButtonDown("Interact"))
            {
                if (!moving)
                {
                    StartCoroutine(ZoomOnString());
                }
            }
        }
    }

    private IEnumerator ZoomOnString()
    {
        moving = true;
        player.movementEnabled = false;
        player.transform.position = startPos.position;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        // Set player animation state to holding_on
        yield return new WaitForSeconds(bufferTime);
        // Start string animation
        // Start flashing VFX and play heartbeat sound effect
        // Darken edges of screen and add blur effect
        player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * zoomSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(zoomDuration);
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        // Change player animation to falling
        player.movementEnabled = true;
        moving = false;
    }
}
