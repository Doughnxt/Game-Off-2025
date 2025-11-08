using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health variables")]
    public int maxHealth = 4;
    public int currentHealth;
    [SerializeField] private GameObject[] healthContainer;
    [SerializeField] private GameObject[] healthIndicator;
    [SerializeField] private GameObject healthUI;
    private SpriteRenderer sprite;

    [Header("Hazard respawn variables")]
    private Transform lastCheckpoint;
    [SerializeField] private float bufferAfterRespawn = 0.1f;
    private PlayerMovement player;
    private Rigidbody2D rb;

    // death and respawning variables
    private LevelLoader levelLoader;
    private Animator anim;
    public bool isDead;

    //misc. variables
    //private SavepointManager savepointManager;
    //public AudioSource damageSound;


    private void Start()
    {
        //savepointManager = FindObjectOfType<SavepointManager>();
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        levelLoader = FindObjectOfType<LevelLoader>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (maxHealth)
        {
            case 4:
                healthContainer[0].SetActive(false);
                healthContainer[1].SetActive(false);
                healthContainer[2].SetActive(false);
                break;

            case 5:
                healthContainer[0].SetActive(true);
                healthContainer[1].SetActive(false);
                healthContainer[2].SetActive(false);
                break;

            case 6:
                healthContainer[0].SetActive(true);
                healthContainer[1].SetActive(true);
                healthContainer[2].SetActive(false);
                break;

            case 7:
                healthContainer[0].SetActive(true);
                healthContainer[1].SetActive(true);
                healthContainer[2].SetActive(true);
                break;

            default:
                break;
        }

        switch (currentHealth)
        {
            case 0:
                healthIndicator[0].SetActive(false);
                healthIndicator[1].SetActive(false);
                healthIndicator[2].SetActive(false);
                rb.velocity = new Vector2(0, 0);
                player.movementEnabled = false;
                if (!isDead)
                {
                    anim.SetTrigger("Death");
                    isDead = true;
                }
                break;

            case 1:
                healthIndicator[0].SetActive(true);
                healthIndicator[1].SetActive(false);
                healthIndicator[2].SetActive(false);
                break;

            case 2:
                healthIndicator[0].SetActive(true);
                healthIndicator[1].SetActive(true);
                healthIndicator[2].SetActive(false);
                break;

            case 3:
                healthIndicator[0].SetActive(true);
                healthIndicator[1].SetActive(true);
                healthIndicator[2].SetActive(true);
                break;

            default:
                break;
        }

        if (currentHealth < 0)
        {
            anim.SetTrigger("Death");
            Die();
        }
    }


    private IEnumerator RespawnFromHazard()
    {
        //damageSound.Play();
        currentHealth--;
        yield return new WaitForSeconds(.05f);
        player.movementEnabled = false;
        rb.velocity = Vector2.zero;
        transform.position = lastCheckpoint.position;
        yield return new WaitForSeconds(bufferAfterRespawn);
        player.movementEnabled = true;
        //do a fade to black or something cool idk
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            StartCoroutine(RespawnFromHazard());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            StartCoroutine(RespawnFromHazard());
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint = collision.gameObject.transform;
        }
    }


    private void Die()
    {
        levelLoader.ReloadLevel();
    }

}
