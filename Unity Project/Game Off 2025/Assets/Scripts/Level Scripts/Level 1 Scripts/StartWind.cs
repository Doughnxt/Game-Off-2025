using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWind : MonoBehaviour
{
    [SerializeField] private float textShowTime = 2f;
    [SerializeField] private float cameraZoomTime = 1.5f;
    [SerializeField] private float memoryTime = 2f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private Animator memoryAnimations;
    [SerializeField] private GameObject uiElements;
    [SerializeField] private GameObject cutsceneText;
    private Transform cameraPos;
    private PlayerMovement player;
    private Wind wind;
    [SerializeField] private WindVisuals visuals;
    private bool cameraZoomIn;
    private bool cameraZoomOut;

    private SaveManager save;


    private void Start()
    {
        save = FindObjectOfType<SaveManager>();
        wind = FindObjectOfType<Wind>();
        player = FindObjectOfType<PlayerMovement>();
        cameraPos = FindObjectOfType<Camera>().transform;
        if (save.windCutsceneWatched)
        {
            wind.windActive = true;
            visuals.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
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
            StartCoroutine(WindStartingCutscene());
        }
    }

    private IEnumerator WindStartingCutscene()
    {
        save.windCutsceneWatched = true;
        player.movementEnabled = false;
        player.direction = 0;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        uiElements.SetActive(false);
        cameraZoomIn = true;
        yield return new WaitForSeconds(cameraZoomTime);
        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        cameraZoomIn = false;
        memoryAnimations.gameObject.SetActive(true);
        yield return new WaitForSeconds(memoryTime);
        cutsceneText.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        cutsceneText.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(0.3f);
        wind.windActive = true;
        memoryAnimations.SetTrigger("Fade");
        yield return new WaitForSeconds(memoryTime);
        visuals.gameObject.SetActive(true);
        cameraZoomOut = true;
    }

}
