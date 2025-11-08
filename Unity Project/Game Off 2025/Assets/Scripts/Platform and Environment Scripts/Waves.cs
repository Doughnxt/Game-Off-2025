using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform peakPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float speed = 5;
    [SerializeField] private float waitTimeBeforeNextCycle;
    private bool startWaveCycle;
    private bool secondHalfOfCycle;
    private bool cycleEnded;

    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        if (transform.position == startPos.position)
        {
            startWaveCycle = true;
            cycleEnded = true;
        }
        if (transform.position == peakPos.position)
        {
            secondHalfOfCycle = true;
        }
        if (transform.position == endPos.position && cycleEnded)
        {
            StartCoroutine(WaitBeforeNextCycle());
        }

        if (startWaveCycle)
        {
            if (secondHalfOfCycle)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, peakPos.position, speed * Time.deltaTime);
            }

        }
    }

    private IEnumerator WaitBeforeNextCycle()
    {
        cycleEnded = false;
        startWaveCycle = false;
        secondHalfOfCycle = false;
        yield return new WaitForSeconds(waitTimeBeforeNextCycle);
        transform.position = startPos.position;

    }
}
