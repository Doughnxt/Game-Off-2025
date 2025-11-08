using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionReset : MonoBehaviour
{
    private SaveManager saveManager;
    [SerializeField] private float respawnTime = 0.3f;
    [SerializeField] private float playerFreezeTime = 0.3f;
    private PlayerMovement player;
    private Rigidbody2D rb;


    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = saveManager.lastSavepointPos;
    }
    public void ResetPosition()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        player.movementEnabled = false;
        player.dashEnabled = false;
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Static;
        // Start animation/fade to black and a screen shake
        // Play sound effect
        yield return new WaitForSeconds(respawnTime);
        transform.position = saveManager.lastCheckpointPos;
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(playerFreezeTime);
        player.movementEnabled = true;
        player.dashEnabled = true;
    }
}
