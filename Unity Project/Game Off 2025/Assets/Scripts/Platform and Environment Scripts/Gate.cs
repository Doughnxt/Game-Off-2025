using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private BoxCollider2D box;
    private Animator animator;
    private KeyManager keyManager;
    [SerializeField] private Range range;
    [SerializeField] private TypeOfGate type;
    [SerializeField] private Lever lever;
    [SerializeField] private bool opened;
    //[SerializeField] private AudioSource gateAudio;
    [SerializeField] private GameObject gateMessage;
    [SerializeField] private GameObject gateInteractText;
    [SerializeField] private GameObject gateOpenText;
    [SerializeField] private float gateMessgeTime = 2f;
    private PlayerMovement player;

    private enum TypeOfGate { key, lever }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        keyManager = FindObjectOfType<KeyManager>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        gateMessage.SetActive(false);
        gateInteractText.SetActive(false);
        gateOpenText.SetActive(false);
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
        //gateAudio.Play();
        box.enabled = false;
        opened = true;
        animator.SetTrigger("Open");
    }

    private void Close()
    {
        //gateAudio.Play();
        box.enabled = true;
        opened = false;
        animator.SetTrigger("Close");
    }

    private void KeyGate()
    {
        if (!opened)
        {
            if (range.inRange)
            {
                if (keyManager.keyCount <= 0)
                {
                    gateInteractText.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        StartCoroutine(ShowGateMessage());
                    }
                }
                else
                {
                    gateOpenText.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        keyManager.keyCount--;
                        Open();
                    }
                }

            }
            else
            {
                gateInteractText.SetActive(false);
                gateOpenText.SetActive(false);
            }
        }
        if (opened)
        {
            gateInteractText.SetActive(false);
            gateOpenText.SetActive(false);
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

    private IEnumerator ShowGateMessage()
    {
        player.direction = 0;
        gateInteractText.SetActive(false);
        player.movementEnabled = false;
        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gateMessage.SetActive(true);
        yield return new WaitForSeconds(gateMessgeTime);
        gateMessage.SetActive(false);
        player.movementEnabled = true;
        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gateInteractText.SetActive(true);
    }

}
