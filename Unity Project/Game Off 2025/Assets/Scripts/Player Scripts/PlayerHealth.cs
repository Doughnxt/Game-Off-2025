using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health variables")]
    public int maxHealth = 3;
    public int currentHealth;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Animator screenShake;

    // death and respawning variables
    private LevelLoader levelLoader;
    private Animator anim;
    public bool isDead;
    private PlayerMovement player;
    private Rigidbody2D rb;

    //misc. variables
    //private SavepointManager savepointManager;
    //public AudioSource damageSound;


    private void Start()
    {
        //savepointManager = FindObjectOfType<SavepointManager>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        levelLoader = FindObjectOfType<LevelLoader>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {

        switch (currentHealth)
        {
            case 0:
                hearts[2].GetComponent<Animator>().SetTrigger("Damage");
                player.movementEnabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                if (!isDead)
                {
                    StartCoroutine(Die());
                }
                break;

            case 1:
                hearts[1].GetComponent<Animator>().SetTrigger("Damage");
                break;

            case 2:
                hearts[0].GetComponent<Animator>().SetTrigger("Damage");
                break;


            default:
                break;
        }

        if (currentHealth < 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isDead = true;
        anim.SetTrigger("Death");
        mainCamera.transform.SetParent(screenShake.gameObject.transform);
        screenShake.SetBool("TakingDamage", true);
        yield return new WaitForSeconds(animationTime);
        mainCamera.transform.SetParent(null);
        screenShake.SetBool("TakingDamage", false);
        levelLoader.ReloadLevel();
    }

}
