using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InspectPointCutscene : MonoBehaviour
{
    private enum Level { denial, anger, bargaining, depression, acceptance }


    [Header("All Levels")]
    [SerializeField] private Level level;
    [SerializeField] private GameObject interactText;
    [SerializeField] private Animator cutsceneTextObject;
    [SerializeField] private Animator cutsceneText;
    [SerializeField] private float cutsceneTime = 3f;
    [SerializeField] private float cutsceneTransitionTime = 1f;
    [SerializeField] private float cameraZoomTime = 1.5f;
    [SerializeField] private GameObject uiElements;
    [SerializeField] private float zoomSpeed = 3f;

    private SaveManager saveManager;
    private PlayerMovement player;

    private bool canStartCutscene;
    private bool cutsceneStarted;
    private bool cutsceneFinished;

    private Transform cameraPos;
    private bool cameraZoomIn;
    private bool cameraZoomOut;

    [Header("Denial")]
    [SerializeField] private GameObject[] stringsOfDenial;

    [Header("Anger")]
    [SerializeField] private GameObject dashCutscene;


    private void Start()
    {
        cameraPos = FindObjectOfType<Camera>().transform;
        player = FindObjectOfType<PlayerMovement>();
        saveManager = FindObjectOfType<SaveManager>();
        cutsceneTextObject.gameObject.SetActive(false);
        cutsceneText.gameObject.SetActive(false);
        interactText.SetActive(false);

        if (level == Level.denial && saveManager.stringsCutsceneWatched)
        {
            foreach (var item in stringsOfDenial)
            {
                item.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
        if (level == Level.denial && saveManager.dashCutsceneWatched)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        CameraZoom();

        if (Input.GetButtonDown("Interact") && !cutsceneStarted && canStartCutscene)
        {
            StartCoroutine(Cutscene());
        }

        if (cutsceneFinished)
        {
            switch (level)
            {
                case Level.denial:
                    if (!saveManager.stringsCutsceneWatched)
                    {
                        saveManager.stringsCutsceneWatched = true;
                    }
                    foreach (var item in stringsOfDenial)
                    {
                        item.SetActive(true);
                    }
                    break;

                case Level.anger:
                    saveManager.dashObtained = true;
                    break;

                case Level.bargaining:
                    break;

                case Level.depression:
                    break;

                case Level.acceptance:
                    break;

                default:
                    break;
            }
        }
    }

    private void CameraZoom()
    {
        if (cameraZoomIn)
        {
            if (cameraPos.position.z != -12)
            {
                cameraPos.position = Vector3.MoveTowards(cameraPos.position, new Vector3(cameraPos.position.x, cameraPos.position.y, -12), zoomSpeed * Time.deltaTime);
            }
        }
        if (cameraZoomOut)
        {
            if (cameraPos.position.z != -20)
            {
                cameraPos.position = Vector3.MoveTowards(cameraPos.position, new Vector3(cameraPos.position.x, cameraPos.position.y, -20), zoomSpeed * Time.deltaTime);
            }
            else
            {
                player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.movementEnabled = true;
                uiElements.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!cutsceneStarted && !cutsceneFinished)
            {
                interactText.SetActive(true);
                canStartCutscene = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!cutsceneStarted && !cutsceneFinished)
        {
            interactText.SetActive(false);
            canStartCutscene = false;
        }
    }

    private IEnumerator Cutscene()
    {
        // Freeze player and remove text and UI
        canStartCutscene = false;
        cutsceneStarted = true;
        interactText.gameObject.SetActive(false);
        uiElements.gameObject.SetActive(false);
        player.movementEnabled = false;
        player.direction = 0;
        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        // Zoom in camera
        cameraZoomIn = true;
        yield return new WaitForSeconds(cameraZoomTime);

        // Start cutscene
        cutsceneTextObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(cutsceneTransitionTime);
        cutsceneText.gameObject.SetActive(true);
        yield return new WaitForSeconds(cutsceneTime);
        cameraZoomIn = false;
        cutsceneFinished = true;
        cutsceneText.SetTrigger("Fade");
        yield return new WaitForSeconds(cutsceneTransitionTime);
        cutsceneTextObject.SetTrigger("Fade");
        yield return new WaitForSeconds(cutsceneTransitionTime);
        if (level == Level.anger)
        {
            dashCutscene.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            cameraZoomOut = true;
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<Light2D>().enabled = false;
    }
}
