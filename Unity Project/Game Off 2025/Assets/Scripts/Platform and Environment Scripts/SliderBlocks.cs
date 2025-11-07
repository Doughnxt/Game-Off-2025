using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBlocks : MonoBehaviour
{
    // START POS IS THE UPMOST POSITION
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float lowerSpeed = 1;
    [SerializeField] private float riseSpeed = 0.5f;
    [SerializeField] private float timeUntilReset = 1;
    private bool playerIsWeighingDown;
    private bool platformCanReturn;

    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        if (playerIsWeighingDown)
        {
            platformCanReturn = false;
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, lowerSpeed * Time.deltaTime);
        }

        if (platformCanReturn && !playerIsWeighingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, riseSpeed * Time.deltaTime);
        }
        if (transform.position == startPos.position)
        {
            platformCanReturn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsWeighingDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsWeighingDown = false;
            StartCoroutine(ResetCountdown());
        }
    }

    private IEnumerator ResetCountdown()
    {
        yield return new WaitForSeconds(timeUntilReset);
        platformCanReturn = true;
    }
}
