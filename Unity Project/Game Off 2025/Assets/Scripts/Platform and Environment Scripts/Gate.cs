using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private BoxCollider2D box;
    private Animator animator;
    private KeyCounter keyCounter;
    [SerializeField] private Range range;
    [SerializeField] private TypeOfGate type;
    [SerializeField] private Lever lever;
    [SerializeField] private bool opened;
    [SerializeField] private AudioSource gateAudio;

    private enum TypeOfGate { key, lever }

    void Start()
    {
        keyCounter = FindObjectOfType<KeyCounter>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (type)
        {
            case TypeOfGate.key:
                KeyGate();
                break;

            case TypeOfGate.lever:
                LeverGate();
                break;

            default:
                break;
        }
    }

    private void Open()
    {
        gateAudio.Play();
        box.enabled = false;
        animator.SetTrigger("Open");
        opened = true;
    }

    private void Close()
    {
        gateAudio.Play();
        box.enabled = true;
        animator.SetTrigger("Close");
        opened = false;
    }

    private void KeyGate()
    {
        if (!opened)
        {
            if (range.inRange)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (keyCounter.keyCount > 0)
                    {
                        keyCounter.keyCount--;
                        Open();
                    }
                }
            }
        }
    }

    private void LeverGate()
    {

        if (!opened)
        {
            if (lever.on)
            {
                Open();
            }
        }
        else if (opened)
        {
            if (!lever.on)
            {
                Close();

            }
        }


    }

}
