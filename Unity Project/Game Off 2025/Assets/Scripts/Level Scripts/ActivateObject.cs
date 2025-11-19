using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private bool deactivate;
    [SerializeField] private bool resetActivationTime;
    [SerializeField] private float timeBeforeActivation = 0;

    private void Start()
    {
        objectToActivate.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Activate());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && deactivate)
        {
            objectToActivate.SetActive(false);
        }
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(timeBeforeActivation);
        objectToActivate.SetActive(true);
        if (resetActivationTime)
        {
            timeBeforeActivation = 0;
        }
    }
}
