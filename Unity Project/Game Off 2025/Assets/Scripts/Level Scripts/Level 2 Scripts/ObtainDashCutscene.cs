using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainDashCutscene : MonoBehaviour
{
    [SerializeField] private Animator whiteFade;
    [SerializeField] private GameObject text;
    private Animator playerAnimator;
    [SerializeField] private float textShowTime = 3f;
    [SerializeField] private float timeBeforeFade = 2f;

    [SerializeField] private GameObject uiElements;
    private Transform cameraPos;
    private bool cameraZoomOut;
    [SerializeField] private float zoomSpeed = 3f;
    private PlayerMovement player;

    private void Start()
    {
        cameraPos = FindObjectOfType<Camera>().transform;
        player = FindObjectOfType<PlayerMovement>();
        playerAnimator = player.gameObject.GetComponent<Animator>();
        StartCoroutine(DashCutscene());
    }

    private void Update()
    {
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

    private IEnumerator DashCutscene()
    {
        yield return new WaitForSeconds(1);
        playerAnimator.SetTrigger("Upgrade");
        yield return new WaitForSeconds(timeBeforeFade);
        whiteFade.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        text.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        whiteFade.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        cameraZoomOut = true;
    }
}
