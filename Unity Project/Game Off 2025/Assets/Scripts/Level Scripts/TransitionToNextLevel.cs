using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToNextLevel : MonoBehaviour
{
    private LevelLoader levelLoader;
    private SaveManager saveManager;
    private PlayerMovement player;
    private bool transitionStarted;
    [SerializeField] private float textShowTime = 2f;
    [SerializeField] private Animator backgroundImage;
    [SerializeField] private Animator titleCardBackgroundImage;
    [SerializeField] private Animator titleCardText;
    [SerializeField] private Animator textReveal;
    [SerializeField] private GameObject endMessage;


    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        saveManager = FindObjectOfType<SaveManager>();
        player = FindObjectOfType<PlayerMovement>();
        if (saveManager.transitioningToNextLevel)
        {
            backgroundImage.gameObject.SetActive(true);
            backgroundImage.SetTrigger("Fade_Out");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!transitionStarted && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        transitionStarted = true;
        backgroundImage.gameObject.SetActive(true);
        backgroundImage.SetTrigger("Fade_In");
        yield return new WaitForSeconds(2);
        player.movementEnabled = false;
        player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        textReveal.gameObject.SetActive(true);
        endMessage.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        textReveal.SetTrigger("Hide");
        yield return new WaitForSeconds(1);
        endMessage.SetActive(false);
        textReveal.gameObject.SetActive(false);
        titleCardBackgroundImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        titleCardText.gameObject.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        titleCardText.SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        titleCardBackgroundImage.SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        saveManager.transitioningToNextLevel = true;
        levelLoader.LoadNextLevel();
    }
}
