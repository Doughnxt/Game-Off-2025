using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensionBlocks : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float speed = 50;
    [SerializeField] private float timeUntilReset = 0.8f;
    [SerializeField] private float timeUntilAscension = 0.2f;
    [SerializeField] private bool resetCountdownStarted;
    [SerializeField] private bool ascensionCoundownStarted;
    private bool canAscend;
    private bool platformCanReturn;
    private bool playerIsOnPlatform;


    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        if (canAscend && playerIsOnPlatform)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
        }
        if (transform.position == endPos.position)
        {
            canAscend = false;
            if (playerIsOnPlatform)
            {
                resetCountdownStarted = false;
            }
            else if (!playerIsOnPlatform && !resetCountdownStarted)
            {
                StartCoroutine(ResetCountdown());
            }
        }

        if (platformCanReturn && transform.position != startPos.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
        }

        if (transform.position == startPos.position)
        {
            platformCanReturn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position == startPos.position)
        {
            if (!ascensionCoundownStarted)
            {
                StartCoroutine(AscensionCountdown());
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnPlatform = false;
            ascensionCoundownStarted = false;
        }
    }

    private IEnumerator AscensionCountdown()
    {
        ascensionCoundownStarted = true;
        canAscend = false;
        yield return new WaitForSeconds(timeUntilAscension);
        canAscend = true;
    }

    private IEnumerator ResetCountdown()
    {
        yield return new WaitForSeconds(timeUntilReset);
        platformCanReturn = true;
        canAscend = false;
    }

}
