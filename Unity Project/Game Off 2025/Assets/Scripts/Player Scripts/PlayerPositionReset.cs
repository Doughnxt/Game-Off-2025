using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionReset : MonoBehaviour
{
    private SaveManager saveManager;
    [SerializeField] private float respawnTime = 0.5f;
    [SerializeField] private float playerFreezeTime = 0.5f;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Animator screenShake;
    private Animator anim;
    private PlayerMovement player;
    private Rigidbody2D rb;
    private DriftingBlocks[] driftingBlocks;


    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = saveManager.lastSavepointPos;
        driftingBlocks = FindObjectsOfType<DriftingBlocks>();
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
        mainCamera.transform.SetParent(screenShake.gameObject.transform);
        screenShake.SetBool("TakingDamage", true);
        anim.SetTrigger("Damage");
        // Play sound effect
        yield return new WaitForSeconds(respawnTime);
        mainCamera.transform.SetParent(null);
        screenShake.SetBool("TakingDamage", false);
        transform.position = saveManager.lastCheckpointPos;
        foreach (var item in driftingBlocks)
        {
            item.RespawnBlock();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(playerFreezeTime);
        player.movementEnabled = true;
        player.dashEnabled = true;
    }
}
